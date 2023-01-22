using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.ToDoWithCategories
{
    public class ToDoWithCategoriesApplication : BaseApplication
    {
        public ToDoWithCategoriesApplication(Guid sessionId,
                                        BigDocInstance instance,
                                        IFormFactory formFactory) : base(sessionId,
                                                                        instance,
                                                                        formFactory,
                                                                        "ToDoWithCategories")
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
