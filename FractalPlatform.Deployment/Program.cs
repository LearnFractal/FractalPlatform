﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FractalPlatform.Deployment
{
    class Program
    {
        public enum ResourceType
        {
            Assembly,
            Applications,
            Databases,
            Files,
            Layouts
        }

        const string _deploymentPath = @"..\..\..\..";

        static string GetAssemblyName(string assemblyFile) => assemblyFile.Replace(".dll", "");

        static string FindAssemblyPath(string assemblyName)
        {
            var path = @$"{_deploymentPath}\{assemblyName}";

            if (Directory.Exists(path))
            {
                return path;
            }

            path = @$"{_deploymentPath}\Projects\{assemblyName}";

            if (Directory.Exists(path))
            {
                return path;
            }

            return null;
        }

        static string FindResourcePath(ResourceType resourceType, string assemblyName, string appName)
        {
            if (assemblyName.Contains(appName))
            {
                if (resourceType == ResourceType.Applications)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"{_deploymentPath}\Projects\{assemblyName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
                else if (resourceType == ResourceType.Databases)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Database";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"{_deploymentPath}\Projects\{assemblyName}\Database";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
                else if (resourceType == ResourceType.Files)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Files";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"{_deploymentPath}\Projects\{assemblyName}\Files";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
                else //if (resourceType == ResourceType.Layouts)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Layouts";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"{_deploymentPath}\Projects\{assemblyName}\Layouts";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
            }
            else //Examples format dll
            {
                if (resourceType == ResourceType.Applications)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Applications\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"..\{_deploymentPath}\{assemblyName}\Applications\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
                else if (resourceType == ResourceType.Databases)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Databases\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"..\{_deploymentPath}\{assemblyName}\Databases\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
                else if (resourceType == ResourceType.Files)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Files\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"..\{_deploymentPath}\{assemblyName}\Files\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
                else //if (resourceType == ResourceType.Layouts)
                {
                    var path = @$"{_deploymentPath}\{assemblyName}\Layouts\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    path = @$"..\{_deploymentPath}\{assemblyName}\Layouts\{appName}";

                    if (Directory.Exists(path))
                    {
                        return path;
                    }

                    return null;
                }
            }
        }

        static bool IsAssemblyHasApp(string assemblyFile, string appName)
        {
            var assemblyName = GetAssemblyName(assemblyFile);

            return FindResourcePath(ResourceType.Applications, assemblyName, appName) != null;
        }

        static string ZipDirectory(ResourceType resourceType, string assemblyName, string appName)
        {
            string startPath = FindResourcePath(resourceType, assemblyName, appName);

            if (startPath != null)
            {
                string zipPath = @$"{_deploymentPath}\{Assembly.GetExecutingAssembly().GetName().Name}\{resourceType}_{appName}.zip";

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
                                         bool isMultithread,
                                         string deploymentParams = null)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(fileBytes)), "upload", fileName);

                    var url = $"{baseUrl}/{appName}/UploadFile/?fileType={fileType}&deploymentKey={deploymentKey}&isMultithread={isMultithread}&deploymentParams={deploymentParams}";

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
                                              bool isDeployApplication,
                                              bool isMultithread)
        {
            var assemblyName = GetAssemblyName(assemblyFile);

            if (isDeployDatabase)
            {
                //upload database
                var zipPath = ZipDirectory(ResourceType.Databases, assemblyName, appName);

                if (zipPath != null)
                {
                    var fileBytes = await File.ReadAllBytesAsync(zipPath);

                    try
                    {
                        await UploadAsync(baseUrl,
                                          appName,
                                          "Databases",
                                          $"{appName}.zip",
                                          fileBytes,
                                          deploymentKey,
                                          isMultithread,
                                          isRecreateDatabase.ToString());
                    }
                    finally
                    {
                        File.Delete(zipPath);
                    }
                }
            }

            if (isDeployFiles)
            {
                //upload database
                var zipPath = ZipDirectory(ResourceType.Files, assemblyName, appName);

                if (zipPath != null)
                {
                    var fileBytes = await File.ReadAllBytesAsync(zipPath);

                    try
                    {
                        await UploadAsync(baseUrl,
                                          appName,
                                          "Files",
                                          $"{appName}.zip",
                                          fileBytes,
                                          deploymentKey,
                                          isMultithread);
                    }
                    finally
                    {
                        File.Delete(zipPath);
                    }
                }
            }

            if (isDeployApplication)
            {
                //upload layouts
                var zipPath = ZipDirectory(ResourceType.Layouts, assemblyName, appName);

                if (zipPath != null)
                {
                    var fileBytes = await File.ReadAllBytesAsync(zipPath);

                    try
                    {
                        await UploadAsync(baseUrl,
                                          appName,
                                          "Layouts",
                                          $"{appName}.zip",
                                          fileBytes,
                                          deploymentKey,
                                          isMultithread);
                    }
                    finally
                    {
                        File.Delete(zipPath);
                    }
                }

                //upload assembly
#if DEBUG
                var filePath = @$"{FindAssemblyPath(assemblyName)}\bin\Debug\net8.0\{assemblyFile}";
#else
            var filePath = @$"{FindAssemblyPath(assemblyName)}\bin\Release\net8.0\{assemblyFile}";
#endif

                if (File.Exists(filePath))
                {
                    var fileBytes = await File.ReadAllBytesAsync(filePath);

                    await UploadAsync(baseUrl,
                                      appName,
                                      "Assembly",
                                      assemblyFile,
                                      fileBytes,
                                      deploymentKey,
                                      isMultithread);
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
                        options.IsDeployApplication,
                        options.IsMultithread).Wait();

            Console.WriteLine($"{appName} application is deployed !");

            if (options.IsRunBrowser)
            {
                var url = string.Format($"{options.BaseUrl}/{appName}");

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;

                var appSettings = File.ReadAllText("appsettings.json");

                var options = JsonConvert.DeserializeObject<Options>(appSettings);

                if (args.Length > 0)
                {
                    options.AppNames = new List<string> { args[0] };

                    if (args.Length > 1)
                    {
                        options.Assemblies = new List<string> { args[1] };

                        if (args.Length > 2)
                        {
                            options.DeploymentKey = args[2];

                            if (args.Length > 3)
                            {
                                options.BaseUrl = args[3];

                                options.IsRunBrowser = false;

                                options.IsMultithread = true;
                            }
                        }
                    }
                }

                if (options.AppNames == null)
                {
					options.AppNames = new List<string>();

					foreach (var assemblyFile in options.Assemblies)
                    {
                        var assemblyName = GetAssemblyName(assemblyFile);

#if DEBUG
                        var filePath = @$"{FindAssemblyPath(assemblyName)}\bin\Debug\net8.0\{assemblyFile}";
#else
                    var filePath = @$"{FindAssemblyPath(assemblyName)}\bin\Release\net8.0\{assemblyFile}";
#endif

                        var bytes = File.ReadAllBytes(filePath);

                        var assembly = Assembly.Load(bytes);

                        foreach (var type in assembly.GetTypes().Where(x => x.Name.EndsWith("Application")))
                        {
                            var appName = type.Name.Replace("Application", "");

                            if (options.ExcludeAppNames == null ||
                                !options.ExcludeAppNames.Contains(appName))
                            {
                                options.AppNames.Add(appName);
                            }
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }

            Console.ResetColor();
        }
    }
}
