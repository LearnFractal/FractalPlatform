using BigDoc.Client.App;
using BigDoc.Client.UI;

namespace FractalPlatform.Examples.Applications.ToDo
{
    public class ToDoApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("ToDoList")
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }
    }
}
