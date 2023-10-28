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
            CloseIfOpenedForm("Dashboard");

            const int topVideos = 4;

            var allChannels = DocsWhere("Channels", "{'IsLocked':false}").Select<ChannelInfo>();

            if (!Context.User.IsGuest)
            {
                var subNames = DocsWhere("Users", "{'Name':@UserName}").Values("{'Subscribes':[$]}");

                var history = DocsWhere("Users", "{'Name':@UserName}").Values("{'History':[{'UID':$}]}");

                var subscribes = allChannels.Where(x => subNames.Contains(x.Name)) //only my subscribes
                                            .SelectMany(x => x.Videos)
                                            .Where(x => !history.Contains(x.UID)) //not in my history
                                            .OrderByDescending(x => x.CountViews) //rate by views
                                            .ToStorage();

                var newVideos = allChannels.Where(x => !subNames.Contains(x.Name)) //not in my subscribes
                                           .SelectMany(x => x.Videos)
                                           .Where(x => !history.Contains(x.UID))  //not in my history
                                           .OrderByDescending(x => x.OnDate)      //only fresh videos
                                           .Take(topVideos)                       //first 5 fresh videos
                                           .ToList()
                                           .ToStorage();

                var recommendations = allChannels.Where(x => !subNames.Contains(x.Name)) //not in my subscribes
                                                 .SelectMany(x => x.Videos)
                                                 .Where(x => !history.Contains(x.UID)) //not in my history
                                                 .OrderByDescending(x => x.CountViews) //rate by views
                                                 .Take(topVideos) //only first 5 videos
                                                 .ToList()
                                                 .ToStorage();

                FirstDocOf("Dashboard")
                      .ToCollection()
                      .DeleteByParent("NewVideos")
                      .DeleteByParent("Subscribes")
                      .DeleteByParent("Recommendations")
                      .MergeToArrayPath(newVideos, "NewVideos")
                      .MergeToArrayPath(subscribes, "Subscribes")
                      .MergeToArrayPath(recommendations, "Recommendations")
                      .OpenForm();
            }
            else
            {
                var newVideos = allChannels.SelectMany(x => x.Videos)
                                           .OrderByDescending(x => x.OnDate) //only fresh videos
                                           .Take(topVideos)                  //first 10 fresh videos
                                           .ToList()
                                           .ToStorage();

                var recommendations = allChannels.SelectMany(x => x.Videos)
                                                 .OrderByDescending(x => x.CountViews) //rate by views
                                                 .Take(topVideos)                      //only first 5 videos
                                                 .ToList()
                                                 .ToStorage();

                FirstDocOf("Dashboard")
                      .ToCollection()
                      .DeleteByParent("NewVideos")
                      .DeleteByParent("Subscribes")
                      .DeleteByParent("Recommendations")
                      .MergeToArrayPath(newVideos, "NewVideos")
                      .MergeToArrayPath(recommendations, "Recommendations")
                      .OpenForm();
            }
        }

        public override void OnLogin(FormResult result)
        {
            Dashboard();
        }

        public override void OnRegister(FormResult result)
        {
            Dashboard();
        }

        private void OpenVideo(string uid)
        {
            if (!Context.User.IsGuest)
            {
                if (!DocsWhere("Users", "{'Name':@UserName,'History':[Any,{'UID':@UID}]}", uid)
                     .Exists())
                {
                    var storage = DocsWhere("Channels", "{'Videos':[{'UID':@UID}]}", uid)
                                  .ToStorage("{'Videos':[!{'Name':$,'Description':$,'UID':$,'OnDate':$}]}");

                    DocsWhere("Users", "{'Name':@UserName}")
                    .Update("{'History':[Add,@Video]}", storage.ToJson(Context, storage.GetFirstDocID(Context)));
                }
            }

            var count = DocsWhere("Channels", "{'Videos':[{'UID':@UID}]}", uid).Count();

            DocsWhere("Channels", "{'Videos':[{'UID':@UID}]}", uid)
            .Update("{'Videos':[{'CountViews':Add(1)}]}");

            DocsWhere("Channels", "{'Videos':[{'UID':@UID}]}", uid)
            .OpenForm("{'Videos':[$]}", result => Dashboard());
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

                return false;
            }
            else if (formInfo.Collection.Name == "Channels" &&
                    formInfo.AttrPath.FirstPath == "Videos")
            {
                var uid = DocsWhere("Channels", formInfo.AttrPath)
                          .Value("{'Videos':[{'UID':$}]}");

                OpenVideo(uid);

                return false;
            }
            else if (formInfo.Collection.Name == "Users" &&
                     formInfo.AttrPath.FirstPath == "History")
            {
                var uid = formInfo.Collection
                                  .GetWhere(formInfo.AttrPath)
                                  .Value("{'History':[{'UID':$}]}");

                OpenVideo(uid);

                return false;
            }

            return true;
        }

        public bool HasSubscription(uint docID)
        {
            var channel = DocsWhere("Channels", docID)
                          .Value("{'Name':$}");

            return DocsWhere("Users", "{'Name':@UserName,'Subscribes':[Any,@Channel]}", channel)
                   .Exists();
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            var owner = DocsWhere("Channels", computedInfo.GrandDocID)
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
                        if (!Context.User.IsGuest)
                        {
                            return DocsWhere("Users", "{'Name':@UserName}")
                                   .Value("{'Photo':$}");
                        }
                        else
                        {
                            return "avatar.png";
                        }
                    }
                default:
                    {
                        return base.OnComputedDimension(computedInfo);
                    }
            }
        }

        public override bool OnSecurityDimension(SecurityInfo securityInfo)
        {
            if (securityInfo.Variable == "MyUser" &&
               (securityInfo.OperationType == OperationType.Create ||
                securityInfo.OperationType == OperationType.Update ||
                securityInfo.OperationType == OperationType.Delete))
            {
                var name = DocsWhere("Users", securityInfo.AttrPath)
                           .Value("{'Name':$}");

                return name == Context.User.Name || Context.User.HasRole("Admin");
            }
            else if (securityInfo.Variable == "MyChannel" &&
                    (securityInfo.OperationType == OperationType.Create ||
                     securityInfo.OperationType == OperationType.Update ||
                     securityInfo.OperationType == OperationType.Delete))
            {
                var name = DocsWhere("Channels", securityInfo.AttrPath)
                           .Value("{'Owner':$}");

                return name == Context.User.Name || Context.User.HasRole("Admin");
            }
            else if (securityInfo.OperationType == OperationType.Read)
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
                        ModifyDocsWhere("Users", "{'Name':@UserName}")
                        .OpenForm();

                        return true;
                    }

                case "Users":
                    {
                        ModifyDocsOf("Users")
                        .OpenForm();

                        return true;
                    }
                case "NewChannel":
                    {
                        CreateNewDocFor("NewChannel", "Channels")
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
                        ModifyDocsWhere("Channels", "{'Owner':@UserName,'IsLocked':false}")
                        .OpenForm();

                        return true;
                    }
                case "Channels":
                    {
                        ModifyDocsWhere("Channels", "{'IsLocked':false}")
                        .OpenForm();

                        return true;
                    }
                case "History":
                    {
                        DocsWhere("Users", "{'Name':@UserName}")
                        .ExtendUIDimension("{'ReadOnly':true,'History':{'Visible':true,'UID':{'Visible':false}}}")
                        .OpenForm("{'History':[$]}");

                        return true;
                    }
                case "ClearHistory":
                    {
                        MessageBox("Are you sure to clear history?",
                                   "Clear history",
                                   MessageBoxButtonType.YesNo,
                                   result => {
                                       if (result.Result)
                                       {
                                           DocsWhere("Users", "{'Name':@UserName}")
                                           .Delete("{'History':[$]}");

                                           Dashboard();
                                       }
                                   });

                        return true;
                    }
                case "NewVideo":
                    {
                        var docID = DocsWhere("Channels", eventInfo.AttrPath)
                                    .GetFirstID();

                        CreateNewDocForArray("NewVideo", "Channels", "{'Videos':[$]}", docID)
                        .OpenForm();

                        return true;
                    }
                case "Subscribe":
                    {
                        var channel = DocsWhere("Channels", eventInfo.AttrPath)
                                      .Value("{'Name':$}");

                        DocsWhere("Users", "{'Name':@UserName}")
                        .Update("{'Subscribes':[Add,@Channel]}", channel);

                        MessageBox("Thank you. You are subscribed on the channel.", MessageBoxButtonType.Ok);

                        return true;
                    }
                case "Unsubscribe":
                    {
                        var channel = DocsWhere("Channels", eventInfo.AttrPath)
                                      .Value("{'Name':$}");

                        DocsWhere("Users", "{'Name':@UserName}")
                        .Delete("{'Subscribes':[@Channel]}", channel);

                        MessageBox("You are unsubscribed from the channel.", MessageBoxButtonType.Ok);

                        return true;
                    }
                case "NewComment":
                    {
                        FirstDocOf("NewComment")
                              .OpenForm(result =>
                              {
                                  if (result.Result)
                                  {
                                      var json = result.Collection.ToJson();

                                      DocsWhere("Channels", eventInfo.AttrPath)
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