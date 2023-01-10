using FractalPlatform.Converter.Models;
using System.Collections.Generic;
using System.Linq;

namespace FractalPlatform.Converter
{
    public static class Extensions
    {
        public static Table GetTable(this List<Table> tables, string tableName) => tables.FirstOrDefault(x => x.FullName == tableName);

        public static bool Contains(this List<Column> columns, string columnName) => columns.Any(x => x.Name == columnName);
    }
}
