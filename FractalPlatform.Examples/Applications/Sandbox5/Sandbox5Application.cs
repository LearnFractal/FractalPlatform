using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.Sandbox5
{
    public class Sandbox5Application : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("ElectricityScore")
                  .GetFirstDoc()
                  .OpenForm(result => {

                      Client.SetDefaultCollection("NewElectricityScore")
                            .WantCreateNewDocumentForArray("ElectricityScore", "{'Apps':[$]}")
                            .OpenForm(result => OnStart());
                  });
        }
    }
}
