using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;
using System.Net.NetworkInformation;

namespace FractalPlatform.Examples.Applications.Electricity
{
    public class ElectricityApplication : BaseApplication
    {
        private bool PingHost(string host)
        {
            var ping = new Ping();

            var pingreply = ping.Send(host, 1000);

            return pingreply.Status == IPStatus.Success;
        }

        public override bool OnTimerDimension(TimerInfo timerInfo)
        {
            var locations = Client.SetDefaultCollection("Locations")
                                  .GetAll()
                                  .Select<Location>();

            foreach (var location in locations)
            {
                if (location.HasElectricity != PingHost(location.IPAddress))
                {
                    location.HasElectricity = !location.HasElectricity;

                    Client.SetDefaultCollection("Locations")
                          .GetWhere("{'Address':@Address}", location.Address)
                          .Update(@"{'TextMessages':[Add,{'Provider':'Telegram',
                                                      'Receiver':@Receiver,
                                                      'Message':@Message,
                                                      'IsSent':false}]}",
                                    location.TelegramUserID,
                                    location.HasElectricity ? "Electricity available" : "No electricity");
                }

                Client.SetDefaultCollection("Locations")
                      .GetWhere("{'Address':@Address}", location.Address)
                      .Update("{'LastPingTime':@Now}");
            }

            return true;
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "NewLocation":
                    Client.SetDefaultCollection("NewLocation")
                          .WantCreateNewDocumentFor("Locations")
                          .OpenForm(result =>
                          {
                              if (result.Result)
                              {
                                  var gps = result.Collection
                                                  .GetFirstDoc()
                                                  .Values("{'Lat':$,'Lng':$}");

                                  Client.SetDefaultCollection("Dashboard")
                                        .GetFirstDoc()
                                        .Update("{'Map':{'Points':[Add,{'Lat':@Lat,'Lng':@Lng}]}}", gps[0], gps[1]);
                              }
                          });
                    return true;
                case "Locations":
                    Client.SetDefaultCollection("Locations")
                          .GetAll()
                          .OpenForm();
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm();
        }
    }
}