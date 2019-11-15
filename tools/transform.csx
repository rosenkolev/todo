#load "common.csx"
#r "nuget:Newtonsoft.Json, 12.0.2"

using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Newtonsoft.Json.Linq;

public static partial class MM
{
    public static class Transform
    {
        public static void TransformSettingsJson(string pathToSettingsJson, string pathToTransformJson)
        {
            WriteLine("Transforming file " + pathToSettingsJson);
            var sourceJson = ReadJsonFile(pathToSettingsJson);
            var transformJson = ReadJsonFile(pathToTransformJson);
            var mergeSettigns = new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Replace };

            sourceJson.Merge(transformJson);

            var transformedContent = sourceJson.ToString();
            WriteLine("Transformed file content " + transformedContent, LogLevel.Debug);

            File.WriteAllText(pathToSettingsJson, transformedContent);
        }

        public static void TransformXmlElement(string pathToXml, string path, Func<string, string> transform)
        {
            WriteLine($"Transform '{path}' in file '{pathToXml}'.", LogLevel.Verbose);
            var xml = XElement.Load(pathToXml);
            XElement el = xml.XPathSelectElement(path);
            var value = el.Value;
            el.Value = transform(value);
            WriteLine($"Transformed '{value}' to '{el.Value}'.", LogLevel.Debug);
            xml.Save(pathToXml);
        }

        private static JObject ReadJsonFile(string pathToSettingsJson) =>
            JObject.Parse(File.ReadAllText(pathToSettingsJson));
    }
}