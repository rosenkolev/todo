using System.Reflection;
using System.IO;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ToDoApp
{
    /// <summary>The application entry point class.</summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>Build web host settings.</summary>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .CaptureStartupErrors(true)
                .UseStartup<Startup>()
                .Build();

        /// <summary>Main application entry point method.</summary>
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();
    }
}
