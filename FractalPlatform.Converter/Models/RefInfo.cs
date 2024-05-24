namespace FractalPlatform.Converter.Models
{
    public class RefInfo
    {
        public Table Table { get; set; }

        public string Id { get; set; }

        public string RefcolumnName { get; set; }

        public bool IsReferencedTo(Table table)
        {
            return (Table == null || table.Name == Table.Name);
        }

        public bool IsReferencedTo(string id, Table table)
        {
            return Id == id && IsReferencedTo(table);
        }
    }
}
