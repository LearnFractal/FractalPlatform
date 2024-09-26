using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Common.Serialization;
using System.Text;

namespace FractalPlatform.Examples.Applications.TableToJson
{
    public class TableToJsonApplication : BaseApplication
    {
        private string TableToJson(string text)
        {
            var lines = text.Split('\n');

            var columns = lines[0].Split(new[] { '\t', ',' });

            var sb = new StringBuilder();

            sb.Append("{\"Rows\":[");

            for (int i = 1; i < lines.Length; i++)
            {
                var cells = lines[i].Split(new[] { '\t', ',' });

                if (columns.Length == cells.Length)
                {
                    if (i > 1)
                    {
                        sb.Append(',');
                    }

                    sb.Append('{');

                    for (int j = 0; j < columns.Length; j++)
                    {
                        if (j > 0)
                        {
                            sb.Append(',');
                        }

                        var attr = columns[j].Trim();
                        var val = cells[j].Trim();

                        bool boolVal;
                        decimal numVal;

                        if (bool.TryParse(val, out boolVal) ||
                            decimal.TryParse(val, out numVal))
                        {
                            sb.Append($"\"{attr}\": {val.ToLower()}");
                        }
                        else
                        {
                            sb.Append($"\"{attr}\": \"{val}\"");
                        }
                    }

                    sb.Append('}');
                }
            }

            sb.Append("]}");

            return JsonConvert.FormatJson(sb.ToString());
        }

        public override void OnStart() =>
            FirstDocOf("Dashboard")
                  .OpenForm(result =>
                  {
                      var fileName = result.Collection
                                           .GetFirstDoc()
                                           .Value("{'Table':$}");

                      var text = ReadFileText(fileName);

                      var json = TableToJson(text);

                      WriteFileText(fileName, json);

                      DQL("{'Download':@FileName}", fileName)
                        .ToCollection()
                        .SetUIDimension("{'Style':'Save:false','Download':{'ControlType':'Link'}}")
                        .OpenForm();
                  });
    }
}
