using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.TextToQRCode
{
    public class TextToQRCodeAppApplication : BaseApplication
    {
        public override void OnStart()
        {
            FirstDocOf("Dashboard")
                  .OpenForm(result =>
                  {
                      new
                      {
                          QRCode = result.Collection
                                         .GetFirstDoc()
                                         .Value("{'Text':$}")
                      }
                      .ToCollection(Constants.FIRST_DOC_ID, string.Empty)
                      .SetUIDimension("{'QRCode':{'ControlType':'Picture','Style':'Type:QRCode'}}")
                      .OpenForm();
                  });
        }
    }
}