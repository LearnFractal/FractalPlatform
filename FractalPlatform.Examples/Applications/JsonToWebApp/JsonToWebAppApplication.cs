using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.JsonToWebApp
{
    public class JsonToWebAppApplication : BaseApplication
    {
        public override object OnComputedDimension(ComputedInfo info)
        {
            bool isValid = true;

            Validators.ValidateJson(info.AttrValue.ToString(), (x, y, z) => isValid = false);

            return isValid;
        }

        public override void OnStart() =>
            FirstDocOf("Dashboard")
                  .OpenForm(result =>
                  {
                      var json = result.FindFirstValue("JSON");

                      new
                      {
                          Image = "https://media.tenor.com/rsB66bq2gIgAAAAd/magic.gif"
                      }
                      .ToCollection(string.Empty)
                      .GetFirstDoc()
                      .SetUIDimension("{'Image':{'ControlType':'Picture','Style':'Save:Do Magic !;Cancel:false'}}")
                      .OpenForm(result => {
                          json.ToCollection()
                              .SetDimension(DimensionType.Theme, "{'DefaultTheme':'LightBlue'}")
                              .OpenForm(result => OnStart());
                      });
                  });
    }
}
