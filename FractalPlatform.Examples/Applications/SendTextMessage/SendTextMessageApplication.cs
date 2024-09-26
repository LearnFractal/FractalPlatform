using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.SendTextMessage
{
    public class SendTextMessageApplication : BaseApplication
    {
        public override void OnStart() =>
            FirstDocOf("Dashboard")
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          FirstDocOf("Dashboard")
                                .Update("{'TextMessages':[Add,{'Provider':'Telegram','Receiver':@Receiver,'Message':@Message,'IsSent':false}]}",
                                        result.Collection
                                              .GetFirstDoc()
                                              .Values("{'Receiver':$,'Message':$}")
                                              .ToArray());
                      }
                  });
    }
}
