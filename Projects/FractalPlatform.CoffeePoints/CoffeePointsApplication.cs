using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine.Query;

namespace FractalPlatform.CoffeePoints
{
    public class CoffeePointsApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Find":
                    FirstDocOf("Find").OpenForm(result =>
                    {
                        if (result.Result)
                        {
                            Dashboard(result.Collection.DocumentStorage);
                        }
                    });
                    return true;
                case "Propose":
                    CreateNewDocFor("NewPropose", "Proposes")
                        .OpenForm(result =>
                        {
                            if (result.Result)
                            {
                                MessageBox("Thank you, we added your proposition.",
                                       "Information",
                                       MessageBoxButtonType.Ok,
                                       result => Dashboard());
                            }
                        });
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override bool OnOpenForm(FormInfo formInfo)
        {
            if (formInfo.AttrPath.FirstPath == "Proposes")
            {
                var dateAndLatAndLng = formInfo.Collection
                                             .GetWhere(formInfo.AttrPath)
                                             .Values("{'Proposes':[{'OnDate':$,'Map':{'Point':{'Lat':$,'Lng':$}}}]}");

                DocsWhere("Proposes", "{'OnDate':@OnDate}", dateAndLatAndLng[0])
                    .ExtendDocument(DQL("{'Map':{'Center':{'Lat':@Lat,'Lng':@Lng},'Points':[{'Lat':@Lat,'Lng':@Lng}]}}", dateAndLatAndLng[1], dateAndLatAndLng[2]))
                    .OpenForm();

                return false;
            }

            return base.OnOpenForm(formInfo);
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            if (computedInfo.AttrPath.LastPath == "Gender")
            {
                return computedInfo.AttrValue.ToString() == "Male" ? "men" : "women";
            }
            else if (computedInfo.AttrValue.IsBoolean)
            {
                return computedInfo.AttrValue.GetBoolValue() ? "" : "style='display:none'";
            }

            return base.OnComputedDimension(computedInfo);
        }

        private void Dashboard(Storage filter = null)
        {
            CloseIfOpenedForm("Dashboard");

            Storage friends;
            Storage points;

            BaseQuery query;

            var minAge = 0;
            var maxAge = 100;

            if (filter != null)
            {
                var where = filter.ToAttrList().Where(x => x.Key.FirstPath != "Gender" &&
                                                           x.Value.GetBoolValue()).ToJson();
                query = DocsWhereFacet("Proposes", where);

                minAge = filter.GetFirstDoc(Context).IntValue("{'Age':{'Min':$}}");
                maxAge = filter.GetFirstDoc(Context).IntValue("{'Age':{'Max':$}}");

                var isMale = filter.GetFirstDoc(Context).BoolValue("{'Gender':{'Male':$}}");
                var isFemale = filter.GetFirstDoc(Context).BoolValue("{'Gender':{'Female':$}}");

                if (isMale && !isFemale)
                {
                    query = query.AndWhere(@"{'Contacts':{'Gender':'Male'}}");
                }
                else if (!isMale && isFemale)
                {
                    query = query.AndWhere(@"{'Contacts':{'Gender':'Female'}}");
                }
            }
            else
            {
                query = DocsOf("Proposes");
            }

            //query = query.AndWhere(@"{'OnDate':Range(@StartDate,@EndDate),
            //                          'Contacts':{'Age':Range(@MinAge,@MaxAge)}}",
            //                            GetNowDate().AddDays(-7), GetNowDate(),
            //                            minAge, maxAge);

            query = query.AndWhere(@"{'Contacts':{'Age':Range(@MinAge,@MaxAge)}}",
                                       minAge, maxAge);

            friends = query.ToStorage();
            points = query.ToStorage("{'Map':{'Point':!{'Lng':$,'Lat':$}}}");

            FirstDocOf("Dashboard")
            .ToCollection()
            .DeleteByParent("Proposes")
            .MergeToArrayPath(points, new AttrPath("Map", "Points"))
            .IfTrue(filter != null, x => x.MergeToArrayPath(friends, "Proposes"), x => x)
            .OpenForm();
        }

        public override void OnStart()
        {
            Dashboard();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
