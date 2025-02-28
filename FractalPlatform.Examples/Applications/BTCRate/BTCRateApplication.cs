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
            REST.Get("https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD,EUR")
                .ToCollection(Constants.FIRST_DOC_ID)
                .SetUIDimension("{'ReadOnly':true,'Style':'Cancel:Refresh'}")
                .SetDimension(DimensionType.Theme, "{'DefaultTheme':'White'}")
                .OpenForm(result => Rate());
        }

        public override void OnStart() => Rate();
    }
}
