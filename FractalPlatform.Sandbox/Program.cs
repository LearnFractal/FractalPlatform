using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using FractalPlatform.Client.App;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Dimensions.Timer;
using FractalPlatform.Database.Engine;
using Newtonsoft.Json;

namespace FractalPlatform.Sandbox
{
    internal class Program
    {
        public static bool NeedRestartApp { get; set; } = false;
        
        static string FindAssemblyPath(string assemblyName)
        {
            var solutionPath = Utils.GetSolutionPath();

            var path = @$"{solutionPath}\{assemblyName}";

            if (Directory.Exists(path))
            {
                return path;
            }

            path = @$"{solutionPath}\Projects\{assemblyName}";

            if (Directory.Exists(path))
            {
                return path;
            }

            return null;
        }

        static string GetAssemblyName(string assemblyFile) => assemblyFile.Replace(".dll", "");

        static string FindResourcePath(string assemblyName, string appName)
        {
            var solutionPath = Utils.GetSolutionPath();

            if (assemblyName.Contains(appName))
            {
                var path = @$"{solutionPath}\{assemblyName}";

                if (Directory.Exists(path))
                {
                    return path;
                }

                path = @$"{solutionPath}\Projects\{assemblyName}";

                if (Directory.Exists(path))
                {
                    return path;
                }

                return null;
            }
            else //Examples format dll
            {
                var path = @$"{solutionPath}\{assemblyName}\Applications\{appName}";

                if (Directory.Exists(path))
                {
                    return path;
                }

                path = @$"..\{solutionPath}\{assemblyName}\Applications\{appName}";

                if (Directory.Exists(path))
                {
                    return path;
                }

                return null;
            }
        }

        static bool IsAssemblyHasApp(string assemblyFile, string appName)
        {
            var assemblyName = GetAssemblyName(assemblyFile);

            return FindResourcePath(assemblyName, appName) != null;
        }

        static void RunTimers(BaseApplication app)
        {
            var dimensions = new List<TimerDimension>();

            foreach (var db in app.Instance.Databases)
            {
                foreach (var coll in db.Collections.Where(x => x.HasDimension(DimensionType.Timer)))
                {
                    var dimension = (TimerDimension)coll.GetDimension(DimensionType.Timer);

                    dimensions.Add(dimension);
                }
            }

            if (dimensions.Any())
            {
                ThreadPool.QueueUserWorkItem(x =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));

                    while (app.Context.FormFactory.HasForms)
                    {
                        foreach (var dimension in dimensions)
                        {
                            dimension.DoCalls(app.Context);
                        }

                        Thread.Sleep(TimeSpan.FromSeconds(30));
                    }
                });
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;

                var appSettings = File.ReadAllText("appsettings.json");

                var options = JsonConvert.DeserializeObject<Options>(appSettings);

                var appName = options.AppName;

                var solutionPath = Utils.GetSolutionPath();

                var assemblyFile = options.Assemblies.FirstOrDefault(assembly => IsAssemblyHasApp(assembly, options.AppName));

                if (assemblyFile == null)
                {
                    throw new InvalidOperationException($"'{appName}' application is not found in assemblies.");
                }

                var assemblyName = GetAssemblyName(assemblyFile);

#if DEBUG
                var filePath = @$"{FindAssemblyPath(assemblyName)}\bin\Debug\net8.0\{assemblyFile}";
#else
            var filePath = @$"{FindAssemblyPath(assemblyName)}\bin\Release\net8.0\{assemblyFile}";
#endif

                var assembly = Assembly.LoadFrom(filePath);

                Type type;
                string workingFolder;
                string dbPath;
                string dbName;

                if (assemblyName == "FractalPlatform.Examples")
                {
                    type = assembly.GetType($"{assemblyName}.Applications.{appName}.{appName}Application");
                    workingFolder = @$"{solutionPath}\{assemblyName}\Databases";
                    dbName = appName.Replace("Application", "");
                    dbPath = @$"{workingFolder}\{dbName}";
                }
                else //Projects folder
                {
                    type = assembly.GetType($"{assemblyName}.{appName}Application");
                    workingFolder = @$"{solutionPath}\Projects\{assemblyName}";
                    dbName = "Database";
                    dbPath = workingFolder;
                }

                Context.BasePath = @$"{solutionPath}\{assemblyName}";

                while (true)
                {
                    var sessionID = Guid.NewGuid();

                    var formFactory = new WinFormsFactory();

                    var app = (BaseApplication)Activator.CreateInstance(type);

                    Console.WriteLine($"Init {options.AppName} application ...");

                    app.Init(sessionID,
                             workingFolder,
                             null,
                             null,
                             formFactory,
                             Directory.Exists(dbPath) ? dbName : null);

                    RunTimers(app);

                    app.Context.UrlTag = options.UrlTag;

                    if (!NeedRestartApp)
                    {
                        Console.WriteLine($"Start {options.AppName} application ...");
                    }
                    else
                    {
                        Console.WriteLine($"Restart {options.AppName} application ...");
                    }

                    app.Start();

                    Console.WriteLine("Application closed.");

                    if (!NeedRestartApp)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ResetColor();
        }
    }
}
