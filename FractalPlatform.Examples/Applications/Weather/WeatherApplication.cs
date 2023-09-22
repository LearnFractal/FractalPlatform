using System;
using System.Linq;
using Newtonsoft.Json;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.Weather
{
    public class WeatherApplication : BaseApplication
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

        private void Weather(string lat, string lng)
        {
            var json = REST.Get($"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lng}&current_weather=true&daily=temperature_2m_max,temperature_2m_min,precipitation_sum");

            var info = JsonConvert.DeserializeObject<WeatherInfo>(json);

            int i = 0;

            new
            {
                Title = "Weather",
                Latitude = lat,
                Longitude = lng,
                Forecast = info.daily
                                   .time
                                   .Select(x => new
                                   {
                                       Date = info.daily.time[i].ToShortDateString(),
                                       Day = info.daily.time[i].DayOfWeek,
                                       MinTemp = info.daily.temperature_2m_min[i],
                                       MaxTemp = info.daily.temperature_2m_max[i],
                                       Precipitation = info.daily.precipitation_sum[i++]
                                   })
            }
            .ToCollection(Constants.FIRST_DOC_ID)
            .SetUIDimension("{'Forecast':{'ReadOnly':true},'Title':{'ControlType':'Label'},'Style':'Save:Refresh'}")
            .SetDimension(DimensionType.Validation, "{'Latitude':{'IsRequired':true,'Type':'float'},'Longitude':{'IsRequired':true,'Type':'float'}}")
            .SetDimension(DimensionType.Theme, "{'DefaultTheme':'LightBlue'}")
            .OpenForm(result =>
            {
                if (result.Result)
                {
                    var gps = result.Collection
                                    .GetFirstDoc()
                                    .Values("{'Latitude':$,'Longitude':$}");

                    Weather(gps[0], gps[1]);
                }
            });
        }

        public override void OnStart()
        {
            Weather("50.4547", "30.5238"); //Kyiv        
        }
    }
}
