using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.UTube
{
    public class UTubeApplication : DashboardApplication
    {
        private void Dashboard()
        {
            var allChannels = Client.SetDefaultCollection("Channels")
                                        .GetWhere("{'IsLocked':false}")
                                        .Select<ChannelInfo>();

            if (!Context.User.IsGuest)
            {
                var subNames = Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Name':@UserName}")
                                     .Values("{'Subscriptions':[$]}");

                var history = Client.SetDefaultCollection("Users")
                                    .GetWhere("{'Name':@UserName}")
                                    .Values("{'History':[{'UID':$}]}");

                var subscribes = allChannels.Where(x => subNames.Contains(x.Name)) //only my subscriptions
                                            .SelectMany(x => x.Videos)
                                            .Where(x => !history.Contains(x.UID)) //not in my history
                                            .OrderByDescending(x => x.CountViews) //rate by views
                                            .ToStorage();

                var newVideos = allChannels.Where(x => !subNames.Contains(x.Name)) //not in my subscriptions
                                           .SelectMany(x => x.Videos)
                                           .Where(x => !history.Contains(x.UID))  //not in my history
                                           .OrderByDescending(x => x.OnDate)      //only fresh videos
                                           .Take(10)                              //first 10 fresh videos
                                           .ToList()
                                           .ToStorage();

                var recommendations = allChannels.Where(x => !subNames.Contains(x.Name)) //not in my subscriptions
                                                 .SelectMany(x => x.Videos)
                                                 .Where(x => !history.Contains(x.UID)) //not in my history
                                                 .OrderByDescending(x => x.CountViews) //rate by views
                                                 .Take(10) //only first 10 videos
                                                 .ToList()
                                                 .ToStorage();

                Client.SetDefaultCollection("Dashboard")
                      .GetFirstDoc()
                      .ToCollection()
                      .DeleteByParent("NewVideos")
                      .DeleteByParent("Subscribes")
                      .DeleteByParent("Recommendations")
                      .MergeToArrayPath(newVideos, "NewVideos")
                      .MergeToArrayPath(subscribes, "Subscribes")
                      .MergeToArrayPath(recommendations, "Recommendations")
                      .OpenForm(result => 
                      {
                          if (result.Result)
                          {
                              Dashboard();
                          }
                      });
            }
            else
            {
                var newVideos = allChannels.SelectMany(x => x.Videos)
                                           .OrderByDescending(x => x.OnDate)      //only fresh videos
                                           .Take(10)                              //first 10 fresh videos
                                           .ToList()
                                           .ToStorage();

                Client.SetDefaultCollection("Dashboard")
                      .GetFirstDoc()
                      .ToCollection()
                      .DeleteByParent("NewVideos")
                      .DeleteByParent("Subscribes")
                      .DeleteByParent("Recommendations")
                      .MergeToArrayPath(newVideos, "NewVideos")
                      .OpenForm(result =>
                      {
                          if (result.Result)
                          {
                              Dashboard();
                          }
                      });
            }
        }

        public override void OnLogin(FormResult result)
        {
            Dashboard();
        }

        private void OpenVideo(string uid)
        {
            if (!Context.User.IsGuest)
            {
                if (!Client.SetDefaultCollection("Users")
                           .GetWhere("{'Name':@UserName,'History':[Any,{'UID':@UID}]}", uid)
                           .Exists())
                {
                    var storage = Client.SetDefaultCollection("Channels")
                                        .GetWhere("{'Videos':[{'UID':@UID}]}", uid)
                                        .ToStorage("{'Videos':[!{'Name':$,'Description':$,'UID':$,'OnDate':$}]}");

                    Client.SetDefaultCollection("Users")
                          .GetWhere("{'Name':@UserName}")
                          .Update("{'History':[Add,@Video]}", storage.ToJson(Context, storage.GetFirstDocID(Context)));
                }
            }

            Client.SetDefaultCollection("Channels")
                  .GetWhere("{'Videos':[{'UID':@UID}]}", uid)
                  .Update("{'Videos':[{'CountViews':Add(1)}]}");

            Client.SetDefaultCollection("Channels")
                  .GetWhere("{'Videos':[{'UID':@UID}]}", uid)
                  .OpenForm("{'Videos':[$]}");
        }

        public override bool OnOpenForm(FormInfo formInfo)
        {
            if (formInfo.Collection.Name == "Dashboard" &&
                (formInfo.AttrPath.FirstPath == "NewVideos" ||
                 formInfo.AttrPath.FirstPath == "Subscribes" ||
                 formInfo.AttrPath.FirstPath == "Recommendations"))
            {
                var uid = formInfo.Collection
                                  .GetWhere(formInfo.AttrPath)
                                  .Value("{'" + formInfo.AttrPath.FirstPath + "':[{'UID':$}]}");

                OpenVideo(uid);
                
                formInfo.Collection.ReloadData();

                return false;
            }
            else if(formInfo.Collection.Name == "Channels" &&
                    formInfo.AttrPath.FirstPath == "Videos")
            {
                var uid = Client.SetDefaultCollection("Channels")
                                .GetWhere(formInfo.AttrPath)
                                .Value("{'Videos':[{'UID':$}]}");

                OpenVideo(uid);

                formInfo.Collection.ReloadData();
            }
            else if (formInfo.Collection.Name == "Users" &&
                     formInfo.AttrPath.FirstPath == "History")
            {
                var uid = formInfo.Collection
                                  .GetWhere(formInfo.AttrPath)
                                  .Value("{'History':[{'UID':$}]}");

                OpenVideo(uid);

                formInfo.Collection.ReloadData();

                return false;
            }

            return true;
        }

        public bool HasSubscription(uint docID)
        {
            var channel = Client.SetDefaultCollection("Channels")
                                .GetDoc(docID)
                                .Value("{'Name':$}");

            return Client.SetDefaultCollection("Users")
                         .GetWhere("{'Name':@UserName,'Subscriptions':[Any,@Channel]}", channel)
                         .Exists();
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            var owner = Client.SetDefaultCollection("Channels")
                                          .GetDoc(computedInfo.GrandDocID)
                                          .Value("{'Owner':$}");

            switch (computedInfo.Variable)
            {
                case "Subscribe":
                    {
                        return !HasSubscription(computedInfo.GrandDocID) &&
                                Context.User.Name != owner &&
                                !Context.User.IsGuest;
                    }
                case "Unsubscribe":
                    {
                        return HasSubscription(computedInfo.GrandDocID) &&
                               Context.User.Name != owner &&
                               !Context.User.IsGuest;
                    }
                case "NewVideo":
                    {
                        return owner == Context.User.Name;
                    }
                case "Avatar": 
                    {
                        return Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Name':@UserName}")
                                     .Value("{'Photo':$}");
                    }
                default:
                    {
                        return base.OnComputedDimension(computedInfo);
                    }
            }
        }

        public override bool OnSecurityDimension(SecurityInfo securityInfo)
        {
            if(securityInfo.Variable == "MyUser" &&
               (securityInfo.OperationType == OperationType.Create ||
                securityInfo.OperationType == OperationType.Update ||
                securityInfo.OperationType == OperationType.Delete))
            {
                var name = Client.SetDefaultCollection("Users")
                                 .GetWhere(securityInfo.AttrPath)
                                 .Value("{'Name':$}");

                return name == Context.User.Name || Context.User.HasRole("Admin");
            }
            else if (securityInfo.Variable == "MyChannel" &&
                    (securityInfo.OperationType == OperationType.Create ||
                     securityInfo.OperationType == OperationType.Update ||
                     securityInfo.OperationType == OperationType.Delete))
            {
                var name = Client.SetDefaultCollection("Channels")
                                 .GetWhere(securityInfo.AttrPath)
                                 .Value("{'Owner':$}");

                return name == Context.User.Name || Context.User.HasRole("Admin");
            }
            else if(securityInfo.OperationType == OperationType.Read)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Login":
                    {
                        Login();

                        return true;
                    }
                case "Logout":
                    {
                        Logout();

                        return true;
                    }
                case "Register":
                    {
                        Register();

                        return true;
                    }
                case "MyUser":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@UserName}")
                              .WantModifyExistingDocuments()
                              .OpenForm();

                        return true;
                    }

                case "Users":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetAll()
                              .WantModifyExistingDocuments()
                              .OpenForm();

                        return true;
                    }
                case "NewChannel":
                    {
                        Client.SetDefaultCollection("NewChannel")
                              .WantCreateNewDocumentFor("Channels")
                              .OpenForm(result =>
                              {
                                  if (result.Result)
                                  {
                                      MessageBox("Thank you ! New channel created.");
                                  }
                              });

                        return true;
                    }
                case "MyChannels":
                    {
                        Client.SetDefaultCollection("Channels")
                              .GetWhere("{'Owner':@UserName,'IsLocked':false}")
                              .WantModifyExistingDocuments()
                              .OpenForm();

                        return true;
                    }
                case "Channels":
                    {
                        Client.SetDefaultCollection("Channels")
                              .GetWhere("{'IsLocked':false}")
                              .WantModifyExistingDocuments()
                              .OpenForm();

                        return true;
                    }
                case "History":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@UserName}")
                              .ExtendUIDimension("{'ReadOnly':true,'History':{'Visible':true,'UID':{'Visible':false}}}")
                              .OpenForm("{'History':[$]}");

                        return true;
                    }
                case "NewVideo":
                    {
                        var docID = Client.SetDefaultCollection("Channels")
                                          .GetWhere(eventInfo.AttrPath)
                                          .GetFirstID();

                        Client.SetDefaultCollection("NewVideo")
                              .WantCreateNewDocumentForArray("Channels", "{'Videos':[$]}", docID)
                              .OpenForm();

                        return true;
                    }
                case "Subscribe":
                    {
                        var channel = Client.SetDefaultCollection("Channels")
                                            .GetWhere(eventInfo.AttrPath)
                                            .Value("{'Name':$}");

                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@UserName}")
                              .Update("{'Subscribes':[Add,@Channel]}", channel);

                        return true;
                    }
                case "Unsubscribe":
                    {
                        MessageBox("Not implemented");

                        return true;
                    }
                case "NewComment":
                    {
                        Client.SetDefaultCollection("NewComment")
                              .GetFirstDoc()
                              .OpenForm(result =>
                              {
                                  if (result.Result)
                                  {
                                      var json = result.Collection.ToJson();

                                      Client.SetDefaultCollection("Channels")
                                            .GetWhere(eventInfo.AttrPath)
                                            .Update("{'Videos':[{'Comments':[Add,@Comment]}]}", json);

                                      result.NeedReloadData = true;
                                  }
                              });

                        return true;
                    }
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override void OnStart()
        {
            TryAutoLogin();
            
            Dashboard();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
