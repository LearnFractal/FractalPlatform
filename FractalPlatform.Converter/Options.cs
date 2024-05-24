using System.Collections.Generic;

namespace FractalPlatform.Converter
{
    public class Options
    {
        public string ConnectionString { get; set; }

        public string AppName { get; set; }

        public List<string> Schemas { get; set; }

        public List<string> RecommendCollections { get; set; }
    }
}
