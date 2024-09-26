using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FractalPlatform.Examples.Applications.Filament
{
    public class FilamentApplication : BaseApplication
    {
        public override void OnStart() =>
            DocsOf("Shops").OpenForm(result => {
                var attrs = result.Collection.ToAttrList();
                var shops = attrs.Where(x => x.Key.FirstPath == "Shop" && x.Value.GetBoolValue()).Select(x => x.Key.LastPath);
                var colors = attrs.Where(x => x.Key.FirstPath == "Color" && x.Value.GetBoolValue()).Select(x => x.Key.LastPath);
                var products = Regex.Matches(REST.Get("https://filament.in.ua"), "(?<name>[a-zA-Zа-яА-Яі\\-\\s0-9]+[0-9].[0-9]{1,2} кг.)")
                                 .Where(x => shops.Any(y => x.Value.Contains(y)) && (colors.Any(y => x.Value.Contains(y)) || colors.Contains("Всі")))
                                 .Select(x => new { Name = x.Value, Price = $"{new Random().Next(100, 700)} грн", Link = "https://shop.com" }).ToList();
                new
                {
                    Count = $"Товарів: {products.Count} штук",
                    Products = products
                }
                .ToCollection(Constants.FIRST_DOC_ID)
                .SetUIDimension("{'Style':'Hide:Link','ReadOnly':true,'Count':{'ControlType':'Label'},'Names':[{'Link':{'ControlType':'Link'}}]}")
                .SetDimension(DimensionType.Pagination, "{'Products':{'Page':{'Size':30}}}")
                .SetDimension(DimensionType.Sort)
                .SetDimension(DimensionType.Filter)
                .OpenForm();
            });
    }
}
