using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.BTCRate
{
    public class BTCRateApplication : BaseApplication
    {
        private void Rate()
        {
            var query = REST.Get("https://api.coindesk.com/v1/bpi/currentprice.json")
                            .ToCollection()
                            .GetFirstDoc();

            new
            {
                USD = query.Value("{'bpi':{'USD':{'rate':$}}}"),
                EUR = query.Value("{'bpi':{'EUR':{'rate':$}}}")
            }
            .ToCollection(Constants.FIRST_DOC_ID)
            .SetUIDimension("{'ReadOnly':true,'Style':'Cancel:Refresh'}")
            .SetDimension(DimensionType.Theme, "{'DefaultTheme':'White'}")
            .OpenForm(result => Rate());
        }

        public override void OnStart() => Rate();
    }
}
