using Newtonsoft.Json;
using System;
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

                    var url = $"{baseUrl}/Home/UploadFile?appName={appName}&fileType={fileType}&deploymentKey={deploymentKey}&deploymentParams={deploymentParams}";

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
                }

                //upload assembly
#if DEBUG
                var filePath = @$"{_deploymentPath}\{assemblyName}\bin\Debug\netcoreapp3.1\{assemblyFile}";
#else
            var filePath = @$"{_deploymentPath}\BigDoc.App\bin\Release\netcoreapp3.1\{assemblyName}";
#endif

                if (File.Exists(filePath))
                {
                    var fileBytes = await File.ReadAllBytesAsync(filePath);

                    await UploadAsync(baseUrl, appName, "Assembly", assemblyFile, fileBytes, deploymentKey);
                }
            }
        }

        static void Main(string[] args)
        {
            var appSettings = File.ReadAllText("appsettings.json");

            var options = JsonConvert.DeserializeObject<Options>(appSettings);

            Console.WriteLine($"Start deploying {options.AppName} application to {options.BaseUrl} host ...");

            var assembly = options.Assemblies.FirstOrDefault(assembly => IsAssemblyHasApp(assembly, options.AppName));

            if(assembly == null)
            {
                throw new InvalidOperationException($"'{options.AppName}' application is not found in assemblies.");
            }

            UploadAsync(options.BaseUrl,
                        options.AppName,
                        assembly,
                        options.DeploymentKey,
                        options.IsDeployDatabase,
                        options.IsRecreateDatabase,
                        options.IsDeployFiles,
                        options.IsDeployApplication).Wait();
            
            Console.WriteLine("Application is deployed !");

            if (options.IsRunBrowser)
            {
                var url = string.Format($"{options.BaseUrl}?appName={options.AppName}");

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }
}
