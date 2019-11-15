#load "common.csx"

using System.IO;
using System.IO.Compression;
using System.Net;

public static partial class MM
{
    public static void UploadFolderAsZip(string url, string pathToFolder, string username = null, string password = null, string zipName = null)
    {
        if (zipName == null)
        {
            zipName = pathToFolder + ".zip";
        }

        WriteLine("Archiving " + pathToFolder, LogLevel.Verbose);
        ZipFile.CreateFromDirectory(pathToFolder, zipName, CompressionLevel.Fastest, true);
        try
        {
            using (WebClient client = new WebClient())
            {
                if (username != null && password != null)
                {
                    WriteLine($"Set credentials for user {username}", LogLevel.Debug);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username,password);
                }

                WriteLine($"Uploading {zipName}", LogLevel.Verbose);
                client.UploadFile(url, zipName);
            }
        }
        finally
        {
            if (File.Exists(zipName))
            {
                WriteLine($"Remove {zipName}", LogLevel.Verbose);
                File.Delete(zipName);
            }
        }
    }
}