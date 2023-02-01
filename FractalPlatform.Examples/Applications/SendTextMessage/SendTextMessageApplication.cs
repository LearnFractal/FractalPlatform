using BigDoc.Client.App;
using BigDoc.Client.UI;

namespace FractalPlatform.Examples.Applications.SendTextMessage
{
    public class SendTextMessageApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          Client.SetDefaultCollection("Dashboard")
                                .GetFirstDoc()
                                .Update("{'TextMessages':[Add,{'Provider':'Telegram','Receiver':@Receiver,'Message':@Message,'IsSent':false}]}",
                                        result.Collection
                                              .GetFirstDoc()
                                              .Values("{'Receiver':$,'Message':$}")
                                              .ToArray());
                      }
                  });
        }
    }
}
