#! "netcoreapp2.2"
#r "nuget:NetStandard.Library,2.0.3"
#r "nuget:Microsoft.Azure.Management.Fluent,1.27.2"
#load "common-process.csx"
#load "transform.csx"
#load "web.csx"

using static MM;
using System.Text.RegularExpressions;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

//Console.WriteLine("Test");
//MM.WriteLine("Test", LogLevel.Error);
//MM.WriteLine("Test 2");

var pathToTools = GetScriptFolder();
var solutionDir = Path.GetFullPath(Path.Combine(pathToTools, ".."));
var testProjectDir = Path.Combine(solutionDir, "ToDoApp.Tests");
var webProjectDir = Path.Combine(solutionDir, "ToDoApp.Presentation.Web");
var webProjectFile = Path.Combine(webProjectDir, "ToDoApp.Presentation.Web.csproj");
var webProjectClientDir = Path.Combine(webProjectDir, "ClientApp");
var pathToPublish = Path.Combine(solutionDir, "webapps");
var azureAppName = "ToDoAppGreenLearning";
var azureRegionName = "todo-app-green-learning";

Action("info", () => Cmd("dotnet --version"));
Action("test", () => Cmd("dotnet test -c Debug -l:\"trx;LogFileName=results.trx\" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover", testProjectDir), "Unit tests", "info");
Action("npm-install", () => Shell("npm install", webProjectClientDir), "Install packages");
Action("ng-build", () => Shell("npm run build -- --prod", webProjectClientDir), "Install Angular", "npm-install");
Action("ng-test", () => Shell("npm run test", webProjectClientDir), "Run Angular Tests", "npm-install");
Action("tslint", () => Shell("npm run lint", webProjectClientDir), "Ts Lint", "npm-install");
Action("cpd", () => Shell($"npm install --no-save jscpd@2.0.15 && \"node_modules/.bin/jscpd.cmd\" -r xml,html -f csharp -i \"**/node_modules/**,**/*.Designer.cs,**/obj/**,**/bin/**\" -o \"{solutionDir}\" \"{solutionDir}\"", pathToTools), "Copy-paste detection");
Action("build", () => Cmd($"dotnet publish -c Release /p:RunCodeAnalysis=false -o {pathToPublish}", webProjectDir), "Build for Release", "test", "ng-build", "ng-test", "cpd", "tslint");
Action("transform", () => Transform.TransformSettingsJson(Path.Combine(pathToPublish, "appsettings.json"), Path.Combine(solutionDir, Globals.Args["settings"])), "Transform appsettings.json");
Action("azure-create", () => Azure
    .Authenticate(Globals.Args["azureauth"])
    .WithDefaultSubscription()
    .WebApps
    .Define(azureAppName)
    .WithRegion(Region.USWest)
    .WithNewResourceGroup(azureRegionName)
    .WithNewLinuxPlan(PricingTier.FreeF1)
    .WithBuiltInImage(RuntimeStack.NETCore_V2_2)
    .Create(), "Craete Azure");
Action("azure-deploy", () => {
    var azure = Azure.Authenticate(Globals.Args["azureauth"]).WithDefaultSubscription();
    var profile = azure
        .WebApps
        .GetByResourceGroup(azureRegionName, azureAppName)
        .GetPublishingProfile();

    UploadFolderAsZip($"https://{azureAppName.ToLowerInvariant()}.scm.azurewebsites.net/api/zipdeploy", pathToPublish, profile.GitUsername, profile.GitPassword);
    }, "Create Azure Web App And Deploy", "build", "transform");
Action("azure-clean", () => Azure.Authenticate(Globals.Args["azureauth"]).WithDefaultSubscription().ResourceGroups.DeleteByName(azureRegionName), "Clean azure rg");
Action("clean", () => RemoveFiles(
    Path.Combine(testProjectDir, "TestResults"),
    Path.Combine(testProjectDir, "coverage.opencover.xml"),
    Path.Combine(solutionDir, "jscpd-report.html"),
    Path.Combine(solutionDir, "jscpd-report.xml"),
    Path.Combine(pathToTools, "node_modules"),
    Path.Combine(webProjectFile, "bin"),
    Path.Combine(webProjectFile, "obj"),
    Path.Combine(testProjectDir, "bin"),
    Path.Combine(testProjectDir, "obj"),
    pathToPublish), "Clean the buid");
Run();