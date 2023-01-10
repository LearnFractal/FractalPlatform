using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Forum
{
    public class ForumApplication : DashboardApplication
    {
        public ForumApplication(Guid sessionId,
                                BigDocInstance instance,
                                IFormFactory formFactory) : base(sessionId,
                                                                instance,
                                                                formFactory,
                                                                "Forum")
        {
        }

        public override void OnStart(Context context)
        {
            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
        }

        private void SendMessage(FormResult result)
        {
            if (result.Result)
            {
                //get message
                var message = result.Collection
                                    .GetDoc(Context, result.DocID)
                                    .Value("{'Message':$}");

                //get user avatar
                var avatar = Client.SetDefaultCollection("Users")
                                   .GetWhere("{'Name':@UserName}")
                                   .Value("{'Avatar':$}");

                //add new message
                Client.SetDefaultCollection("Topics")
                      .GetDoc(result.DocID)
                      .Update(@"{'Messages':[Add,{'OnDate':@Now,
                                                      'Who':@UserName,
                                                      'Avatar':@Avatar,
                                                      'Message':@Message}],
                                     'Message':'Put your message here'}",
                                  avatar,
                                  message);

                //show topic form
                Client.SetDefaultCollection("Topics")
                      .GetDoc(result.DocID)
                      .OpenForm(SendMessage);
            }
        }

        public void Topics()
        {
            Client.SetDefaultCollection("Topics")
                  .GetAll()
                  .WantModifyExistingDocuments()
                  .OpenForm(SendMessage);
        }

        public void NewTopic()
        {
            Client.SetDefaultCollection("NewTopic")
                  .WantCreateNewDocumentFor("Topics")
                  .OpenForm();
        }

        public override bool OnOpenForm(Context context, FormInfo formInfo)
        {
            if (formInfo.Collection.Name == "Topics" &&
               formInfo.DocID != Constants.ANY_DOC_ID)
            {
                var countViews = Client.SetDefaultCollection("Topics")
                                       .GetDoc(formInfo.DocID)
                                       .Value("{'CountViews':$}");

                Client.SetDefaultCollection("Topics")
                      .GetDoc(formInfo.DocID)
                      .UpdateByObject(new { CountViews = int.Parse(countViews) + 1 });

                formInfo.Collection.ReloadData();
            }

            return true;
        }

        public override object OnComputedDimension(Context context,
                                                   ComputedInfo computedInfo)
        {
            switch (computedInfo.Variable)
            {
                case "CountMessages":
                    {
                        var count = Client.SetDefaultCollection("Topics")
                                          .GetDoc(computedInfo.DocID)
                                          .Count("{'Messages':[{'Who':$}]}");

                        return count;
                    }
                default:
                    break;
            }

            return base.OnComputedDimension(context, computedInfo);
        }

        public override bool OnEventDimension(Context context,
                                              EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Login":
                    Login("Bob", "Bob");
                    return true;
                case "Topics":
                    Topics();
                    return true;
                case "NewTopic":
                    NewTopic();
                    return true;
                default:
                    return base.OnEventDimension(context, eventInfo);
            }
        }

        public override BaseRenderForm CreateRenderForm(string formName) => new RenderForm(this);
    }
}
