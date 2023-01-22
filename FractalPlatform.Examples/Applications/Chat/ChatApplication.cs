using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.Chat
{
    public class ChatApplication : BaseApplication
    {
        public ChatApplication(Guid sessionId,
                               BigDocInstance instance,
                               IFormFactory formFactory) : base(sessionId,
                                                               instance,
                                                               formFactory,
                                                               "Chat")
        {
        }

        private class MessageInfo
        {
            public string Who { get; set; }
            public string Message { get; set; }
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("Chats")
                  .GetFirstDoc()
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          var message = result.Collection
                                              .GetDoc(Constants.FIRST_DOC_ID)
                                              .SelectOne<MessageInfo>();

                          Client.SetDefaultCollection("Chats")
                                .GetFirstDoc()
                                .Update("{'Messages':[Add,{'OnDate':@Now,'Who':@Who,'Message':@Message}]}",
                                            message.Who,
                                            message.Message);

                          OnStart();
                      }
                  });
        }

        public override BaseRenderForm CreateRenderForm(string formName) => new RenderForm(this);
    }
}
