using System.Collections.Generic;
using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.OnlineShop
{
    public class OnlineShopApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo info)
        {
            switch (info.Action)
            {
                case "Categories":
                    {
                        DocsOf("Categories")
                              .OpenForm();

                        return true;
                    }
                case "Products":
                    {
                        DocsOf("Products")
                              .OpenForm();

                        return true;
                    }
                case "NewCategory":
                    {
                        CreateNewDocFor("NewCategory", "Categories")
                              .OpenForm();

                        return true;
                    }
                case "NewProduct":
                    {
                        CreateNewDocFor("NewProduct", "Products")
                              .OpenForm();

                        return true;
                    }
                case "Category":
                    {
                        OpenCategory(info.AttrValue.ToString());

                        return true;
                    }
                case "OpenButton":
                    {
                        var name = info.Collection
                                            .GetWhere(info.AttrPath.Parent)
                                            .Value("{'Products':[{'Name':$}]}");

                        var categories = info.Collection
                                            .GetWhere(info.AttrPath.Parent)
                                            .Values("{'Products':[{'Categories':[$]}]}");

                        DocsWhere("Products", "{'Name':@Name,'Categories':[In,@Categories]}", name, categories)
                              .ExtendUIDimension("{'ReadOnly':true}")
                              .OpenForm();

                        return true;
                    }
                case "SearchButton":
                    {
                        var searchText = info.Collection
                                                  .GetFirstDoc()
                                                  .Value("{'Header':{'SearchText':$}}");

                        var categories = DocsWhere("Products", "{'Name':@SearchText}", searchText)
                                               .ToStorage("{'Categories':[$]}")
                                               .ToAttrList()
                                               .Select(x => x.Value.ToString())
                                               .Distinct()
                                               .Select(x => new { Category = x });

                        var products = DocsWhere("Products", "{'Name':@Name}", searchText).ToStorage();

                        info.Collection
                                 .DeleteByParent("Filters")
                                 .DeleteByParent("Products")
                                 .MergeToArrayPath(categories, "Filters")
                                 .MergeToArrayPath(products, "Products")
                                 .OpenForm();

                        return true;
                    }
                default:
                    return base.OnEventDimension(info);
            }
        }

        public override List<string> OnEnumDimension(EnumInfo info)
        {
            if (info.Variable == "Categories")
            {
                return DocsOf("Categories")
                             .Values("{'Name':$}");
            }
            else
            {
                return base.OnEnumDimension(info);
            }
        }

        public void Filter(FormResult result)
        {
            if (result.Result)
            {
                var category = result.Collection
                                     .GetFirstDoc()
                                     .Value("{'Header':{'Category':$}}");

                var filters = result.Collection
                                     .GetWhere("{'Filters':[{'Checked':true}]}")
                                     .Values("{'Filters':[{'Query':$}]}");

                Storage products;

                if (filters.Count > 0)
                {
                    var orQuery = DocsWhere("Products", filters[0]);

                    for (int i = 1; i < filters.Count; i++)
                    {
                        orQuery.OrWhere(filters[i]);
                    }

                    products = DocsWhere("Products", "{'Categories':[Any,@Category]}", category)
                                     .AndWhere(orQuery)
                                     .ToStorage();
                }
                else
                {
                    products = DocsWhere("Products", "{'Categories':[Any,@Category]}", category)
                                     .ToStorage();
                }

                result.Collection
                      .DeleteByParent("Products")
                      .MergeToArrayPath(products, "Products")
                      .OpenForm(Filter);
            }
        }

        public void OpenCategory(string category)
        {
            var collection = FirstDocOf("Dashboard")
                                   .ToCollection();

            collection.GetFirstDoc()
                      .Update("{'Header':{'Category':@Category}}", category);

            var filter = DocsWhere("Categories", "{'Name':@Category}", category)
                               .ToStorage("{'Filters':[$]}");

            collection.MergeToPath(filter)
                      .OpenForm(Filter);
        }

        public override void OnStart() => OpenCategory("Cars");
        
        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}