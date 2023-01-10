using System;
using System.Collections.Generic;
using System.Linq;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;
using BigDoc.Database.Storages;

namespace FractalPlatform.Examples.Applications.OnlineShop
{
    public class OnlineShopApplication : BaseApplication
    {
        public OnlineShopApplication(Guid sessionId,
                                     BigDocInstance instance,
                                     IFormFactory formFactory) : base(sessionId,
                                                                      instance,
                                                                      formFactory,
                                                                      "OnlineShop")
        {
        }

        public override bool OnEventDimension(Context context, EventInfo eventInfo)
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
                        OpenCategory(context, eventInfo.AttrValue.ToString());

                        return true;
                    }
                case "Open":
                    {
                        var name = eventInfo.Collection
                                            .GetWhere(context, eventInfo.AttrPath.Parent)
                                            .Value("{'Products':[{'Name':$}]}");

                        Client.SetDefaultCollection("Products")
                              .GetWhere("{'Name':@Name}", name)
                              .ExtendDimension(DimensionType.UI, "{'ReadOnly':true}")
                              .OpenForm();

                        return true;
                    }
                case "Search":
                    {
                        var searchText = eventInfo.Collection
                                                  .GetFirstDoc(context)
                                                  .Value("{'Header':{'SearchText':$}}");

                        var categories = Client.SetDefaultCollection("Products")
                                               .GetWhere("{'Name':@SearchText}", searchText)
                                               .ToStorage("{'Categories':[$]}")
                                               .ToAttrList()
                                               .Select(x => x.Value.ToString())
                                               .Distinct()
                                               .Select(x => new { Category = x })
                                               .ToStorage(context);

                        var products = Client.SetDefaultCollection("Products")
                                             .GetWhere("{'Name':@Name}", searchText)
                                             .ToStorage();
                    
                        eventInfo.Collection
                                 .DeleteByParent(context, "Filters")
                                 .DeleteByParent(context, "Products")
                                 .MergeAsArray(context, categories, "Filters")
                                 .MergeAsArray(context, products, "Products")
                                 .OpenForm(context);

                        return true;
                    }
                default:
                    return base.OnEventDimension(context, eventInfo);
            }
        }

        public override List<string> OnEnumDimension(Context context, EnumInfo enumInfo)
        {
            if (enumInfo.Variable == "Categories")
            {
                return Client.SetDefaultCollection("Categories")
                             .GetAll()
                             .Values("{'Name':$}");
            }
            else
            {
                return base.OnEnumDimension(context, enumInfo);
            }
        }

        public void Filter(FormResult result)
        {
            if (result.Result)
            {
                var category = result.Collection
                                     .GetFirstDoc(Context)
                                     .Value("{'Header':{'Category':$}}");

                var filters = result.Collection
                                     .GetWhere(Context, "{'Filters':[{'Checked':true}]}")
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
                      .DeleteByParent(Context, "Products")
                      .MergeAsArray(Context, products, "Products")
                      .OpenForm(Context, Filter);
            }
        }

        public void OpenCategory(Context context, string category)
        {
            var collection = Client.SetDefaultCollection("Dashboard")
                                   .GetFirstDoc()
                                   .ToCollection();

            collection.GetFirstDoc(context)
                      .Update("{'Header':{'Category':@Category}}", category);

            var filter = Client.SetDefaultCollection("Categories")
                               .GetWhere("{'Name':@Category}", category)
                               .ToStorage("{'Filters':[$]}", true);

            collection.MergeAsDoc(context, filter)
                      .OpenForm(Context, Filter);
        }

        public override void OnStart(Context context)
        {
            OpenCategory(context, "Cars");
        }

        public override BaseRenderForm CreateRenderForm(string formName)
        {
            return new RenderForm(this);
        }
    }
}