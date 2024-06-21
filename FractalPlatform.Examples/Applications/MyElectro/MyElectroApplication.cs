using System;
using System.Linq;
using System.Text.RegularExpressions;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FractalPlatform.Examples.Applications.MyElectro
{
    public class MyElectroApplication : BaseApplication
    {
        private static DateTime _nextUpdateTime = DateTime.MinValue;
        private static string _today;
        private static string _tomorrow;

        private object GetSchedule(string schedule)
        {
            string prevElectricity = null;
            int groupId = 0;

            return schedule.ToCollection().ToAttrList().Where(x => DateTime.Now.Hour < int.Parse(x.Key.FirstPath))
                                    .Select(x =>
                                    {
                                        var hour = int.Parse(x.Key.FirstPath);
                                        var startHour = hour - 1;
                                        var endHour = hour;
                                        var electricity = x.Value.ToString().Replace("yes", "ПРИСУТНЄ").Replace("no", "ВІДСУТНЄ").Replace("maybe", "НЕВІДОМО");

                                        if (prevElectricity == null || prevElectricity != electricity)
                                        {
                                            groupId++;
                                            prevElectricity = electricity;
                                        }

                                        return new
                                        {
                                            StartHour = startHour,
                                            EndHour = endHour,
                                            Electricity = electricity,
                                            GroupId = groupId
                                        };
                                    })
                                    .GroupBy(x => x.GroupId,
                                             (k, g) => new
                                             {
                                                 StartHour = g.Min(x => x.StartHour),
                                                 EndHour = g.Max(x => x.EndHour),
                                                 Electricity = g.First().Electricity
                                             })
                                    .Select(x => new
                                    {
                                        Час = $"{x.StartHour.ToString("00")}:00 - {x.EndHour.ToString("00")}:00",
                                        Світло = x.Electricity
                                    })
                                    .ToList();
        }

        public override void OnStart()
        {
            //1. Download data
            if (DateTime.Now > _nextUpdateTime)
            {
                try
                {
                    var html = REST.Get("https://www.dtek-kem.com.ua/ua/shutdowns");
                    var startIndex = html.IndexOf("DisconSchedule.preset") + 23;
                    var endIndex = html.IndexOf("DisconSchedule.showCurSchedule");
                    var json = html.Substring(startIndex, endIndex - startIndex);
                    var data = (JObject)JsonConvert.DeserializeObject(json);
                    var dtekGroupId = "1";
                    var today = DateTime.Now.DayOfWeek != DayOfWeek.Sunday ? (int)DateTime.Now.DayOfWeek : 7;
                    var tomorrow = (today + 1) % 7 + 1;

                    _today = data["data"][dtekGroupId][today.ToString()].ToString();
                    _tomorrow = data["data"][dtekGroupId][tomorrow.ToString()].ToString();
                    _nextUpdateTime = DateTime.Now.AddMinutes(15);
                }
                catch { }
            }

            //2. Show data
            new
            {
                Exists = IsPowerLineStatusOnline ? $"На зараз світло => ПРИСУТНЄ" : "На зараз світло => ВІДСУТНЄ",
                Label = "ДТЕК прогнозує наявність світла:",
                Сьогодні = GetSchedule(_today),
                Завтра = GetSchedule(_tomorrow)
            }
            .ToCollection("Моніторинг")
            .SetUIDimension("{'Style':'Hide:Number;Cancel:Refresh','ReadOnly':true,'Label':{'ControlType':'Label'},'Exists':{'ControlType':'Label'}}")
            .SetDimension(DimensionType.Theme, "{'DefaultTheme': 'LightBlue', 'ChooseThemeOnLoginPage': false, 'ChooseThemeOnAllPages': false}")
            .OpenForm();
        }
    }
}