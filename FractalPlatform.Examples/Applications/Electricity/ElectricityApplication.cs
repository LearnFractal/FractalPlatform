using System.Net.NetworkInformation;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Electricity
{
    public class ElectricityApplication : BaseApplication
    {
        private bool PingHost(string host)
        {
            try
            {
                var ping = new Ping();

                var pingreply = ping.Send(host, 1000);

                return pingreply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        public override bool OnTimerDimension(TimerInfo info)
        {
            var locations = DocsOf("Locations").Select<Location>();

            foreach (var location in locations)
            {
                if (location.HasElectricity != PingHost(location.IPAddress) && location.TelegramUserID > 0)
                {
                    location.HasElectricity = !location.HasElectricity;

                    DocsWhere("Locations", "{'Address':@Address}", location.Address)
                    .Update(@"{'HasElectricity':@HasElectricity,
                              'TextMessages':[Add,{'Provider':'Telegram',
                                                   'Receiver':@Receiver,
                                                   'Message':@Message,
                                                   'IsSent':false}]}",
                             location.HasElectricity,
                             location.TelegramUserID,
                             location.HasElectricity ? $"{location.Address} HAS electricity" : $"{location.Address} HAS NO electricity");
                }

                DocsWhere("Locations", "{'Address':@Address}", location.Address)
                .Update("{'LastPingTime':@Now}");
            }

            return true;
        }

        public override bool OnEventDimension(EventInfo info)
        {
            switch (info.Action)
            {
                case "NewLocation":
                    CreateNewDocFor("NewLocation", "Locations").OpenForm(result =>
                    {
                        if (result.Result)
                        {
                            var gps = result.Collection
                                            .GetFirstDoc()
                                            .Values("{'Lat':$,'Lng':$}");

                            FirstDocOf("Dashboard").Update("{'Map':{'Points':[Add,{'Lat':@Lat,'Lng':@Lng}]}}", gps[0], gps[1]);

                            result.NeedReloadData = true;
                        }
                    });
                    return true;
                case "Locations":
                    DocsOf("Locations").OpenForm();
                    return true;
                default:
                    return base.OnEventDimension(info);
            }
        }

        public override void OnStart() => FirstDocOf("Dashboard").OpenForm();
        
        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}