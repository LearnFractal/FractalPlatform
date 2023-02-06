using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;
using BigDoc.Database.Storages;
using System;

namespace FractalPlatform.Examples.Applications.Test
{
    public class TestApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm();
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            Client.SetDefaultCollection(eventInfo.Action)
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();

            return true;
        }
    }
}
