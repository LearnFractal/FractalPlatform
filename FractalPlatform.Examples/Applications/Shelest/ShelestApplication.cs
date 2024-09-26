using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using Newtonsoft.Json;

namespace FractalPlatform.Examples.Applications.Shelest
{
    public class ShelestApplication : BaseApplication
    {
        private class DailyInfo
        {
            public DateTime[] time { get; set; }
            public decimal[] temperature_2m_min { get; set; }
            public decimal[] temperature_2m_max { get; set; }
            public decimal[] precipitation_sum { get; set; }
        }

        private class WeatherInfo
        {
            public DailyInfo daily { get; set; }
        }

        private string _lat = "50.4547"; //Kiev
        private string _lng = "30.5238";

        private void Dashboard(int nights, int adults, int scanDays)
        {
            var rooms = UseCache(() =>
            {
                //weather
                var json = REST.Get($"https://api.open-meteo.com/v1/forecast?latitude={_lat}&longitude={_lng}&current_weather=true&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=GMT");

                var info = JsonConvert.DeserializeObject<WeatherInfo>(json);

                //rooms
                var rooms = new List<object>();
                var currDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                for (int i = 0; i < scanDays; i++)
                {
                    var startDate = currDate.ToString("yyyy-MM-dd");
                    var endDate = currDate.AddDays(nights).ToString("yyyy-MM-dd");

                    json = REST.Post("https://shelest.rooms-wizard.com/search",
                                     "from_date", startDate,
                                     "to_date", endDate,
                                     "currency_id", "62cd16f0-e292-4952-a454-addbb244c492",
                                     "count_adults", adults.ToString());

                    if (!json.Contains("rooms_not_found"))
                    {
                        var prices = Regex.Matches(json, $"\"adult_{adults}\":(?<price>[0-9]+)").Select(x => int.Parse(x.Groups["price"].Value));
                        var minPrice = prices.Min();
                        var maxPrice = prices.Max();

                        var weather = "?";

                        for (int j = 0; j < info.daily.time.Length; j++)
                        {
                            if (info.daily.time[j] == currDate)
                            {
                                weather = $"+{info.daily.temperature_2m_min[j]}C - +{info.daily.temperature_2m_max[j]}C";

                                if (info.daily.precipitation_sum[j] == 0)
                                {
                                    weather += " Sunny";
                                }
                            }
                        }

                        rooms.Add(new
                        {
                            Day = currDate.DayOfWeek.ToString().Substring(0, 3),
                            StartDate = startDate,
                            EndDate = endDate,
                            MinPrice = minPrice * nights,
                            MaxPrice = maxPrice * nights,
                            Weather = weather
                        });
                    }

                    currDate = currDate.AddDays(1);
                }

                return rooms;
            },
            TimeSpan.FromMinutes(15),
            $"{nights}_{adults}_{scanDays}");

            new
            {
                Hotel = $"Available rooms in Shelest.ua hotel",
                Nights = nights,
                Adults = adults,
                ScanDays = scanDays,
                Rooms = rooms
            }
            .ToCollection("Shelest")
            .SetUIDimension("{'Style':'Save:Refresh;Cancel:false;Hide:Number','ReadOnly':true,'Hotel':{'ControlType':'Label'},'Nights':{'ReadOnly':false},'Adults':{'ReadOnly':false},'ScanDays':{'ReadOnly':false}}")
            .SetDimension(DimensionType.Enum, "{'Adults':{'Items':[1,2,3]},'Nights':{'Items':[1,2,3,4,5,6,7]}}")
            .SetDimension(DimensionType.Validation, "{'ScanDays':{'MinValue':1,'MaxValue':30}}")
            .SetThemeDimension(ThemeType.LightGreen)
            .OpenForm(result => 
            {
                if (result.Result)
                {
                    var vals = result.Collection.FindFirstValues("Nights", "Adults", "ScanDays");
                    
                    Dashboard(int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2]));
                }
            });
        }

        public override void OnStart() => Dashboard(1, 2, 7);
    }
}