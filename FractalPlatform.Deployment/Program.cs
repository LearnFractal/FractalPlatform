using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace FractalPlatform.Deployment
{
    class Program
    {
        const string _deploymentPath = @"..\..\..\..";

        static string GetAssemblyName(string assemblyFile) => assemblyFile.Replace(".dll", "");

        static bool IsAssemblyHasApp(string assemblyFile, string appName)
        {
            var assemblyName = GetAssemblyName(assemblyFile);

            string startPath = @$"{_deploymentPath}\{assemblyName}\Applications\{appName}";

            return Directory.Exists(startPath);
        }

        static string ZipDirectory(string directoryName, string assemblyName, string appName)
        {
            string startPath = @$"{_deploymentPath}\{assemblyName}\{directoryName}\{appName}";

            if (Directory.Exists(startPath))
            {
                string zipPath = @$"{_deploymentPath}\{Assembly.GetExecutingAssembly().GetName().Name}\{directoryName}_{appName}.zip";

                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                ZipFile.CreateFromDirectory(startPath, zipPath);

                return zipPath;
            }

            return null;
        }

        private static async Task UploadAsync(string baseUrl,
                                         string appName,
                                         string fileType,
                                         string fileName,
                                         byte[] fileBytes,
                                         string deploymentKey,
                                         string deploymentParams = null)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(fileBytes)), "upload", fileName);

                    var url = $"{baseUrl}/{appName}/UploadFile/?fileType={fileType}&deploymentKey={deploymentKey}&deploymentParams={deploymentParams}";

                    var response = await client.PostAsync(url, content);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        var error = await response.Content.ReadAsStringAsync();

                        throw new Exception(error);
                    }
                }
            }
        }

        private static async Task UploadAsync(string baseUrl,
                                              string appName,
                                              string assemblyFile,
                                              string deploymentKey,
                                              bool isDeployDatabase,
                                              bool isRecreateDatabase,
                                              bool isDeployFiles,
                                              bool isDeployApplication)
        {
            var assemblyName = GetAssemblyName(assemblyFile);

            if (isDeployDatabase)
            {
                //upload database
                var zipPath = ZipDirectory("Databases", assemblyName, appName);

                if (zipPath != null)
                {
                    var fileBytes = await File.ReadAllBytesAsync(zipPath);

                    await UploadAsync(baseUrl,
                                      appName,
                                      "Databases",
                                      $"{appName}.zip",
                                      fileBytes,
                                      deploymentKey,
                                      isRecreateDatabase.ToString());

                    File.Delete(zipPath);
                }
            }

            if (isDeployFiles)
            {
                //upload database
                var zipPath = ZipDirectory("Files", assemblyName, appName);

                if (zipPath != null)
                {
                    var fileBytes = await File.ReadAllBytesAsync(zipPath);

                    await UploadAsync(baseUrl,
                                      appName,
                                      "Files",
                                      $"{appName}.zip",
                                      fileBytes,
                                      deploymentKey);

                    File.Delete(zipPath);
                }
            }

            if (isDeployApplication)
            {
                //upload layouts
                var zipPath = ZipDirectory("Layouts", assemblyName, appName);

                if (zipPath != null)
                {
                    var fileBytes = await File.ReadAllBytesAsync(zipPath);

                    await UploadAsync(baseUrl, appName, "Layouts", $"{appName}.zip", fileBytes, deploymentKey);

                    File.Delete(zipPath);
                }

                //upload assembly
#if DEBUG
                var filePath = @$"{_deploymentPath}\{assemblyName}\bin\Debug\netcoreapp3.1\{assemblyFile}";
#else
            var filePath = @$"{_deploymentPath}\FractalPlatform.App\bin\Release\netcoreapp3.1\{assemblyName}";
#endif

                if (File.Exists(filePath))
                {
                    var fileBytes = await File.ReadAllBytesAsync(filePath);

                    await UploadAsync(baseUrl, appName, "Assembly", assemblyFile, fileBytes, deploymentKey);
                }
            }
        }

        static void Deploy(Options options, string appName)
        {
            Console.WriteLine($"Start deploying {appName} application to {options.BaseUrl} host ...");

            var assembly = options.Assemblies.FirstOrDefault(assembly => IsAssemblyHasApp(assembly, appName));

            if (assembly == null)
            {
                throw new InvalidOperationException($"'{appName}' application is not found in assemblies.");
            }

            UploadAsync(options.BaseUrl,
                        appName,
                        assembly,
                        options.DeploymentKey,
                        options.IsDeployDatabase,
                        options.IsRecreateDatabase,
                        options.IsDeployFiles,
                        options.IsDeployApplication).Wait();

            Console.WriteLine($"{appName} application is deployed !");

            if (options.IsRunBrowser)
            {
                var url = string.Format($"{options.BaseUrl}/{appName}");

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }

        static void Main(string[] args)
        {
            var appSettings = File.ReadAllText("appsettings.json");

            var options = JsonConvert.DeserializeObject<Options>(appSettings);

            if(options.AppNames == null)
            {
                var assemblyName = GetAssemblyName(options.Assemblies[0]);

                var filePath = @$"{_deploymentPath}\{assemblyName}\bin\Debug\netcoreapp3.1\{options.Assemblies[0]}";

                var bytes = File.ReadAllBytes(filePath);

                var assembly = Assembly.Load(bytes);

                options.AppNames = new List<string>();

                foreach(var type in assembly.GetTypes().Where(x => x.Name.EndsWith("Application")))
                {
                    var appName = type.Name.Replace("Application", "");

                    if (options.ExcludeAppNames == null ||
                        !options.ExcludeAppNames.Contains(appName))
                    {
                        options.AppNames.Add(appName);
                    }
                }

                options.IsRunBrowser = false;
            }

            Console.WriteLine($"Start deploying {options.AppNames.Count} applications ...");

            foreach (var appName in options.AppNames)
            {
                Deploy(options, appName);
            }

            Console.WriteLine("All applications are deployed !");
        }
    }
}
