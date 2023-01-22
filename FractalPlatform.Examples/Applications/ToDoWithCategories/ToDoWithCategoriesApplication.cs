using BigDoc.Client.App;
using BigDoc.Client.UI;

namespace FractalPlatform.Examples.Applications.ToDoWithCategories
{
    public class ToDoWithCategoriesApplication : BaseApplication
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
