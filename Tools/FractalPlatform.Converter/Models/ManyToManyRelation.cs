using System.Data;

namespace FractalPlatform.Converter.Models
{
    public class ManyToManyRelation
    {
        public DataTable ManyToManyTable { get; set; }

        public Table RelatedToTable { get; set; }
    }
}
