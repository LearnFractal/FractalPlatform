using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;
using System;
using System.Linq;

namespace FractalPlatform.Examples.Applications.FreelanceResponse
{
    public class FreelanceResponseApplication : DashboardApplication
    {
        public TaskInfo GetTask(uint docID)
        {
            return Client.SetDefaultCollection("Tasks")
                         .GetDoc(docID)
                         .SelectOne<TaskInfo>();
        }

        private string GetDemoWho(AttrPath attrPath)
        {
            return Client.SetDefaultCollection("Tasks")
                         .GetWhere(attrPath)
                         .Value("{'Demos':[{'Who':$}]}");
        }

        private uint GetCustomerUserID(uint docID)
        {
            var task = GetTask(docID);

            return Client.SetDefaultCollection("Users")
                         .GetWhere("{'Name':@Who}", task.Who)
                         .GetFirstID();
        }

        private uint GetDemoUserID(AttrPath attrPath)
        {
            var who = GetDemoWho(attrPath);

            return Client.SetDefaultCollection("Users")
                         .GetWhere("{'Name':@Who}", who)
                         .GetFirstID();
        }

        private void RejectTask(uint docID)
        {
            var userID = GetCustomerUserID(docID);

            Client.SetDefaultCollection("Users")
                  .GetDoc(userID)
                  .Update("{'Rates':[Add,{'Who':'Bot','OnDate':@Now,'Comment':'Task rejected','Stars':1}]}");

            Client.SetDefaultCollection("Tasks")
                  .GetDoc(docID)
                  .Update("{'Status':'Rejected'}");
        }

        public override bool OnTimerDimension(TimerInfo timerInfo)
        {
            if (timerInfo.Action == "Expired")
            {
                var expired = DateTime.Now.AddDays(-3);

                var docIDs = Client.SetDefaultCollection("Tasks")
                                   .GetWhere("{'Status':Any('New','InProgress'),'EndDate':Less(@Expired)}",
                                             expired)
                                   .GetDocIDs();

                foreach (var docID in docIDs)
                {
                    RejectTask(docID);
                }
            }

            return true;
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            switch (computedInfo.Variable)
            {
                case "NewTaskNumber":
                    {
                        return "T-" + (Client.SetDefaultCollection("Tasks")
                                             .GetAll()
                                             .Count() + 1).ToString("00000");
                    }
                case "Rating":
                    {
                        var count = Client.SetDefaultCollection("Users")
                                          .GetWhere(computedInfo.AttrPath)
                                          .Count("{'Rates':[{'Stars':$}]}");

                        var sum = Client.SetDefaultCollection("Users")
                                        .GetWhere(computedInfo.AttrPath)
                                        .Sum("{'Rates':[{'Stars':$}]}");

                        return count > 0 ? sum / count : 0;
                    }
                case "NewAccept":
                    {
                        var task = GetTask(computedInfo.GrandDocID);

                        return task.Who != Context.User.Name &&
                                   !task.Accepts.Any(x => x.Who == Context.User.Name) &&
                                   (string.IsNullOrEmpty(task.Developer) || task.TenderModel == TenerModelType.TheBestDemo) &&
                                   (task.Status == StatusType.New || task.Status == StatusType.InProgress);
                    }
                case "NewDemo":
                    {
                        var task = GetTask(computedInfo.GrandDocID);

                        return task.Accepts.Any(x => x.Who == Context.User.Name) &&
                               !task.Demos.Any(x => x.Who == Context.User.Name) &&
                               (task.TenderModel == TenerModelType.TheBestDemo || 
                                (task.TenderModel == TenerModelType.TheBestDeveloper && task.Developer == Context.User.Name)) &&
                               (task.Status == StatusType.New || task.Status == StatusType.InProgress);
                    }
                default:
                    break;

            }

            return base.OnComputedDimension(computedInfo);
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Users":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetAll()
                              .OpenForm();

                        break;
                    }
                case "Tasks":
                    {
                        var query = Client.SetDefaultCollection("Tasks")
                                          .GetWhere("{'Status':'New'}");

                        if (query.Any())
                        {
                            query.OpenForm();
                        }
                        else
                        {
                            MessageBox("We don't have any new available tasks. Please, check later.", MessageBoxButtonType.Ok);
                        }

                        break;
                    }
                case "MyTasks":
                    {
                        var query = Client.SetDefaultCollection("Tasks")
                                          .GetWhere("{'Who':@UserName}")
                                          .OrWhere("{'Accepts':[{'Who':@UserName}]}");

                        if (query.Any())
                        {
                            query.OpenForm();
                        }
                        else
                        {
                            MessageBox("You don't have any acceptances for tasks.", MessageBoxButtonType.Ok);
                        }

                        break;
                    }
                case "UserTasks":
                    {
                        var user = Client.SetDefaultCollection("Users")
                                         .GetWhere(eventInfo.AttrPath)
                                         .Value("{'Name':$}");

                        var query = Client.SetDefaultCollection("Tasks")
                                          .GetWhere("{'Who':@UserName}")
                                          .OrWhere("{'Accepts':[{'Who':@User}]}", user);

                        if (query.Any())
                        {
                            query.OpenForm();
                        }
                        else
                        {
                            MessageBox("User has no any completed tasks yet.", MessageBoxButtonType.Ok);
                        }

                        break;
                    }
                case "CompletedTasks":
                    {
                        var query = Client.SetDefaultCollection("Tasks")
                                          .GetWhere("{'Status':'Completed'}");

                        if (query.Any())
                        {
                            query.OpenForm();
                        }
                        else
                        {
                            MessageBox("We don't have completed tasks yet.", MessageBoxButtonType.Ok);
                        }

                        break;
                    }
                case "NewTask":
                    {
                        Client.SetDefaultCollection("NewTask")
                              .WantCreateNewDocumentFor("Tasks")
                              .OpenForm(result =>
                              {
                                  if (result.Result)
                                  {
                                      MessageBox("Thank you. Your task is created.", MessageBoxButtonType.Ok);
                                  }
                              });

                        break;
                    }
                case "NewCustomerRate":
                    {
                        var userID = GetCustomerUserID(eventInfo.DocID);

                        var task = GetTask(eventInfo.DocID);

                        if (Client.SetDefaultCollection("Users")
                                  .GetDoc(userID)
                                  .AndWhere("{'Rates':[{'TaskNumber':@TaskNumber,'Who':@UserName}]}", task.TaskNumber)
                                  .Any())
                        {
                            MessageBox("Customer already rated for this task", MessageBoxButtonType.Ok);
                        }
                        else
                        {
                            Client.SetDefaultCollection("NewRate")
                                  .WantCreateNewDocumentForArray("Users",
                                                                 "{'Rates':[$]}",
                                                                 userID)
                                  .ExtendDocument(DQL("{'TaskNumber':@TaskNumber}", task.TaskNumber))
                                  .OpenForm(result =>
                                  {
                                      if (result.Result)
                                      {
                                          MessageBox("Thank you. Customer was rated.", MessageBoxButtonType.Ok);
                                      }
                                  });
                        }

                        break;
                    }
                case "NewUserRate":
                    {
                        var userID = GetDemoUserID(eventInfo.AttrPath);

                        var task = GetTask(eventInfo.DocID);

                        if (Client.SetDefaultCollection("Users")
                                 .GetDoc(userID)
                                 .AndWhere("{'Rates':[{'TaskNumber':@TaskNumber}]}", task.TaskNumber)
                                 .Any())
                        {
                            MessageBox("Developer already rated for this task", MessageBoxButtonType.Ok);
                        }
                        else
                        {
                            Client.SetDefaultCollection("NewRate")
                                  .WantCreateNewDocumentForArray("Users",
                                                                 "{'Rates':[$]}",
                                                                 userID)
                                  .ExtendDocument(DQL("{'TaskNumber':@TaskNumber}", task.TaskNumber))
                                  .OpenForm(result =>
                                  {
                                      if (result.Result)
                                      {
                                          MessageBox("Thank you. Developer was rated.", MessageBoxButtonType.Ok);
                                      }
                                  });
                        }

                        break;
                    }
                case "NewAccept":
                    {
                        var task = GetTask(eventInfo.DocID);

                        Client.SetDefaultCollection("NewAccept")
                              .WantCreateNewDocumentForArray("Tasks", "{'Accepts':[$]}", eventInfo.DocID)
                              .ExtendDocument(DQL("{'MinPrice':@MinPrice}", task.MinPrice))
                              .OpenForm(result =>
                              {
                                  if (result.Result)
                                  {
                                      MessageBox("Thank you. Customer will review your accept.", MessageBoxButtonType.Ok);

                                      eventInfo.Collection.ReloadData();
                                  }
                              });

                        break;
                    }
                case "NewDemo":
                    {
                        Client.SetDefaultCollection("NewDemo")
                              .WantCreateNewDocumentForArray("Tasks", "{'Demos':[$]}", eventInfo.DocID)
                              .OpenForm(result =>
                              {
                                  if (result.Result)
                                  {
                                      MessageBox("Thank you. Your demo is added.", MessageBoxButtonType.Ok);

                                      eventInfo.Collection.ReloadData();
                                  }
                              });

                        break;
                    }
                case "Reject":
                    {
                        MessageBox("Are you sure that you want to reject the Task ? It would decrease your rating.",
                                   "Reject task",
                                   MessageBoxButtonType.YesNo,
                                   result =>
                                   {
                                       if (result.Result)
                                       {
                                           RejectTask(eventInfo.DocID);

                                           MessageBox("Task was rejected.", MessageBoxButtonType.Ok);

                                           eventInfo.Collection.ReloadData();
                                       }
                                   });
                        break;
                    }
                case "AcceptDeveloper":
                    {
                        var developer = Client.SetDefaultCollection("Tasks")
                                              .GetWhere(eventInfo.AttrPath)
                                              .Value("{'Accepts':[{'Who':$}]}");

                        Client.SetDefaultCollection("Tasks")
                              .GetWhere(eventInfo.AttrPath)
                              .Update("{'Developer':@Developer,'Status':'InProgress'}", developer);

                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@Developer}", developer)
                              .Update("{'Accepted':Add(1)}");

                        MessageBox("Developer was accepted.", MessageBoxButtonType.Ok);

                        eventInfo.Collection.ReloadData();

                        break;
                    }
                case "AcceptDemo":
                    {
                        var developer = Client.SetDefaultCollection("Tasks")
                                              .GetWhere(eventInfo.AttrPath)
                                              .Value("{'Demos':[{'Who':$}]}");

                        Client.SetDefaultCollection("Tasks")
                              .GetWhere(eventInfo.AttrPath)
                              .Update("{'Developer':@Developer,'Status':'Completed'}", developer);

                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@Developer}", developer)
                              .Update("{'Completed':Add(1)}");

                        MessageBox("Demo was accepted.", MessageBoxButtonType.Ok);

                        eventInfo.Collection.ReloadData();

                        break;
                    }
                case "SendMessage":
                    {
                        var message = eventInfo.Collection
                                               .GetWhere(eventInfo.AttrPath)
                                               .Value("{'Demos':[{'Message':$}]}");

                        var avatar = Client.SetDefaultCollection("Users")
                                           .GetWhere("{'Name':@UserName}")
                                           .Value("{'Photo':$}");

                        eventInfo.Collection
                                 .GetWhere(eventInfo.AttrPath)
                                 .Update("{'Demos':[{'Chat':[Add,{'Avatar':@Avatar,'OnDate':@Now,'Who':@UserName,'Text':@Message}]}]}",
                                           avatar,
                                           message);

                        if (!string.IsNullOrEmpty(message))
                        {
                            Client.SetDefaultCollection("Tasks")
                                  .GetWhere(eventInfo.AttrPath)
                                  .Update("{'Demos':[{'Chat':[Add,{'Avatar':@Avatar,'OnDate':@Now,'Who':@UserName,'Text':@Message}]}]}", "", message);

                            eventInfo.Collection.ReloadData();
                        }
                        else
                        {
                            MessageBox("You try to send empty message");
                        }

                        break;
                    }
                case "Reports":
                    {
                        NotImplemented();

                        break;
                    }
                default:
                    return base.OnEventDimension(eventInfo);
            }

            return true;
        }

        public override void OnLogin(FormResult result)
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}