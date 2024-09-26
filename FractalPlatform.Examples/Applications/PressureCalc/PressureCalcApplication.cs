using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.PressureCalc
{
    public class PressureCalcApplication : BaseApplication
    {
        public override void OnStart() =>
            ModifyFirstDocOf("Params")
                .OpenForm(result =>
                {
                    var ageAndWeight = result.Collection.GetFirstDoc().IntValues("{'Age':$,'Weight':$}");

                    var sistolic = 108 + (0.5 * ageAndWeight[0]) + (0.1 * ageAndWeight[1]);
                    var distolic = 63 + (0.1 * ageAndWeight[0]) + (0.15 * ageAndWeight[1]);

                    MessageBox($"Your max blood pressure should be: {(int)sistolic}/{(int)distolic}.", "Max blood pressure");
                });
    }
}