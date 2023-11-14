using FractalPlatform.Client;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine.Query;
using System.Linq;

namespace FractalPlatform.Examples.Applications.CoffeePoints
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
                            Dashboard();
                        });
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        private void Dashboard(Storage filter = null)
        {
            CloseIfOpenedForm("Dashboard");

            Storage friends;
            Storage points;

            BaseQuery query;
            
            if (filter != null)
            {
                var where = filter.ToAttrList().Where(x => x.Value.GetBoolValue()).ToJson();
                query = DocsWhereFacet("Proposes", where);
            }
            else
            {
                query = DocsOf("Proposes");
            }

            query = query.AndWhere("{'OnDate':Range(@StartDate,@EndDate)}", GetNowDate(), GetNowDate().AddDays(7));

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
