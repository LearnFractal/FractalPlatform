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
        public TestApplication(Guid sessionId,
                               BigDocInstance instance,
                               IFormFactory formFactory) : base(sessionId,
                                                               instance,
                                                               formFactory,
                                                               "Test")
        {
        }

        public override void OnStart(Context context)
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm();
        }

        public override bool OnEventDimension(Context context, EventInfo eventInfo)
        {
            Client.SetDefaultCollection(eventInfo.Action)
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();

            return true;
        }
    }
}
