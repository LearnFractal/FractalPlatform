using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System;
using System.Linq;

namespace FractalPlatform.Examples.Applications.FreelanceResponse
{
    public class FreelanceResponseApplication : DashboardApplication
    {
        public TaskInfo GetTask(uint docID)
        {
            return DocsWhere("Tasks", docID).SelectOne<TaskInfo>();
        }

        private string GetDemoWho(AttrPath attrPath)
        {
            return DocsWhere("Tasks", attrPath).Value("{'Demos':[{'Who':$}]}");
        }

        private uint GetCustomerUserID(uint docID)
        {
            var task = GetTask(docID);

            return DocsWhere("Users", "{'Name':@Who}", task.Who).GetFirstID();
        }

        private uint GetDemoUserID(AttrPath attrPath)
        {
            var who = GetDemoWho(attrPath);

            return DocsWhere("Users", "{'Name':@Who}", who)
                         .GetFirstID();
        }

        private void RejectTask(uint docID)
        {
            var userID = GetCustomerUserID(docID);

            DocsWhere("Users", userID)
                  .Update("{'Rates':[Add,{'Who':'Bot','OnDate':@Now,'Comment':'Task rejected','Stars':1}]}");

            DocsWhere("Tasks", docID)
                  .Update("{'Status':'Rejected'}");
        }

        public override bool OnTimerDimension(TimerInfo timerInfo)
        {
            if (timerInfo.Action == "Expired")
            {
                var expired = DateTime.Now.AddDays(-3);

                var docIDs = DocsWhere("Tasks", "{'Status':Any('New','InProgress'),'EndDate':Less(@Expired)}",
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
                        return "T-" + (DocsOf("Tasks")
                                             .Count() + 1).ToString("00000");
                    }
                case "Rating":
                    {
                        var count = DocsWhere("Users", computedInfo.AttrPath)
                                          .Count("{'Rates':[{'Stars':$}]}");

                        var sum = DocsWhere("Users", computedInfo.AttrPath)
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
                        DocsOf("Users")
                              .OpenForm();

                        break;
                    }
                case "Tasks":
                    {
                        var query = DocsWhere("Tasks", "{'Status':'New'}");

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
                        var query = DocsWhere("Tasks", "{'Who':@UserName}")
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
                        var user = DocsWhere("Users", eventInfo.AttrPath)
                                         .Value("{'Name':$}");

                        var query = DocsWhere("Tasks", "{'Who':@UserName}")
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
                        var query = DocsWhere("Tasks", "{'Status':'Completed'}");

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
                        CreateNewDocFor("NewTask", "Tasks")
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

                        if (DocsWhere("Users", userID)
                                  .AndWhere("{'Rates':[{'TaskNumber':@TaskNumber,'Who':@UserName}]}", task.TaskNumber)
                                  .Any())
                        {
                            MessageBox("Customer already rated for this task", MessageBoxButtonType.Ok);
                        }
                        else
                        {
                            CreateNewDocForArray("NewRate",
                                                 "Users",
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

                        if (DocsWhere("Users", userID)
                                 .AndWhere("{'Rates':[{'TaskNumber':@TaskNumber}]}", task.TaskNumber)
                                 .Any())
                        {
                            MessageBox("Developer already rated for this task", MessageBoxButtonType.Ok);
                        }
                        else
                        {
                            CreateNewDocForArray("NewRate", "Users", "{'Rates':[$]}", userID)
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

                        CreateNewDocForArray("NewAccept", "Tasks", "{'Accepts':[$]}", eventInfo.DocID)
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
                        CreateNewDocForArray("NewDemo", "Tasks", "{'Demos':[$]}", eventInfo.DocID)
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
                        var developer = DocsWhere("Tasks", eventInfo.AttrPath)
                                              .Value("{'Accepts':[{'Who':$}]}");

                        DocsWhere("Tasks", eventInfo.AttrPath)
                              .Update("{'Developer':@Developer,'Status':'InProgress'}", developer);

                        DocsWhere("Users", "{'Name':@Developer}", developer)
                              .Update("{'Accepted':Add(1)}");

                        MessageBox("Developer was accepted.", MessageBoxButtonType.Ok);

                        eventInfo.Collection.ReloadData();

                        break;
                    }
                case "AcceptDemo":
                    {
                        var developer = DocsWhere("Tasks", eventInfo.AttrPath)
                                              .Value("{'Demos':[{'Who':$}]}");

                        DocsWhere("Tasks", eventInfo.AttrPath)
                              .Update("{'Developer':@Developer,'Status':'Completed'}", developer);

                        DocsWhere("Users", "{'Name':@Developer}", developer)
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

                        var avatar = DocsWhere("Users", "{'Name':@UserName}")
                                           .Value("{'Photo':$}");

                        if (!string.IsNullOrEmpty(message))
                        {
                            DocsWhere("Tasks", eventInfo.AttrPath)
                                  .Update("{'Demos':[{'Chat':[Add,{'Avatar':@Avatar,'OnDate':@Now,'Who':@UserName,'Text':@Message}]}]}",
                                          avatar,
                                          message);

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
            FirstDocOf("Dashboard")
                  .OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}