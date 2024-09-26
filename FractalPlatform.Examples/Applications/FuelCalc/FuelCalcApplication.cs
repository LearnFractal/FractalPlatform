using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.FuelCalc
{
    public class FuelCalcApplication : BaseApplication
    {
        private class Params
        {
            public decimal PricePerLitter { get; set; }
            public decimal ConsumptionPer100Km { get; set; }
            public int DistanceInKm { get; set; }
            public int NumberOfPeopeles { get; set; }
        }

        public override void OnStart() =>
            ModifyFirstDocOf("Fuel")
                .OpenForm(result =>
                {
                    var prms = result.Collection.GetFirstDoc().SelectOne<Params>();
                    var pricePerKm = prms.ConsumptionPer100Km * prms.PricePerLitter / 100;

                    new
                    {
                        Title = "See calculation below",
                        Price = new
                        {
                            OneKmCosts = pricePerKm,
                            DistanceCosts = pricePerKm * prms.DistanceInKm,
                            OnePersonPay = pricePerKm * prms.DistanceInKm / prms.NumberOfPeopeles
                        }
                    }
                    .ToCollection(Constants.FIRST_DOC_ID)
                    .SetUIDimension("{'Style':'Save:false','ReadOnly':true,'Title':{'ControlType':'Label'}}")
                    .OpenForm();
                });
    }
}
