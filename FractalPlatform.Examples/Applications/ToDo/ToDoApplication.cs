using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.ToDo
{
    public class ToDoApplication : BaseApplication
    {
        public ToDoApplication(Guid sessionId,
                               BigDocInstance instance,
                               IFormFactory formFactory) : base(sessionId,
                                                               instance,
                                                               formFactory,
                                                               "ToDo")
        {
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("ToDoList")
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }
    }
}
