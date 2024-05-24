using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Storages;
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
