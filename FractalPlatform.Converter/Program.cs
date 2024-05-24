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

            Console.WriteLine($"Start converting tables to collections for {options.AppName} application ...");

            var repo = new BaseRepository(options.ConnectionString);

            var converter = new TablesToCollectionsConverter(repo,
                                                             options.AppName,
                                                             options.Schemas,
                                                             options.RecommendCollections);

            converter.Run();
        }
    }
}
