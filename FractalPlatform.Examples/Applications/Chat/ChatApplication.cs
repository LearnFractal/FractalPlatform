using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.Chat
{
    public class ChatApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Chats")
                  .GetFirstDoc()
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          Client.SetDefaultCollection("Chats")
                                .GetFirstDoc()
                                .Update("{'Messages':[Add,{'OnDate':@Now,'Who':@Who,'Message':@Message}]}",
                                        result.Collection
                                              .GetFirstDoc()
                                              .Values("{'Who':$,'Message':$}")
                                              .ToArray());
                      }

                      OnStart();
                  });
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this);
    }
}
