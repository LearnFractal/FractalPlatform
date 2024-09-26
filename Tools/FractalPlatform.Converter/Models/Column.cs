using System;

namespace FractalPlatform.Converter.Models
{
    public class Column
    {
        public string Name { get;set; }

        public Type Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
