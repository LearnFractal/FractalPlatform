using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System.Linq;

namespace FractalPlatform.Examples.Applications.MeetMeMan
{
    public class MeetMeManApplication : BaseApplication
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

            if (filter != null)
            {
                var where = filter.ToAttrList().Where(x => x.Value.GetBoolValue()).ToJson();
                friends = DocsWhereFacet("Proposes", where).ToStorage();
                points = DocsWhereFacet("Proposes", where).ToStorage("{'Map':{'Point':!{'Lng':$,'Lat':$}}}");
            }
            else
            {
                friends = DocsOf("Proposes").ToStorage();
                points = DocsOf("Proposes").ToStorage("{'Map':{'Point':!{'Lng':$,'Lat':$}}}");
            }

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
