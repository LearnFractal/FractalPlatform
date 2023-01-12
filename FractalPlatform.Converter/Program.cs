using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;

namespace FractalPlatform.Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            var threads = 16;
            var count = 1000;

            for (int i = 0; i < threads; i++)
            {
                ThreadPool.QueueUserWorkItem(async x =>
                {
                    var sum = 0;

                    var watch = new Stopwatch();

                    var client = new HttpClient();

                    watch.Start();

                    for (int i = 0; i < count; i++)
                    {
                        sum += (await client.GetStringAsync("https://localhost:44319/?appName=Test")).Length;
                    }

                    watch.Stop();

                    Console.WriteLine((count / watch.Elapsed.Seconds * threads).ToString() + "ops");

                    Console.WriteLine(sum);
                });
            }

            Console.ReadLine();

            //var appSettings = File.ReadAllText("appsettings.json");

            //var options = JsonConvert.DeserializeObject<Options>(appSettings);

            //if (options.Direction == "TablesToCollections")
            //{
            //    Console.WriteLine($"Start converting tables to collections for {options.TablesToCollections.AppName} application ...");

            //    var repo = new BaseRepository(options.TablesToCollections.ConnectionString);

            //    var converter = new TablesToCollectionsConverter(repo,
            //                                                     options.TablesToCollections.AppName,
            //                                                     options.TablesToCollections.Schemas,
            //                                                     options.TablesToCollections.RecommendCollections);

            //    converter.Run();
            //}
            //else if(options.Direction == "CollectionsToTables")
            //{
            //    Console.WriteLine($"Start converting collections to tables for {options.CollectionsToTables.AppName} application ...");

            //    var repo = new BaseRepository(options.CollectionsToTables.ConnectionString);

            //    var converter = new CollectionsToTablesConverter(repo,
            //                                                     options.CollectionsToTables.AppName,
            //                                                     options.CollectionsToTables.Schema,
            //                                                     options.CollectionsToTables.IsAddConstraints);

            //    converter.Run();
            //}
            //else
            //{
            //    throw new InvalidOperationException($"Incorrect '{options.Direction}' direction");
            //}
        }
    }
}
