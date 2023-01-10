using BigDoc.Client;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;
using BigDoc.Database.Storages;
using System;
using System.ComponentModel;

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

        public override bool OnEventDimension(Context context, Collection collection, KeyMap key, AttrValue attrValue, uint docID, string eventType, string action)
        {
            Client.SetDefaultCollection(action)
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();

            return true;
        }
    }
}
