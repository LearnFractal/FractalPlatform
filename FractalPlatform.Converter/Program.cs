using FractalPlatform.Database.Converter;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FractalPlatform.Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettings = File.ReadAllText("appsettings.json");

            var options = JsonConvert.DeserializeObject<Options>(appSettings);

            if (options.Direction == "TablesToCollections")
            {
                Console.WriteLine($"Start converting tables to collections for {options.TablesToCollections.AppName} application ...");

                var repo = new BaseRepository(options.TablesToCollections.ConnectionString);

                var converter = new TablesToCollectionsConverter(repo,
                                                                 options.TablesToCollections.AppName,
                                                                 options.TablesToCollections.Schemas,
                                                                 options.TablesToCollections.RecommendCollections);

                converter.Run();
            }
            else if (options.Direction == "CollectionsToTables")
            {
                Console.WriteLine($"Start converting collections to tables for {options.CollectionsToTables.AppName} application ...");

                var repo = new BaseRepository(options.CollectionsToTables.ConnectionString);

                var converter = new CollectionsToTablesConverter(repo,
                                                                 options.CollectionsToTables.AppName,
                                                                 options.CollectionsToTables.Schema,
                                                                 options.CollectionsToTables.IsAddConstraints);

                converter.Run();
            }
            else
            {
                throw new InvalidOperationException($"Incorrect '{options.Direction}' direction");
            }
        }
    }
}
