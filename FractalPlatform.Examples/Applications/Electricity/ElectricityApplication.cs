using BigDoc.Client.App;
using BigDoc.Client.UI;

namespace FractalPlatform.Examples.Applications.Electricity
{
    public class ElectricityApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Hosts")
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }
    }
}
