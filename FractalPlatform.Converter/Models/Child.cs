namespace FractalPlatform.Converter.Models
{
    public class Child
    {
        public Table Table { get; set; }

        public Relation Relation { get; set; }

        public override string ToString()
        {
            return $"Name:{Table.FullName}, Relation:{Relation}";
        }
    }
}
