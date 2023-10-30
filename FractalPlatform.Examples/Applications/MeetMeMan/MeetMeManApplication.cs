using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using System.Linq;

namespace FractalPlatform.Examples.Applications.Sandbox5
{
    public class MeetMeManApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Find":
                    FirstDocOf("Find").OpenForm(result => Dashboard(result.Collection.DocumentStorage));
                    return true;
                case "Propose":
                    CreateNewDocFor("NewFriend", "Friends").OpenForm(result => Dashboard());
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
                friends = DocsWhereFacet("Friends", where).ToStorage();
                points = DocsWhereFacet("Friends", where).ToStorage("{'Map':{'Point':!{'Lng':$,'Lat':$}}}");
            }
            else
            {
                friends = DocsOf("Friends").ToStorage();
                points = DocsOf("Friends").ToStorage("{'Map':{'Point':!{'Lng':$,'Lat':$}}}");
            }

            FirstDocOf("Dashboard")
            .ToCollection()
            .MergeToArrayPath(points, new AttrPath("Map", "Points"))
            .IfTrue(filter != null, x => x.MergeToArrayPath(friends, "Friends"), x => x)
            .OpenForm();
        }

        public override void OnStart()
        {
            Dashboard(null);
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
