using System.Collections.Generic;
using System.Linq;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.OnlineShop
{
    public class OnlineShopApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Categories":
                    {
                        Client.SetDefaultCollection("Categories")
                              .GetAll()
                              .OpenForm();

                        return true;
                    }
                case "Products":
                    {
                        Client.SetDefaultCollection("Products")
                              .GetAll()
                              .OpenForm();

                        return true;
                    }
                case "NewCategory":
                    {
                        Client.SetDefaultCollection("NewCategory")
                              .GetFirstDoc()
                              .WantCreateNewDocumentFor("Categories")
                              .OpenForm();

                        return true;
                    }
                case "NewProduct":
                    {
                        Client.SetDefaultCollection("NewProduct")
                              .GetFirstDoc()
                              .WantCreateNewDocumentFor("Products")
                              .OpenForm();

                        return true;
                    }
                case "Category":
                    {
                        OpenCategory(eventInfo.AttrValue.ToString());

                        return true;
                    }
                case "OpenButton":
                    {
                        var name = eventInfo.Collection
                                            .GetWhere(eventInfo.AttrPath.Parent)
                                            .Value("{'Products':[{'Name':$}]}");

                        var categories = eventInfo.Collection
                                            .GetWhere(eventInfo.AttrPath.Parent)
                                            .Values("{'Products':[{'Categories':[$]}]}");

                        Client.SetDefaultCollection("Products")
                              .GetWhere("{'Name':@Name,'Categories':[In,@Categories]}", name, categories)
                              .ExtendUIDimension("{'ReadOnly':true}")
                              .OpenForm();

                        return true;
                    }
                case "SearchButton":
                    {
                        var searchText = eventInfo.Collection
                                                  .GetFirstDoc()
                                                  .Value("{'Header':{'SearchText':$}}");

                        var categories = Client.SetDefaultCollection("Products")
                                               .GetWhere("{'Name':@SearchText}", searchText)
                                               .ToStorage("{'Categories':[$]}")
                                               .ToAttrList()
                                               .Select(x => x.Value.ToString())
                                               .Distinct()
                                               .Select(x => new { Category = x })
                                               .ToStorage();

                        var products = Client.SetDefaultCollection("Products")
                                             .GetWhere("{'Name':@Name}", searchText)
                                             .ToStorage();

                        eventInfo.Collection
                                 .DeleteByParent("Filters")
                                 .DeleteByParent("Products")
                                 .MergeAsArray(categories, "Filters")
                                 .MergeAsArray(products, "Products")
                                 .OpenForm();

                        return true;
                    }
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override List<string> OnEnumDimension(EnumInfo enumInfo)
        {
            if (enumInfo.Variable == "Categories")
            {
                return Client.SetDefaultCollection("Categories")
                             .GetAll()
                             .Values("{'Name':$}");
            }
            else
            {
                return base.OnEnumDimension(enumInfo);
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
                    var orQuery = Client.SetDefaultCollection("Products")
                                        .GetWhere(filters[0]);

                    for (int i = 1; i < filters.Count; i++)
                    {
                        orQuery.OrWhere(filters[i]);
                    }

                    products = Client.SetDefaultCollection("Products")
                                     .GetWhere("{'Categories':[Any,@Category]}", category)
                                     .AndWhere(orQuery)
                                     .ToStorage();
                }
                else
                {
                    products = Client.SetDefaultCollection("Products")
                                     .GetWhere("{'Categories':[Any,@Category]}", category)
                                     .ToStorage();
                }

                result.Collection
                      .DeleteByParent("Products")
                      .MergeAsArray(products, "Products")
                      .OpenForm(Filter);
            }
        }

        public void OpenCategory(string category)
        {
            var collection = Client.SetDefaultCollection("Dashboard")
                                   .GetFirstDoc()
                                   .ToCollection();

            collection.GetFirstDoc()
                      .Update("{'Header':{'Category':@Category}}", category);

            var filter = Client.SetDefaultCollection("Categories")
                               .GetWhere("{'Name':@Category}", category)
                               .ToStorage("{'Filters':[$]}");

            collection.MergeAsDoc(filter)
                      .OpenForm(Filter);
        }

        public override void OnStart()
        {
            OpenCategory("Cars");
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form)
        {
            return new RenderForm(this, form);
        }
    }
}