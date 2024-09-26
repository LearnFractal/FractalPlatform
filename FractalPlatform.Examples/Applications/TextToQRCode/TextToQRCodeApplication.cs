using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.TextToQRCode
{
    public class TextToQRCodeApplication : BaseApplication
    {
        public override void OnStart() =>
            FirstDocOf("Dashboard")
                  .OpenForm(result =>
                  {
                      new
                      {
                          QRCode = result.Collection
                                         .GetFirstDoc()
                                         .Value("{'Text':$}")
                      }
                      .ToCollection(string.Empty)
                      .SetUIDimension("{'QRCode':{'ControlType':'Picture','Style':'Save:false;Type:QRCode'}}")
                      .OpenForm();
                  });
    }
}