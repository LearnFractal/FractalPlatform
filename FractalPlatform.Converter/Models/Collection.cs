namespace FractalPlatform.Converter.Models
{
    public class Collection
    {
        public Table RootTable { get; set; }

        public override string ToString()
        {
            return RootTable.FullName;
        }
    }
}
