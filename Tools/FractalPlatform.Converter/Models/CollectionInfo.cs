using System;
using System.Collections.Generic;
using System.Text;

namespace FractalPlatform.Converter.Models
{
    public class CollectionInfo
    {
        public List<string> Documents { get; } = new List<string>();

        public List<string> LastAccessDocuments { get; } = new List<string>();

        public string LastAccess { get; set; }

        public string Enum { get; set; }

        public string UI { get; set; }

        public string Validation { get; set; }

        public string Relation { get; set; }

        public bool HasEnum => Enum?.Length > 2; //Skip empty jsons: {}

        public bool HasUI => UI?.Length > 2;

        public bool HasValidation => Validation?.Length > 2;

        public bool HasRelation => Relation?.Length > 2;

        public bool HasLastAccess => LastAccess?.Length > 2;
    }
}
