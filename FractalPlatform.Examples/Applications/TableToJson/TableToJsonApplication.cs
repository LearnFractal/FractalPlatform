using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using System.Text;

namespace FractalPlatform.Examples.Applications.TableToJson
{
    public class TableToJsonApplication : BaseApplication
    {
        public override void OnStart()
        {
            FirstDocOf("Dashboard")
                  .OpenForm(result =>
                  {
                      var table = result.Collection
                                        .GetFirstDoc()
                                        .Value("{'Table':$}");

                      var lines = table.Split('\n');

                      var columns = lines[0].Split('\t');

                      var sb = new StringBuilder();

                      sb.Append("{'Documents':[");

                      for (int i = 1; i < lines.Length; i++)
                      {
                          sb.Append('{');

                          var cells = lines[i].Split("\t");

                          for (int j = 0; j < columns.Length; j++)
                          {
                              if (j > 0)
                              {
                                  sb.Append(',');
                              }

                              sb.Append($"'{columns[j].Trim()}': '{cells[j].Trim()}'");
                          }

                          sb.Append('}');
                      }

                      sb.Append("]}");

                      var json = sb.ToString();

                      MessageBox(json);
                  });
        }
    }
}
