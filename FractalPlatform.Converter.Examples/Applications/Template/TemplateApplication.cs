using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;
using BigDoc.Database.Storages;
using System;

namespace FractalPlatform.Examples.Applications.Template
{
    public class TemplateApplication : BaseApplication
    {
        public TemplateApplication(Guid sessionId,
                               BigDocInstance instance,
                               IFormFactory formFactory) : base(sessionId,
                                                               instance,
                                                               formFactory,
                                                               "Template")
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
