using System.Collections.Generic;

namespace FractalPlatform.Converter
{
    public class TablesToCollections
    {
        public string ConnectionString { get; set; }

        public string AppName { get; set; }

        public List<string> Schemas { get; set; }

        public List<string> RecommendCollections { get; set; }
    }

    public class CollectionsToTables
    {
        public string ConnectionString { get; set; }

        public string Schema { get; set; }

        public string AppName { get; set; }

        public bool IsAddConstraints { get; set; }
    }

    public class Options
    {
        public TablesToCollections TablesToCollections { get; set; }

        public CollectionsToTables CollectionsToTables { get; set; }

        public string Direction { get; set; }
    }
}
