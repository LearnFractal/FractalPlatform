using System;
using System.Linq;
using Newtonsoft.Json;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Weather
{
    public class WeatherApplication : BaseApplication
    {
        private const string _apiKey = "AIzaSyCwOrdckrXQNjKXhiG7ZRsKy0459ck8VDU";
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

        private string GetPicture(decimal prec)
        {
            if (prec == 0) return "Sun.svg";
            if (prec < 5) return "Clouds.svg";
            return "Rain.svg";
        }

        private string _lat = "50.4547"; //Kiev
        private string _lng = "30.5238";

        private void Weather()
        {
            var json = REST.Get($"https://api.open-meteo.com/v1/forecast?latitude={_lat}&longitude={_lng}&current_weather=true&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=GMT");

            var info = JsonConvert.DeserializeObject<WeatherInfo>(json);

            int i = 0;

            //Forecast page
            new
            {
                Latitude = _lat,
                Longitude = _lng,
                Forecast = info.daily.time
                                   .Select(x => new
                                   {
                                       Date = info.daily.time[i].ToShortDateString(),
                                       Day = info.daily.time[i].DayOfWeek.ToString().Substring(0, 3),
                                       MinTemp = info.daily.temperature_2m_min[i],
                                       MaxTemp = info.daily.temperature_2m_max[i],
                                       Picture = GetPicture(info.daily.precipitation_sum[i]),
                                       Precipitation = info.daily.precipitation_sum[i++]
                                   })
            }
            .ToCollection("Forecast")
            .SetUIDimension("{'Layout':'Forecast','IsRawPage':true}")
            .OpenForm(result =>
            {
                FirstDocOf("ChooseLocation")
                .ExtendDocument(DQL("{'Map':{'Center':{'Lat':@Lat,'Lng':@Lng},'Point':{'Lat':@Lat,'Lng':@Lng}}}", _lat, _lng))
                .OpenForm(result => {
                    if (result.Result)
                    {
                        var gps = result.Collection
                                        .GetFirstDoc()
                                        .Values("{'Map':{'Point':{'Lat':$,'Lng':$}}}");

                        _lat = gps[0];
                        _lng = gps[1];

                        Weather();
                    }
                });
            });
        }

        public override object OnComputedDimension(ComputedInfo info) =>
            info.Variable switch
            {
                "Key" => _apiKey,
                _ => REST.Get($"https://maps.googleapis.com/maps/api/geocode/json?latlng={_lat},{_lng}&sensor=true&key={_apiKey}")
                           .ToCollection()
                           .GetFirstDoc()
                           .Value("{'plus_code':{'compound_code':$}}")
            };
		
		public override void OnStart() => Weather();
        
        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}