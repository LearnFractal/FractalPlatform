using System;
using System.Linq;
using System.Text;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.MultTable
{
    public class MultTableApplication : BaseApplication
    {
        private class Setting
        {
            public uint From { get; set; }
            public uint To { get; set; }
            public bool Shuffle { get; set; }
            public uint PagePortion { get; set; }
        }

        private uint[] GetArray(uint from, uint to, bool shuffle)
        {
            var arr = new uint[to - from + 1];

            for (uint i = from, j = 0; i <= to; i++, j++)
            {
                arr[j] = i;
            }

            if (shuffle)
            {
                Random rnd = new Random();

                arr = arr.OrderBy(x => rnd.Next())
                         .ToArray();
            }

            return arr;

        }

        public override void OnStart() =>
            FirstDocOf("Setting")
                  .OpenForm(null, null, result =>
                  {
                      if (result.Result)
                      {
                          var setting = result.Collection
                                              .GetFirstDoc()
                                              .SelectOne<Setting>();

                          var sbDoc = new StringBuilder();
                          var sbVal = new StringBuilder();

                          sbDoc.Append('{');
                          sbVal.Append('{');

                          foreach (var i in GetArray(setting.From, setting.To, setting.Shuffle))
                          {
                              foreach (var j in GetArray(0, 9, setting.Shuffle))
                              {
                                  if (j > 0 && i % j == 0)
                                  {
                                      if (sbDoc.Length > 1)
                                      {
                                          sbDoc.AppendLine(",");
                                          sbVal.AppendLine(",");
                                      }

                                      sbDoc.Append($"'{i} : {j} =':''");
                                      sbVal.Append($"'{i} : {j} =':").Append("{'Formula':'@Value = " + (i / j).ToString() + "'}");
                                  }
                              }
                          }

                          sbDoc.Append('}');
                          sbVal.Append('}');

                          new Collection("Collection", sbDoc.ToString())
                              .SetDimension(DimensionType.Validation, sbVal.ToString())
                              .OpenForm(handleResult =>
                              {
                                  MessageBox("All answers are successful !", "Result");
                              });
                      }
                  });
    }
}
