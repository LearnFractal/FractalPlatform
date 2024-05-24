namespace FractalPlatform.Converter.Models
{
    public class Parent
    {
        public Table Table { get; set; }

        public string RefColumnName { get; set; }

        public override string ToString()
        {
            return $"Name:{Table.FullName}, ColumnName:{RefColumnName}";
        }
    }
}
