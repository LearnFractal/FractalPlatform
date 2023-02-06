using BigDoc.Client;
using BigDoc.Client.App;
using BigDoc.Client.App.Chart;
using BigDoc.Client.DQL;
using BigDoc.Client.SQL;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;
using BigDoc.Database.Engine.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace FractalPlatform.Applications.Portal
{
    public class PortalApplication : DashboardApplication
    {
        private enum SeverityType
        {
            Important,
            NotImportant
        }

        private string _currentProject;

        private string _currentChatName;

        private Collection _currentChatDashboard;

        public List<string> AppNames { get; set; }

        //public UserSessions UserSessions { get; set; }

        //public AutoTesting AutoTesting { get; set; }

        protected override void OnLogin(FormResult result)
        {
            _currentProject = Client.SetDefaultCollection("Users")
                                    .GetWhere("{'Name':@UserName}")
                                    .Value("{'CurrentProject':$}");

            AddNotificationToUser("Login", SeverityType.NotImportant, "User logged in");

            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
        }

        protected override void OnRegister(FormResult result)
        {
            if (result.Collection
                  .GetDoc(result.DocID)
                  .GetWhere("{'Role':{'I am Developer':true}}")
                  .Any("{'Name':$}"))
            {
                Client.SetDefaultCollection("FinishRegistration")
                  .GetWhere("{'Role':'Developer'}")
                  .OpenForm();

                return;
            }

            if (result.Collection
                  .GetDoc(result.DocID)
                  .GetWhere("{'Role':{'I am Customer':true}}")
                  .Any("{'Name':$}"))
            {
                Client.SetDefaultCollection("FinishRegistration")
                  .GetWhere("{'Role':'Customer'}")
                  .OpenForm();

                return;
            }

            Client.SetDefaultCollection("FinishRegistration")
                  .GetWhere("{'Role':'Guest'}")
                  .OpenForm();
        }

        public void Stories()
        {
            if (EnsureCurrentProject())
            {
                Client.SetDefaultCollection("Stories")
                  .GetWhere("{'ProjectName':@CurrentProject}", _currentProject)
                  .WantModifyExistingDocuments()
                  .OpenForm();
            }
        }

        public void MyStories()
        {
            if (EnsureCurrentProject())
            {
                Client.SetDefaultCollection("Stories")
                  .GetWhere("{'ProjectName':@CurrentProject,'AssignedTo':@UserName}", _currentProject)
                  .WantModifyExistingDocuments()
                  .OpenForm();
            }
        }

        public void MyProjects()
        {
            Client.SetDefaultCollection("Projects")
                  .GetWhere("{'Owner':@UserName}")
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        private bool EnsureCurrentProject()
        {
            if (string.IsNullOrEmpty(_currentProject))
            {
                MessageBox("You don't have current project.");

                return false;
            }
            else
            {
                return true;
            }
        }

        public void Sprints()
        {
            if (EnsureCurrentProject())
            {
                Client.SetDefaultCollection("Sprints")
                  .GetWhere("{'ProjectName':@CurrentProject}", _currentProject)
                  .WantModifyExistingDocuments()
                  .OpenForm();
            }
        }

        public void NewSprint()
        {
            if (EnsureCurrentProject())
            {
                Client.SetDefaultCollection("NewSprint")
                  .WantCreateNewDocumentFor("Sprints")
                  .OpenForm();
            }
        }

        public void Projects()
        {
            Client.SetDefaultCollection("Projects")
                  .GetAll()
                  .OpenForm();
        }

        public void Templates()
        {
            Client.SetDefaultCollection("Templates")
                  .GetAll()
                  .OpenForm();
        }

        public void Environments()
        {
            Client.SetDefaultCollection("Environments")
                  .GetAll()
                  .OpenForm();
        }

        public void Specifications()
        {
            Client.SetDefaultCollection("Specifications")
                  .GetAll()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        public void Articles()
        {
            Client.SetDefaultCollection("Articles")
                  .GetAll()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        public void Raiting()
        {
            MessageBox("Not Implemented");

        }

        public void ChangePassword()
        {
            Client.SetDefaultCollection("ChangePassword")
                  .GetAll()
                  .OpenForm(null, null, result =>
                  {
                      if (result.Result)
                      {
                          var newPassword = result.Collection
                                                  .GetDoc(Constants.FIRST_DOC_ID)
                                                  .Value("{'NewPassword':$}");

                          Client.GetWhere("{'Name':@UserName}")
                                .Update("{'Password':@Password}", newPassword);

                          MessageBox("Password successfuly updated.");
                      }
                  });
        }

        public void Votes()
        {
            var userInfo = GetUserInfo();

            Client.SetDefaultCollection("Votes")
                  .GetWhere("{'Projects':[Any,@MyProjects]}", userInfo.ProjectsAsString)
                  .OrWhere("{'Teams':[Any,@MyTeams]}", userInfo.TeamsAsString)
                  .OrWhere("{'Users':[Any,@UserName]}")
                  .AndWhere("{'StartDate':LessOrEqual(@Now),'EndDate':GreatOrEqual(@Now)}")
                  .OpenForm("{'Question':$,'StartDate':$,'EndDate':$,'Important':$,'Type':$}");
        }

        public void NewProject()
        {
            Client.SetDefaultCollection("NewProject")
                  .WantCreateNewDocumentFor("Projects")
                  .OpenForm();
        }

        public void NewEnvironment()
        {
            Client.SetDefaultCollection("NewEnvironment")
                  .WantCreateNewDocumentFor("Environments")
                  .OpenForm();
        }

        private class DBConsoleInfo
        {
            public string QueryLanguage { get; set; }

            public string Database { get; set; }

            public string Query { get; set; }
        }

        public void ExecutDBConsole(FormResult result)
        {
            if (result.Result)
            {
                var info = result.Collection
                                 .GetFirstDoc()
                                 .SelectOne<DBConsoleInfo>();

                Client.SetDefaultDatabase(info.Database);

                DataTable dt;

                if (info.QueryLanguage == "SQL")
                {
                    var executor = new SQLExecutor(Client);

                    dt = executor.Run(info.Query);

                }
                else if (info.QueryLanguage == "DQL")
                {
                    var executor = new DQLExecutor(Client);

                    dt = executor.Run(info.Query);
                }
                else
                {
                    throw new InvalidOperationException();
                }

                var collection = result.Collection;

                BaseExecutor.CopyToStorage(Context,
                                           collection.DocumentStorage,
                                           "Result",
                                           Constants.FIRST_DOC_ID,
                                           dt);

                FormBuilder.OpenForm(Context,
                                     collection,
                                     Constants.FIRST_DOC_ID,
                                     null,
                                     ExecutDBConsole);
            }
        }

        public void DBConsole()
        {
            Client.SetDefaultCollection("DBConsole")
                  .GetFirstDoc()
                  .OpenForm(ExecutDBConsole);
        }

        public List<StatInfo> GetStats(string appName,
                                   DateTime startDate)
        {
            var baseDate = new DateTime(1990, 1, 1);
            var nowDate = new DateTime(startDate.Year,
                                       startDate.Month,
                                       startDate.Day,
                                       startDate.Hour,
                                       0,
                                       0);

            var startPeriod = Convert.ToUInt32(nowDate.Subtract(baseDate).TotalHours);

            var currStats = Client.SetDefaultCollection("Stats")
                                  .GetWhere("{'AppName':@AppName,'Logs':[{'Period':GreatOrEqual(@StartPeriod)}]}",
                                            appName,
                                            startPeriod)
                                  .Select<StatInfo>("{'Logs':[!{'Period':$,'Requests':$,'Errors':$,'TimeInMsec':$}]}");

            uint avgTimeInMsec;

            if (currStats.Length > 0)
            {
                avgTimeInMsec = Convert.ToUInt32(currStats.Average(x => x.AverageTimeInMsec));
            }
            else
            {
                avgTimeInMsec = 0;
            }

            var stats = new List<StatInfo>();

            for(var currDate = startDate; currDate <= DateTime.Now; currDate = currDate.AddHours(1))
            {
                var currPeriod = Convert.ToUInt32(currDate.Subtract(baseDate).TotalHours);

                var currStat = currStats.Where(x => x.Period == currPeriod).FirstOrDefault();

                if(currStat != null)
                {
                    stats.Add(currStat);
                }
                else
                {
                    stats.Add(new StatInfo { Period = currPeriod, TimeInMsec = avgTimeInMsec });
                }
            }

            return stats;
        }

        public DateTime GetStatDate(uint period)
        {
            var baseDate = new DateTime(1990, 1, 1);

            return baseDate.AddHours(period);
        }

        public void EnvironmentReport(string appName, string period)
        {
            var dbPath = Path.Combine(this.Instance.WorkingFolder, appName);

            var freeKb = 2 * 1024 * 1024;

            long dbUsedKb;

            if (Directory.Exists(dbPath))
            {
                dbUsedKb = new DirectoryInfo(dbPath)
                        .EnumerateFiles("*", SearchOption.AllDirectories)
                        .Sum(file => file.Length) / 1024;
            }
            else
            {
                dbUsedKb = 0;
            }

            var filesPath = Path.Combine(this.Instance.WorkingFolder.Replace("../Databases", "files"), appName);

            long filesUsedKb;

            if (Directory.Exists(filesPath))
            {
                filesUsedKb = new DirectoryInfo(filesPath)
                        .EnumerateFiles("*", SearchOption.AllDirectories)
                        .Sum(file => file.Length) / 1024;
            }
            else
            {
                filesUsedKb = 0;
            }

            var activityReport = new BarChartInfo
            {
                Title = new TitleChartInfo
                {
                    Name = "Activity",
                    X = "Period",
                    Y = "Count"
                }
            };

            var storageReport = new PieChartInfo
            {
                Title = "Storage",
                Parts = new List<PartChartInfo>
                    {
                        new PartChartInfo
                        {
                            Name = "kb DB Used",
                            Value = (uint)dbUsedKb
                        },
                        new PartChartInfo
                        {
                            Name = "kb Files Used",
                            Value = (uint)filesUsedKb
                        },
                        new PartChartInfo
                        {
                            Name = "kb Free",
                            Value = (uint)freeKb
                        }
                    }
            };

            var perfLine = new LineChartInfo { Points = new List<PointChartInfo>() };

            var perfReport = new LineGraphsChartInfo
            {
                Lines = new List<LineChartInfo> { perfLine },
                Title = new TitleChartInfo
                {
                    Name = "Performance",
                    Y = "Time (ms)"
                }
            };

            var errorsReport = new BarChartInfo
            {
                Title = new TitleChartInfo
                {
                    Name = "Errors",
                    X = "Period",
                    Y = "Count"
                }
            };

            DateTime startDate;
            DateTime endDate;

            uint periodInHours;

            Func<DateTime, string> getName;

            if (period == "LastDay")
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;

                periodInHours = 2;
                getName = x => x.ToString("hh:mm");

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Time";
            }
            else if (period == "LastWeek")
            {
                startDate = DateTime.Now.AddDays(-7);
                endDate = DateTime.Now;

                periodInHours = 24;
                getName = x => x.DayOfWeek.ToString().Substring(0, 3);

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Week";
            }
            else if (period == "LastMonth")
            {
                startDate = DateTime.Now.AddMonths(-1);
                endDate = DateTime.Now;

                periodInHours = 3 * 24;
                getName = x => x.Day.ToString("00");

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Month";
            }
            else if (period == "LastYear")
            {
                startDate = DateTime.Now.AddYears(-1);
                endDate = DateTime.Now;

                periodInHours = 30 * 24;
                getName = x => x.ToString("MMMM").Substring(0, 3);

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Year";
            }
            else
            {
                throw new NotImplementedException();
            }

            activityReport.Columns = GetStats(appName, startDate)
                                     .GroupBy(x => x.Period - x.Period % periodInHours)
                                     .Select(x => new PartChartInfo
                                     {
                                         Name = getName(GetStatDate(x.Key)),
                                         Value = (uint)x.Sum(y => y.Requests)
                                     }).ToList();

            errorsReport.Columns = GetStats(appName, startDate)
                                   .GroupBy(x => x.Period - x.Period % periodInHours)
                                   .Select(x => new PartChartInfo
                                   {
                                       Name = getName(GetStatDate(x.Key)),
                                       Value = (uint)x.Sum(y => y.Errors)
                                   }).ToList();

            uint i = 0;

            perfLine.Points = GetStats(appName, startDate)
                              .GroupBy(x => x.Period - x.Period % periodInHours)
                              .Select(x => new PointChartInfo
                              {
                                  X = i++,
                                  Y = (uint)x.Average(y => y.AverageTimeInMsec)
                              }).ToList();

            Client.SetDefaultCollection("EnvironmentReport")
                  .GetFirstDoc()
                  .ToCollection()
                  .ExtendDocument(DQL("{'AppName':@Project,'Period':@Period}", appName, period))
                  .DeleteByParent("Reports")
                  .Merge(activityReport.ToJson(),
                         new AttrPath("Reports", "Activity", "Report"))
                  .Merge(storageReport.ToJson(),
                         new AttrPath("Reports", "Storage", "Report"))
                  .Merge(perfReport.ToJson(),
                         new AttrPath("Reports", "Performance", "Report"))
                  .Merge(errorsReport.ToJson(),
                         new AttrPath("Reports", "Errors", "Report"))
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          var appName = result.Collection
                                              .GetFirstDoc()
                                              .Value("{'AppName':$}");

                          var period = result.Collection
                                              .GetFirstDoc()
                                              .Value("{'Period':$}");

                          EnvironmentReport(appName, period);
                      }
                  });
        }

        public void NewSpecification()
        {
            Client.SetDefaultCollection("NewSpecification")
                  .WantCreateNewDocumentFor("Specifications")
                  .OpenForm();
        }

        public void CurrentSprint()
        {
            if (EnsureCurrentProject())
            {
                Client.SetDefaultCollection("Sprints")
                      .GetWhere("{'ProjectName':@CurrentProject,'StartDate':LessOrEqual(@Now),'EndDate':GreatOrEqual(@Now)}", _currentProject)
                      .OpenForm();
            }
        }

        public void Users()
        {
            Client.SetDefaultCollection("Users")
                  .GetAll()
                  .OpenForm();
        }

        public void Teams()
        {
            Client.SetDefaultCollection("Teams")
                  .GetAll()
                  .OpenForm();
        }

        public void NewVacancy()
        {
            Client.SetDefaultCollection("NewVacancy")
                  .WantCreateNewDocumentFor("Vacancies")
                  .OpenForm();
        }

        public void Vacancies()
        {
            Client.SetDefaultCollection("Vacancies")
                  .GetAll()
                  .OpenForm("{'VacancyNumber':$,'Position':$,'Team':$,'Project':$,'Skills':$,'Responsibilities':$}");
        }

        public void Courses()
        {
            Client.SetDefaultCollection("Courses")
                  .GetAll()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        public void Exams()
        {
            Client.SetDefaultCollection("Exams")
                  .GetAll()
                  .OpenForm();
        }

        public void Calendar()
        {
            //1. Read user
            var userInfo = GetUserInfo();

            //2. Read meetings
            var meetings = Client.SetDefaultCollection("Meetings")
                                 .GetWhere("{'Projects':[Any,@MyProjects]}", userInfo.ProjectsAsString)
                                 .OrWhere("{'Teams':[Any,@MyTeams]}", userInfo.TeamsAsString)
                                 .OrWhere("{'Users':[Any,@UserName]}")
                                 .AndWhere("{'StartDate':LessOrEqual(@Now),'EndDate':GreatOrEqual(@Now)}")
                                 .Select<MeetingInfo>();

            //3. Add participants
            foreach (var meeting in meetings)
            {
                var meetingProjects = meeting.Projects;

                var users = Client.SetDefaultCollection("Users")
                                  .GetWhere("{'Projects':[Any,@MyProjects]}", meeting.Projects)
                                  .OrWhere("{'Teams':[Any,@MyTeams]}", meeting.Teams)
                                  .OrWhere("{'Name':@UserName}")
                                  .Select<UserInfo>();

                meeting.Participants = users.Select(x => x.Name).ToList();
            }


            //4. Get actual meetings
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);

            var currDate = startDate;

            var sb = new StringBuilder();

            sb.Append("{'Calendar':[");

            meetings = meetings.OrderBy(x => x.StartTime)
                               .ToArray();

            var bFirst1 = true;

            while (currDate <= endDate)
            {
                var currDateStr = currDate.ToString("yyyy-MM-dd");

                if (!bFirst1)
                {
                    sb.Append(",");
                }

                bFirst1 = false;

                sb.Append("{")
                  .Append($"'Date':'{currDateStr}',")
                  .Append($"'Day':'{currDate.DayOfWeek.ToString()}',")
                  .Append("'Meetings':[");

                var bFirst2 = true;

                foreach (var meeting in meetings)
                {
                    if (!meeting.ExceptDates.Contains(currDateStr))
                    {
                        bool addMeeting = false;

                        if (meeting.Period.Once)
                        {
                            if (meeting.StartDate == currDateStr)
                            {
                                addMeeting = true;
                            }
                        }
                        else if (meeting.Period.EveryDay)
                        {
                            addMeeting = true;
                        }
                        else if (meeting.Period.EveryWorkingDay)
                        {
                            if (currDate.DayOfWeek != DayOfWeek.Saturday &&
                                currDate.DayOfWeek != DayOfWeek.Sunday)
                            {
                                addMeeting = true;
                            }
                        }
                        else if (meeting.Period.EveryWeekAt == currDate.DayOfWeek.ToString())
                        {
                            addMeeting = true;
                        }
                        else if (meeting.Period.EveryPeriodInDays > 0)
                        {
                            var meetingStartDate = DateTime.ParseExact(meeting.StartDate, "yyyy-MM-dd", null);

                            var days = (int)currDate.Subtract(meetingStartDate).TotalDays;

                            if (days % meeting.Period.EveryPeriodInDays == 0)
                            {
                                addMeeting = true;
                            }
                        }
                        else
                        {
                            addMeeting = false;
                        }

                        if (addMeeting)
                        {
                            var calendarMeeting = new CalendarMeetingInfo
                            {
                                Date = currDate.ToString("yyyy-MM-dd"),
                                Day = currDate.DayOfWeek.ToString(),
                                StartTime = meeting.StartTime,
                                EndTime = meeting.EndTime,
                                Title = meeting.Title,
                                Link = meeting.Link,
                                Description = meeting.Description,
                                Participants = meeting.Participants
                            };

                            if (!bFirst2)
                            {
                                sb.Append(",");
                            }

                            bFirst2 = false;

                            sb.Append(JsonConvert.SerializeObject(calendarMeeting));
                        }
                    }
                }

                sb.Append("]}");

                currDate = currDate.AddDays(1);
            }

            sb.Append("]}");

            //5. Show form
            var collection = new Collection("Calendar", sb.ToString());

            collection.SetDimension(DimensionType.UI, "{'Enabled':false}")
                      .OpenForm(Constants.FIRST_DOC_ID);
        }

        public void NewMeeting()
        {
            Client.SetDefaultCollection("NewMeeting")
                  .WantCreateNewDocumentFor("Meetings")
                  .OpenForm();
        }

        public void Meetings()
        {
            Client.SetDefaultCollection("Meetings")
                  .GetWhere("{'Who':@UserName}")
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        private UserInfo GetUserInfo()
        {
            return Client.SetDefaultCollection("Users")
                         .GetWhere("{'Name':@UserName}")
                         .Select<UserInfo>()[0];
        }

        private ChatInfo GetChatInfo(string name)
        {
            return Client.SetDefaultCollection("Chats")
                         .GetWhere("{'Name':@Name}", name)
                         .Select<ChatInfo>()[0];
        }

        private NewsInfo GetNewsInfo(string title)
        {
            return Client.SetDefaultCollection("News")
                         .GetWhere("{'Title':@Title}", title)
                         .Select<NewsInfo>()[0];
        }

        private MeetingInfo GetMeetingInfo(string title)
        {
            return Client.SetDefaultCollection("Meetings")
                         .GetWhere("{'Title':@Title}", title)
                         .Select<MeetingInfo>()[0];
        }

        private VoteInfo GetVoteInfo(string question)
        {
            return Client.SetDefaultCollection("Votes")
                         .GetWhere("{'Question':@Question}", question)
                         .Select<VoteInfo>()[0];
        }

        public void News()
        {
            var userInfo = GetUserInfo();

            Client.SetDefaultCollection("News")
                  .GetWhere("{'Projects':[Any,@MyProjects]}", userInfo.ProjectsAsString)
                  .OrWhere("{'Teams':[Any,@MyTeams]}", userInfo.TeamsAsString)
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        public void Notifications()
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere("{'Name':@UserName}")
                  .OrderByDesc("{'Notification':{'Messages':[{'OnDate':$}]}}")
                  .SetDimension(DimensionType.UI, "{'Style':'Add:false;Del:false;Save:false;PrevPage:false;NextPage:false'}")
                  .OpenForm("{'Notification':!{'Messages':[{'OnDate':$,'Section':$,'Severity':$,'Text':$}]}}");
        }

        public void NewChat()
        {
            Client.SetDefaultCollection("NewChat")
                  .WantCreateNewDocumentFor("Chats")
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          _currentChatDashboard.ReloadData();
                      }
                  });
        }

        public void EditChats()
        {
            Client.SetDefaultCollection("Chats")
                  .GetWhere("{'Who':@UserName}")
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        private Collection CreateChatDashboard(string name = null)
        {
            //1. Read user
            var userInfo = GetUserInfo();

            var chats = Client.SetDefaultCollection("Chats")
                              .GetWhere("{'Projects':[Any,@MyProjects]}", userInfo.ProjectsAsString)
                              .OrWhere("{'Teams':[Any,@MyTeams]}", userInfo.TeamsAsString)
                              .OrWhere("{'Users':[Any,@UserName]}")
                              .Select<ChatInfo>("{'Name':$}");

            //3. Show form
            var documentJson =
            @"{
                'Menu': @Menu,
                'Messages':@Messages,
                'CurrentChatName':'@CurrentChatName',
                'Message':'',
                'SendMessage':'Send Message',
                'NewChat':'New Chat',
                'EditChats':'Edit Chats'
            }";

            var menu = new StringBuilder();
            menu.Append('{');

            var menuIndex = 0;

            foreach (var menuItem in chats)
            {
                if (menuIndex > 0)
                    menu.Append(',');

                menu.Append($"'menu{menuIndex}':'{menuItem.Name}'");

                menuIndex++;
            }

            menu.Append('}');

            documentJson = documentJson.Replace("@Menu", menu.ToString());

            if (name == null)
            {
                name = chats[0].Name;
            }

            chats = Client.SetDefaultCollection("Chats")
                          .GetWhere("{'Name':@Name}", name)
                          .Select<ChatInfo>();

            documentJson = documentJson.Replace("@Messages", JsonConvert.SerializeObject(chats[0].Messages));
            documentJson = documentJson.Replace("@CurrentChatName", name);

            var uiJson =
            @"{
                'Layout':'ChatForm',
                'MobileLayout':'ChatForm',
                'Style':'Save:Refresh',
                'Menu': {'ControlType':'Button','LayoutLocation':'Menu','Style':'Type:Link'},
                'Messages': {'ControlType':'Messages','LayoutLocation':'Messages','Enabled':false},
                'Message': {'ControlType':'TextBox','LayoutLocation':'Message','Width':400},
                'SendMessage':{'ControlType':'Button','Width':150,'LayoutLocation':'SendMessage'},
                'NewChat':{'ControlType':'Button','Width':200,'LayoutLocation':'NewChat'},
                'EditChats':{'ControlType':'Button','Width':200,'LayoutLocation':'EditChats'}
            }";

            string eventJson =
            @"{
                'Menu': {'OnClick':'ShowChat'},
                'SendMessage':{'OnClick':'SendChatMessage'},
                'NewChat':{'OnClick':'NewChat'},
                'EditChats':{'OnClick':'EditChats'}
            }";

            string validationJson =
            @"{
                'Message': {'MinLen':3,'MaxLen':256}
            }";

            var collection = new Collection("ChatDashboard", documentJson);

            collection.SetDimension(DimensionType.UI, uiJson)
                      .SetDimension(DimensionType.Event, eventJson)
                      .SetDimension(DimensionType.Validation, validationJson);

            return collection;
        }

        public void Chats()
        {
            _currentChatDashboard = CreateChatDashboard(_currentChatName);

            _currentChatDashboard.SetReloadDataFunc(() => CreateChatDashboard(_currentChatName).DocumentStorage);

            _currentChatDashboard.OpenForm(result =>
                                  {
                                      if(result.Result)
                                      {
                                          Chats();
                                      }
                                      else
                                      {
                                          _currentChatName = null;
                                      }
                                  });
        }

        public void ShowChat(string name)
        {
            _currentChatName = name;

            _currentChatDashboard.ReloadData();
        }

        public void SendChatMessage()
        {
            var message = _currentChatDashboard.GetDoc(Constants.FIRST_DOC_ID)
                                               .Value("{'Message':$}");

            if (string.IsNullOrEmpty(message))
                return;

            _currentChatDashboard.GetDoc(Constants.FIRST_DOC_ID)
                                 .Update("{'Message':''}");

            var currentChatName = _currentChatDashboard.GetDoc(Constants.FIRST_DOC_ID)
                                                       .Value("{'CurrentChatName':$}");

            Client.SetDefaultCollection("Chats")
                  .GetWhere("{'Name':@Name}", currentChatName)
                  .Update("{'Messages':[Add,{'OnDate':@Now,'Who':@UserName,'Text':@Message}]}", message);

            _currentChatDashboard.ReloadData();

            AddNotificationToUser("Chat",
                            SeverityType.Important,
                            string.Format("Message was sent in {0} chat", currentChatName));
        }

        public void NewStory()
        {
            Client.SetDefaultCollection("NewStory")
                  .WantCreateNewDocumentFor("Stories")
                  .OpenForm();
        }

        public void NewTask(uint docID)
        {
            Client.SetDefaultCollection("NewTask")
                  .WantCreateNewDocumentForArray("Stories", "{'Tasks':[$]}", docID)
                  .OpenForm();
        }

        public void NewDefect(uint docID)
        {
            Client.SetDefaultCollection("NewDefect")
                  .WantCreateNewDocumentForArray("Stories", "{'Defects':[$]}", docID)
                  .OpenForm();
        }

        public void NewComment(string collName, uint docID)
        {
            Client.SetDefaultCollection("NewComment")
                  .WantCreateNewDocumentForArray(collName, "{'Comments':[$]}", docID)
                  .OpenForm();
        }

        public void NewTaskOrDefectComment(AttrPath attrPath, uint docID)
        {
            attrPath.RemoveLastPath()
                    .AddPath("Comments")
                    .AddIndex();

            Client.SetDefaultCollection("NewComment")
                  .WantCreateNewDocumentForArray("Stories", attrPath, docID)
                  .OpenForm();
        }

        public void NewArticle()
        {
            Client.SetDefaultCollection("NewArticle")
                  .WantCreateNewDocumentFor("Articles")
                  .OpenForm();
        }

        public void NewCandidate(uint docID)
        {
            Client.SetDefaultCollection("NewCandidate")
                  .WantCreateNewDocumentForArray("Vacancies", "{'Candidates':[$]}", docID)
                  .OpenForm();
        }

        public void NewNews()
        {
            Client.SetDefaultCollection("NewNews")
                  .WantCreateNewDocumentFor("News")
                  .OpenForm();
        }

        private LineGraphsChartInfo GetRemainedStoriesReport(string sprintNumber)
        {
            var sprintInfo = Client.SetDefaultCollection("Sprints")
                                   .GetWhere("{'SprintNumber':@SprintNumber}", sprintNumber)
                                   .SelectOne<SprintInfo>();

            uint prevRemained = 0;

            uint velocityInDay = 0;

            if (sprintInfo.RemainedHistory.Any())
            {
                prevRemained = sprintInfo.RemainedHistory.Max(x => x.RemainedTasks);

                var minRemained = sprintInfo.RemainedHistory.Min(x => x.RemainedTasks);

                var restOfDays = sprintInfo.EndDate.Subtract(DateTime.Now).Days;

                if (restOfDays > 0)
                {
                    velocityInDay = (uint)((prevRemained - minRemained) / restOfDays);
                }
            }

            var beforeLine = new LineChartInfo
            {
                Name = "Before Days",
                Points = new List<PointChartInfo>()
            };

            var afterLine = new LineChartInfo
            {
                Name = "After Days",
                Points = new List<PointChartInfo>()
            };

            var days = 0U;

            for (var date = sprintInfo.StartDate; date <= sprintInfo.EndDate; date = date.AddDays(1), days++)
            {
                var onDate = date.ToShortDateString();

                var currRemained = sprintInfo.RemainedHistory.FirstOrDefault(x => x.OnDate == onDate);

                if (currRemained != null)
                {
                    beforeLine.Points.Add(new PointChartInfo { X = days, Y = currRemained.RemainedTasks });

                    prevRemained = currRemained.RemainedTasks;
                }
                else
                {
                    var point = new PointChartInfo { X = days, Y = prevRemained };

                    if (date < DateTime.Now)
                    {
                        beforeLine.Points.Add(point);
                    }
                    else
                    {
                        if(afterLine.Points.Count == 0)
                        {
                            beforeLine.Points.Add(point);
                        }

                        afterLine.Points.Add(point);
                    }

                    if (date > DateTime.Now && velocityInDay <= prevRemained)
                    {
                        prevRemained -= velocityInDay;
                    }
                }
            }

            var remainedStoriesReport = new LineGraphsChartInfo
            {
                Lines = new List<LineChartInfo> { beforeLine, afterLine },
                Title = new TitleChartInfo
                {
                    Name = "Remained Tasks",
                    Y = "Hours"
                }
            };

            return remainedStoriesReport;
        }

        public void Report(string currentUser)
        {
            if (EnsureCurrentProject())
            {
                var sprintNumber = Client.SetDefaultCollection("Sprints")
                                         .GetWhere("{'ProjectName':@CurrentProject,'StartDate':LessOrEqual(@Now),'EndDate':GreatOrEqual(@Now)}",
                                                    _currentProject)
                                         .Value("{'SprintNumber':$}");

                if(string.IsNullOrEmpty(sprintNumber))
                {
                    MessageBox("There is no current sprint. Did you miss start a Sprint ?", MessageBoxButtonType.Ok);

                    return;
                }

                Client.SetDefaultCollection("Stories");

                var userStoriesReport = new PieChartInfo
                {
                    Title = "User Stories",
                    Parts = new List<PartChartInfo>
                    {
                        new PartChartInfo
                        {
                            Name = "New (hrs)",
                            Value = Client.GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':Any('New','Active'),'AssignedTo':@CurrentUser}]}", sprintNumber, currentUser)
                                          .Sum("{'Tasks':[{'Estimated':$}]}")
                        },
                        new PartChartInfo
                        {
                            Name = "InProgress (hrs)",
                            Value = Client.GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':'InProgress','AssignedTo':@CurrentUser}]}", sprintNumber, currentUser)
                                          .Sum("{'Tasks':[{'Estimated':$}]}")
                        },
                        new PartChartInfo
                        {
                            Name = "Closed (hrs)",
                            Value = Client.GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':'Closed','AssignedTo':@CurrentUser}]}", sprintNumber, currentUser)
                                          .Sum("{'Tasks':[{'Estimated':$}]}")
                        }
                    }
                };

                var sprintStoriesReport = new PieChartInfo
                {
                    Title = "Sprint Stories",
                    Parts = new List<PartChartInfo>
                    {
                         new PartChartInfo
                        {
                            Name = "New (hrs)",
                            Value = Client.GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':Any('New','Active')}]}", sprintNumber)
                                          .Sum("{'Tasks':[{'Estimated':$}]}")
                        },
                        new PartChartInfo
                        {
                            Name = "InProgress (hrs)",
                            Value = Client.GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':'InProgress'}]}", sprintNumber)
                                          .Sum("{'Tasks':[{'Estimated':$}]}")
                        },
                        new PartChartInfo
                        {
                            Name = "Closed (hrs)",
                            Value = Client.GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':'Closed'}]}", sprintNumber)
                                          .Sum("{'Tasks':[{'Estimated':$}]}")
                        }
                    }
                };

                var remainedStoriesReport = GetRemainedStoriesReport(sprintNumber);

                Client.SetDefaultCollection("Report")
                      .GetFirstDoc()
                      .ToCollection()
                      .DeleteByParent("Reports")
                      .ExtendDocument(DQL("{'User':@CurrentUser}", currentUser))
                      .Merge(userStoriesReport.ToJson(),
                             new AttrPath("Reports", "UserStories"))
                      .Merge(sprintStoriesReport.ToJson(),
                             new AttrPath("Reports", "SprintStories"))
                      .Merge(remainedStoriesReport.ToJson(),
                             new AttrPath("Reports", "RemainedStories"))
                      .OpenForm(result =>
                      {
                          if (result.Result)
                          {
                              var currentUser = result.Collection
                                                      .GetFirstDoc()
                                                      .Value("{'User':$}");

                              Report(currentUser);
                          }
                      });
            }
        }

        public override bool OnOpenForm(FormInfo formInfo)
        {
            if (formInfo.Collection.Name == "Votes" &&
                formInfo.DocID != Constants.ANY_DOC_ID &&
                formInfo.AttrPath.IsEmpty)
            {
                if (Client.SetDefaultCollection("Votes")
                         .GetDoc(formInfo.DocID)
                         .AndWhere("{'Users':[Any,@UserName]}")
                         .Count("{'Question':$}") == 0)
                {
                    Client.SetDefaultCollection("NewVote")
                          .GetDoc(formInfo.DocID)
                          .WantMergeDocumentFor("Votes", formInfo.DocID)
                          .OpenForm(result =>
                          {
                              if (result.Result)
                              {
                                  Client.SetDefaultCollection("Votes")
                                        .GetDoc(formInfo.DocID)
                                        .Update("{'Users':[Add,@UserName]}");
                              }
                          });
                }
                else
                {
                    Client.SetDefaultCollection("Votes")
                          .GetDoc(formInfo.DocID)
                          .OpenForm();
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            switch (computedInfo.Variable)
            {
                case "NewStoryNumber":
                    {
                        var count = Client.SetDefaultCollection("Stories")
                                          .GetAll()
                                          .Count("{'StoryNumber':$}") + 1;

                        return $"S-{count.ToString("0000")}";
                    }
                case "NewSprintNumber":
                    {
                        var count = Client.SetDefaultCollection("Sprints")
                                          .GetAll()
                                          .Count("{'SprintNumber':$}") + 1;

                        return $"SP-{count.ToString("0000")}";
                    }
                case "NewMeetingNumber":
                    {
                        var count = Client.SetDefaultCollection("Meetings")
                                          .GetAll()
                                          .Count("{'MeetingNumber':$}") + 1;

                        return $"M-{count.ToString("0000")}";
                    }
                case "NewVacancyNumber":
                    {
                        var count = Client.SetDefaultCollection("Vacancies")
                                          .GetAll()
                                          .Count("{'VacancyNumber':$}") + 1;

                        return $"V-{count.ToString("0000")}";
                    }
                case "NewDeploymentKey":
                    {
                        return $"{Guid.NewGuid()}";
                    }
                case "TaskEstimated":
                    {
                        var count = Client.SetDefaultCollection("Stories")
                                          .GetDoc(computedInfo.DocID)
                                          .Sum("{'Tasks':[{'Estimated':$}]}");

                        return count.ToString();
                    }
                case "TaskRemained":
                    {
                        var count = Client.SetDefaultCollection("Stories")
                                          .GetDoc(computedInfo.DocID)
                                          .Sum("{'Tasks':[{'Remained':$}]}");

                        return count.ToString();
                    }
                case "CurrentProject":
                    {
                        if (_currentProject != null)
                            return $"{_currentProject}";
                        else
                            return "None";
                    }
                case "SprintDaysRemained":
                    {
                        var endDate = Client.SetDefaultCollection("Sprints")
                                            .GetDoc(computedInfo.DocID)
                                            .DateTimeValue("{'EndDate':$}");

                        if (DateTime.Now < endDate)
                        {
                            return endDate.Subtract(DateTime.Now).Days;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                case "SprintTotalDays":
                    {
                        var startDate = Client.SetDefaultCollection("Sprints")
                                            .GetDoc(computedInfo.DocID)
                                            .DateTimeValue("{'StartDate':$}");

                        var endDate = Client.SetDefaultCollection("Sprints")
                                            .GetDoc(computedInfo.DocID)
                                            .DateTimeValue("{'EndDate':$}");

                        return endDate.Subtract(startDate).Days;
                    }
                case "SprintStoryEstimated":
                    {
                        var sprintNumber = Client.SetDefaultCollection("Sprints")
                                                 .GetDoc(computedInfo.DocID)
                                                 .Value("{'SprintNumber':$}");

                        var sum = Client.SetDefaultCollection("Stories")
                                        .GetWhere("{'SprintNumber':@SprintNumber}", sprintNumber)
                                        .Sum("{'StoryEstimated':$}");

                        return sum;
                    }
                case "RemainedTasks":
                    {
                        var sprintNumber = Client.SetDefaultCollection("Sprints")
                                                 .GetDoc(computedInfo.DocID)
                                                 .Value("{'SprintNumber':$}");

                        var sum = Client.SetDefaultCollection("Stories")
                                        .GetWhere("{'SprintNumber':@SprintNumber,'Tasks':[{'Status':Any('New','Active','InProgress')}]}", sprintNumber)
                                        .Sum("{'Tasks':[{'Estimated':$}]}");

                        return sum;
                    }
                case "Avatar":
                    {
                        var photo = Client.SetDefaultCollection("Users")
                                           .GetWhere("{'Name':@UserName}")
                                           .Value("{'Photo':$}");

                        return photo;
                    }
                case "OldPassword":
                    {
                        if (Client.SetDefaultCollection("Users")
                                 .GetWhere("{'Name':@UserName, 'Password':@Password}",
                                           computedInfo.AttrValue.ToString())
                                 .Any())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case "Power":
                    {
                        //if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
                        //{
                        //    return "Electricity";
                        //}
                        //else
                        //{
                        //    var ts = TimeSpan.FromSeconds(SystemInformation.PowerStatus.BatteryLifeRemaining);
                        //    return $"Battery (Life: {ts.Hours} hours {ts.Minutes} minutes)";
                        //}

                        return "Electricity";
                    }
                case "Battery":
                    {
                        return "100%"; //$"{SystemInformation.PowerStatus.BatteryLifePercent * 100}%";
                    }
                case "Stories":
                case "Notifications":
                case "Articles":
                case "News":
                case "Chats":
                case "Meetings":
                case "Projects":
                case "Sprints":
                case "Teams":
                case "Vacancies":
                case "Specifications":
                case "Users":
                case "Votes":
                case "Environments":
                    {
                        return SectionLabelWithCountNotifications(computedInfo.Variable);
                    }
                default:
                    return base.OnComputedDimension(computedInfo);
            }
        }

        private List<string> GetAllAppNames()
        {
            return AppNames.Union(Instance.Databases.Select(x => x.Name))
                           .Distinct()
                           .OrderBy(x => x)
                           .ToList();
        }

        public override List<string> OnEnumDimension(EnumInfo enumInfo)
        {
            switch (enumInfo.Variable)
            {
                case "Users":
                    {
                        var userInfo = GetUserInfo();

                        return Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Projects':[Any,@MyProjects]}", userInfo.ProjectsAsString)
                                     .OrWhere("{'Teams':[Any,@MyTeams]}", userInfo.TeamsAsString)
                                     .Values("{'Name':$}");
                    }
                case "Databases":
                    {
                        return GetAllAppNames();
                    }
                default:
                    {
                        return Client.SetDefaultCollection(enumInfo.Variable)
                                     .GetAll()
                                     .Values("{'Name':$}");
                    }
            }
        }

        private string SectionLabelWithCountNotifications(string section)
        {
            if (Context.User.IsGuest)
                return section;

            var isImportant = bool.Parse(Client.SetDefaultCollection("Users")
                                    .GetWhere(@"{'Name':@UserName}")
                                    .Value("{'Notification':{'Filter':{'Important':$}}}"));

            var isNotImportant = bool.Parse(Client.SetDefaultCollection("Users")
                                    .GetWhere(@"{'Name':@UserName}")
                                    .Value("{'Notification':{'Filter':{'NotImportant':$}}}"));

            if (isImportant || isNotImportant)
            {
                string severity;

                if (isImportant && isNotImportant)
                {
                    severity = "'Important','NotImportant'";
                }
                else if (isImportant)
                {
                    severity = "'Important'";
                }
                else if (isNotImportant)
                {
                    severity = "'NotImportant'";
                }
                else
                {
                    severity = "'None'";
                }

                BaseQuery query;


                if (section == "Notifications")
                {
                    query = Client.SetDefaultCollection("Users")
                                  .GetWhere(@"{'Name':@UserName,
                                               'Notification':{'Messages':[{'Severity':Any(@Severity),
                                                                            'IsRead':false}]}}",
                                                severity);
                }
                else
                {
                    query = Client.SetDefaultCollection("Users")
                                  .GetWhere(@"{'Name':@UserName,
                                               'Notification':{'Filter':{'Sections':[Any,@Section]},
                                                               'Messages':[{'Section':@MessageSection,
                                                                            'Severity':Any(@Severity),
                                                                            'IsRead':false}]}}",
                                                section,
                                                section,
                                                severity);

                }

                var count = query.Count("{'Notification':{'Messages':[{'OnDate':$}]}}");

                if (count > 0)
                    return string.Format("\"{0} ({1})\"", section, count);
            }

            return string.Format("\"{0}\"", section);
        }

        public void SetReadNotifications(string section)
        {
            var query = Client.SetDefaultCollection("Users")
                              .GetWhere(@"{'Name':@UserName,'Notification':{'Messages':[{'IsRead':false}]}}");

            if (section != "Notifications")
            {
                query.AndWhere(@"{'Notification':{'Messages':[{'Section':@Section}]}}",
                                   section);
            }

            query.Update("{'Notification':{'Messages':[{'IsRead':true}]}}");
        }

        private void Scenarios()
        {
            Client.SetDefaultCollection("Scenarios")
                  .GetAll()
                  .OpenForm(result => {
                      if(result.Result)
                      {
                          Scenarios();
                      }
                  });
        }

        private void NewScenario()
        {
            Client.SetDefaultCollection("NewScenario")
                  .WantCreateNewDocumentFor("Scenarios")
                  .OpenForm();
        }

        private void RunResults()
        {
            Client.SetDefaultCollection("Scenarios")
                  .GetAll()
                  .OpenForm("{'RunResults':[!$]}", true);
        }

        private void TestingReport()
        {
            /*
            var dbPath = Path.Combine(this.Instance.WorkingFolder, appName);

            var freeKb = 2 * 1024 * 1024;

            long dbUsedKb;

            if (Directory.Exists(dbPath))
            {
                dbUsedKb = new DirectoryInfo(dbPath)
                        .EnumerateFiles("*", SearchOption.AllDirectories)
                        .Sum(file => file.Length) / 1024;
            }
            else
            {
                dbUsedKb = 0;
            }

            var filesPath = Path.Combine(this.Instance.WorkingFolder.Replace("../Databases", "files"), appName);

            long filesUsedKb;

            if (Directory.Exists(filesPath))
            {
                filesUsedKb = new DirectoryInfo(filesPath)
                        .EnumerateFiles("*", SearchOption.AllDirectories)
                        .Sum(file => file.Length) / 1024;
            }
            else
            {
                filesUsedKb = 0;
            }

            var activityReport = new BarChartInfo
            {
                Title = new TitleChartInfo
                {
                    Name = "Activity",
                    X = "Period",
                    Y = "Count"
                }
            };

            var storageReport = new PieChartInfo
            {
                Title = "Storage",
                Parts = new List<PartChartInfo>
                    {
                        new PartChartInfo
                        {
                            Name = "kb DB Used",
                            Value = (uint)dbUsedKb
                        },
                        new PartChartInfo
                        {
                            Name = "kb Files Used",
                            Value = (uint)filesUsedKb
                        },
                        new PartChartInfo
                        {
                            Name = "kb Free",
                            Value = (uint)freeKb
                        }
                    }
            };

            var perfLine = new LineChartInfo { Points = new List<PointChartInfo>() };

            var perfReport = new LineGraphsChartInfo
            {
                Lines = new List<LineChartInfo> { perfLine },
                Title = new TitleChartInfo
                {
                    Name = "Performance",
                    Y = "Time (ms)"
                }
            };

            var errorsReport = new BarChartInfo
            {
                Title = new TitleChartInfo
                {
                    Name = "Errors",
                    X = "Period",
                    Y = "Count"
                }
            };

            DateTime startDate;
            DateTime endDate;

            uint periodInHours;

            Func<DateTime, string> getName;

            if (period == "LastDay")
            {
                startDate = DateTime.Now.AddDays(-1);
                endDate = DateTime.Now;

                periodInHours = 2;
                getName = x => x.ToString("hh:mm");

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Time";
            }
            else if (period == "LastWeek")
            {
                startDate = DateTime.Now.AddDays(-7);
                endDate = DateTime.Now;

                periodInHours = 24;
                getName = x => x.DayOfWeek.ToString().Substring(0, 3);

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Week";
            }
            else if (period == "LastMonth")
            {
                startDate = DateTime.Now.AddMonths(-1);
                endDate = DateTime.Now;

                periodInHours = 3 * 24;
                getName = x => x.Day.ToString("00");

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Month";
            }
            else if (period == "LastYear")
            {
                startDate = DateTime.Now.AddYears(-1);
                endDate = DateTime.Now;

                periodInHours = 30 * 24;
                getName = x => x.ToString("MMMM").Substring(0, 3);

                perfReport.Title.X = $"{startDate} - {endDate}";
                perfLine.Name = $"Year";
            }
            else
            {
                throw new NotImplementedException();
            }

            activityReport.Columns = GetStats(appName, startDate)
                                     .GroupBy(x => x.Period - x.Period % periodInHours)
                                     .Select(x => new PartChartInfo
                                     {
                                         Name = getName(GetStatDate(x.Key)),
                                         Value = (uint)x.Sum(y => y.Requests)
                                     }).ToList();

            errorsReport.Columns = GetStats(appName, startDate)
                                   .GroupBy(x => x.Period - x.Period % periodInHours)
                                   .Select(x => new PartChartInfo
                                   {
                                       Name = getName(GetStatDate(x.Key)),
                                       Value = (uint)x.Sum(y => y.Errors)
                                   }).ToList();

            uint i = 0;

            perfLine.Points = GetStats(appName, startDate)
                              .GroupBy(x => x.Period - x.Period % periodInHours)
                              .Select(x => new PointChartInfo
                              {
                                  X = i++,
                                  Y = (uint)x.Average(y => y.AverageTimeInMsec)
                              }).ToList();

            Client.SetDefaultCollection("EnvironmentsReport")
                  .GetFirstDoc()
                  .ToCollection()
                  .ExtendDocument(DQL("{'AppName':@Project,'Period':@Period}", appName, period))
                  .DeleteByParent("Reports")
                  .Merge(activityReport.ToJson(),
                         new AttrPath("Reports", "Activity", "Report"))
                  .Merge(storageReport.ToJson(),
                         new AttrPath("Reports", "Storage", "Report"))
                  .Merge(perfReport.ToJson(),
                         new AttrPath("Reports", "Performance", "Report"))
                  .Merge(errorsReport.ToJson(),
                         new AttrPath("Reports", "Errors", "Report"))
                  .OpenForm(result =>
                  {
                      if (result.Result)
                      {
                          var appName = result.Collection
                                              .GetFirstDoc()
                                              .Value("{'AppName':$}");

                          var period = result.Collection
                                              .GetFirstDoc()
                                              .Value("{'Period':$}");

                          EnvironmentsReport(appName, period);
                      }
                  });
            */
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Login":
                    Login();
                    break;
                case "Logout":
                    AddNotificationToUser("Logout", SeverityType.NotImportant, "User logged out");
                    Logout();
                    break;
                case "Register":
                    Register();
                    break;
                case "Stories":
                    SetReadNotifications(eventInfo.Action);
                    Stories();
                    break;
                case "MyStories":
                    MyStories();
                    break;
                case "NewStory":
                    NewStory();
                    break;
                case "NewTask":
                    NewTask(eventInfo.DocID);
                    break;
                case "NewArticleComment":
                    NewComment("Articles", eventInfo.DocID);
                    break;
                case "NewNewsComment":
                    NewComment("News", eventInfo.DocID);
                    break;
                case "NewSpecificationComment":
                    NewComment("Specifications", eventInfo.DocID);
                    break;
                case "NewDefect":
                    NewDefect(eventInfo.DocID);
                    break;
                case "NewStoryComment":
                    NewComment("Stories", eventInfo.DocID);
                    break;
                case "NewProjectComment":
                    NewComment("Projects", eventInfo.DocID);
                    break;
                case "NewSprintComment":
                    NewComment("Sprints", eventInfo.DocID);
                    break;
                case "NewVacancyComment":
                    NewComment("Vacancies", eventInfo.DocID);
                    break;
                case "NewCourseComment":
                    NewComment("Courses", eventInfo.DocID);
                    break;
                case "NewExamComment":
                    NewComment("Exams", eventInfo.DocID);
                    break;
                case "NewTaskComment":
                    NewTaskOrDefectComment(eventInfo.AttrPath, eventInfo.DocID);
                    break;
                case "NewDefectComment":
                    NewTaskOrDefectComment(eventInfo.AttrPath, eventInfo.DocID);
                    break;
                case "Sprints":
                    SetReadNotifications(eventInfo.Action);
                    Sprints();
                    break;
                case "NewSprint":
                    NewSprint();
                    break;
                case "NewProject":
                    NewProject();
                    break;
                case "Projects":
                    SetReadNotifications(eventInfo.Action);
                    Projects();
                    break;
                case "Templates":
                    Templates();
                    break;
                case "NewEnvironment":
                    NewEnvironment();
                    break;
                case "DBConsole":
                    DBConsole();
                    break;
                case "EnvironmentReport":
                    EnvironmentReport("FractalPlatform", "LastDay");
                    break;
                case "Environments":
                    Environments();
                    break;
                case "NewSpecification":
                    NewSpecification();
                    break;
                case "Specifications":
                    SetReadNotifications(eventInfo.Action);
                    Specifications();
                    break;
                case "CurrentSprint":
                    CurrentSprint();
                    break;
                case "Users":
                    SetReadNotifications(eventInfo.Action);
                    Users();
                    break;
                case "NewVacancy":
                    NewVacancy();
                    break;
                case "Vacancies":
                    SetReadNotifications(eventInfo.Action);
                    Vacancies();
                    break;
                case "Teams":
                    SetReadNotifications(eventInfo.Action);
                    Teams();
                    break;
                case "NewArticle":
                    NewArticle();
                    break;
                case "Articles":
                    SetReadNotifications(eventInfo.Action);
                    Articles();
                    break;
                case "Courses":
                    Courses();
                    break;
                case "Exams":
                    Exams();
                    break;
                case "NewNews":
                    NewNews();
                    break;
                case "News":
                    SetReadNotifications(eventInfo.Action);
                    News();
                    break;
                case "Notifications":
                    SetReadNotifications(eventInfo.Action);
                    Notifications();
                    break;
                case "Chats":
                    SetReadNotifications(eventInfo.Action);
                    Chats();
                    break;
                case "ShowChat":
                    ShowChat(eventInfo.AttrValue.ToString());
                    break;
                case "NewChat":
                    NewChat();
                    break;
                case "EditChats":
                    EditChats();
                    break;
                case "SendChatMessage":
                    SendChatMessage();
                    break;
                case "Calendar":
                    Calendar();
                    break;
                case "NewMeeting":
                    NewMeeting();
                    break;
                case "Meetings":
                    SetReadNotifications(eventInfo.Action);
                    Meetings();
                    break;
                case "Votes":
                    SetReadNotifications(eventInfo.Action);
                    Votes();
                    break;
                case "Raiting":
                    Raiting();
                    break;
                case "Report":
                    Report(User.Name);
                    break;
                case "ChangePassword":
                    ChangePassword();
                    break;
                case "StartCourse":
                    OpenCourse("Courses", eventInfo.DocID, "Lesson1");
                    break;
                case "StartExam":
                    _pointsExam = 0;
                    _questionExam = 1;
                    OpenExam("Exams", eventInfo.DocID, "Question1");
                    break;
                case "ViewDeployments":
                    ViewDeployments(eventInfo.DocID);
                    break;
                case "ViewLogs":
                    ViewLogs(eventInfo.DocID);
                    break;
                case "ViewApps":
                    ViewApps();
                    break;
                case "Scenarious":
                    Scenarios();
                    break;
                case "NewScenario":
                    NewScenario();
                    break;
                case "RunResults":
                    RunResults();
                    break;
                case "TestingReport":
                    TestingReport();
                    break;
                default:
                    return base.OnEventDimension(eventInfo);
            }

            return true;
        }

        public override bool OnMenuDimension(MenuInfo menuInfo)
        {
            switch (menuInfo.Action)
            {
                case "AddStoryToSprint":
                    {
                        var storyNumber = menuInfo.Collection.GetWhere(menuInfo.AttrPath)
                                                             .Value("{'AvailableStories':[{'StoryNumber':$}]}");

                        var sprintNumber = menuInfo.Collection.GetDoc(menuInfo.DocID)
                                                              .Value("{'SprintNumber':$}");

                        Client.SetDefaultCollection("Stories")
                              .GetWhere("{'StoryNumber':@StoryNumber}", storyNumber)
                              .Update("{'SprintNumber':@SprintNumber}", sprintNumber);

                        return true;
                    }
                case "RemoveStoryFromSprint":
                    {
                        var storyNumber = menuInfo.Collection.GetWhere(menuInfo.AttrPath)
                                                             .Value("{'Stories':[{'StoryNumber':$}]}");

                        Client.SetDefaultCollection("Stories")
                              .GetWhere("{'StoryNumber':@StoryNumber}", storyNumber)
                              .Update("{'SprintNumber':''}");

                        return true;
                    }
                case "SetAsCurrentProject":
                    {
                        _currentProject = Client.SetDefaultCollection("Projects")
                                                .GetDoc(menuInfo.DocID)
                                                .Value("{'Name':$}");

                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@UserName}")
                              .Update("{'CurrentProject':@CurrentProject}", _currentProject);

                        return false;
                    }
                case "AssignStoryToMe":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'AssignedTo':@UserName}");

                        return false;
                    }
                case "AddToCurrentSprint":
                    {
                        var sprintNumber = Client.SetDefaultCollection("Sprints")
                                                 .GetWhere("{'ProjectName':@CurrentProject,'StartDate':LessOrEqual(@Now),'EndDate':GreatOrEqual(@Now)}", _currentProject)
                                                 .Value("{'SprintNumber':$}");

                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'SprintNumber':@SprintNumber}", sprintNumber);

                        return false;
                    }
                case "SetNewStatusForStory":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Status':'New'}");

                        return false;
                    }
                case "SetActiveStatusForStory":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Status':'Active'}");

                        return false;
                    }
                case "SetInProgressStatusForStory":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Status':'InProgress'}");

                        return false;
                    }
                case "SetReadyForQAStatusForStory":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Status':'ReadyForQA'}");

                        return false;
                    }
                case "SetClosedStatusForStory":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Status':'Closed'}");

                        return false;
                    }
                case "AssignTaskToMe":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Tasks':[@@Index,{'AssignedTo':@UserName}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetNewStatusForTask":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Tasks':[@@Index,{'Status':'New'}]}",
                                          menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetActiveStatusForTask":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Tasks':[@@Index,{'Status':'Active'}]}",
                                          menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetInProgressStatusForTask":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Tasks':[@@Index,{'Status':'InProgress'}]}",
                                          menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetClosedStatusForTask":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Tasks':[@@Index,{'Status':'Closed'}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "AssignDefectToMe":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Defects':[@@Index,{'AssignedTo':@UserName}]}",
                                          menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetNewStatusForDefect":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Defects':[@@Index,{'Status':'New'}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetActiveStatusForDefect":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Defects':[@@Index,{'Status':'Active'}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetInProgressStatusForDefect":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Defects':[@@Index,{'Status':'InProgress'}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetReadyForQAStatusForDefect":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Defects':[@@Index,{'Status':'ReadyForQA'}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "SetClosedStatusForDefect":
                    {
                        Client.SetDefaultCollection("Stories")
                              .GetDoc(menuInfo.DocID)
                              .Update("{'Defects':[@@Index,{'Status':'Closed'}]}",
                                      menuInfo.AttrPath.GetFirstIndex());

                        return false;
                    }
                case "StartCourse":
                    {
                        OpenCourse("Courses", menuInfo.DocID, "Lesson1");

                        return false;
                    }
                case "StartExam":
                    {
                        _pointsExam = 0;
                        _questionExam = 1;

                        OpenExam("Exams", menuInfo.DocID, "Question1");

                        return false;
                    }
                case "ApplyVacancy":
                    {
                        NewCandidate(menuInfo.DocID);

                        return false;
                    }
                case "RunScenario":
                    {
                        //AutoTesting.RunScenario(menuInfo.DocID);

                        return false;
                    }
                case "RunAllScenarios":
                    {
                        //AutoTesting.RunAllScenarios();

                        return false;
                    }
                case "StartRecording":
                    {
                        //AutoTesting.StartRecording(menuInfo.DocID);

                        return false;
                    }
                case "StopRecording":
                    {
                        //AutoTesting.StopRecording(menuInfo.DocID);

                        return false;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private void OpenCourse(string collName, uint docID, string refNameKeyValue)
        {
            Client.SetDefaultCollection(collName)
                  .GetDoc(docID)
                  .AndWhere("{'Lessons':[{'Name':@RefName}]}",
                                refNameKeyValue)
                  .ExtendDimension(DimensionType.UI, "{'Style':'Save:Continue;Hide:Likes,Comments,NewComment,StartCourse,Value,Comments'}")
                  .OpenForm("{'Lessons':[!$]}", null, OpenCourse, true);
        }

        private void OpenCourse(FormResult result)
        {
            if (!result.Result)
                return;

            var parentKey = KeyMap.CreateEmpty();
            parentKey.AddAttr2("Options");
            parentKey.SetDocID2(result.DocID);

            result.Collection
                  .DocumentStorage
                  .ScanKeysAndValues2(Context,
                                      parentKey,
                                      (currKeyMap, currAttrValue, data) =>
                                      {
                                          //question was checked
                                          if (currAttrValue.GetBoolValue())
                                          {
                                              //get next form
                                              var refKeyMap = currKeyMap.Clone();

                                              refKeyMap.RemoveParentAttrs2(1); //remove Questions tag

                                              refKeyMap.AddParentAttrs2("References");

                                              var refNameKeyValue = result.Collection
                                                                          .GetDoc(result.DocID)
                                                                          .Value(refKeyMap);

                                              //open form
                                              OpenCourse(result.Collection.Name,
                                                                   result.DocID,
                                                                   refNameKeyValue);
                                          }

                                          return true;
                                      },
                                      null,
                                      result.DocID);
        }

        private uint _questionExam = 0;
        private uint _pointsExam = 0;

        private void OpenExam(string collName, uint docID, string refNameKeyValue)
        {
            var coll = Client.SetDefaultCollection(collName)
                             .GetDoc(docID)
                             .AndWhere("{'Questions':[{'Name':@RefName}]}",
                                       refNameKeyValue)
                             .ToCollection("{'Questions':[!$]}", true)
                             .ExtendDimension(DimensionType.UI, "{'Style':'Save:Continue;Hide:Likes,NewComment,StartExam,Value,Comments'}");

            if (!coll.IsEmpty)
            {
                FormBuilder.OpenForm(Context,
                                 coll,
                                 docID,
                                 null,
                                 OpenExam);
            }
            else
            {
                var title = Client.SetDefaultCollection("Exams")
                                     .GetDoc(docID)
                                     .Value("{'Title':$}");

                Client.SetDefaultCollection("Users")
                      .GetWhere("{'Name':@UserName}")
                      .Update("{'Exams':[Add,{'OnDate':@Now,'Name':@Name,'Points':@Points}]}",
                                  title,
                                  _pointsExam);

                MessageBox($"You have {_pointsExam} points in {title}.");
            }

        }

        private void OpenExam(FormResult result)
        {
            if (!result.Result)
                return;

            var parentKey = KeyMap.CreateEmpty();
            parentKey.AddAttr2("Options");
            parentKey.SetDocID2(result.DocID);

            var hasAnswer = false;

            result.Collection
                  .DocumentStorage
                  .ScanKeysAndValues2(Context,
                                      parentKey,
                                      (currKeyMap, currAttrValue, data) =>
                                      {
                                          //question was checked
                                          if (currAttrValue.GetBoolValue())
                                          {
                                              hasAnswer = true;

                                              //get next form
                                              var refKeyMap = currKeyMap.Clone();

                                              refKeyMap.RemoveParentAttrs2(1); //remove Questions tag

                                              refKeyMap.AddParentAttrs2("Points");

                                              var point = result.Collection
                                                                .GetDoc(result.DocID)
                                                                .Value(refKeyMap);

                                              _pointsExam += uint.Parse(point);
                                              _questionExam++;

                                              //open form
                                              OpenExam(result.Collection.Name,
                                                       result.DocID,
                                                       $"Question{_questionExam}");
                                          }

                                          return true;
                                      },
                                      null,
                                      result.DocID);

            if (!hasAnswer)
            {
                OpenExam(result.Collection.Name,
                         result.DocID,
                         $"Question{_questionExam}");
            }
        }

        private void ViewDeployments(uint docID)
        {
            Client.SetDefaultCollection("Environments")
                  .GetDoc(docID)
                  .ExtendDimension(DimensionType.UI, "{'Deployments':[{'Visible':true}]}")
                  .OpenForm("{'Deployments':[$]}", null, true);
        }

        private void ViewLogs(uint docID)
        {
            Client.SetDefaultCollection("Environments")
                  .GetDoc(docID)
                  .ExtendDimension(DimensionType.UI, "{'Logs':[{'Visible':true,'StackTrace':{'ControlType':'RichTextBox'}}]}")
                  .OpenForm("{'Logs':[$]}", null, true);
        }

        private void ViewApps()
        {
            //GetAllAppNames().Select(x => new { AppName = x,
            //                                   Sessions = UserSessions.GetUserSessions(x).Count,
            //                                   OpenApp = $"http://booben.com/{Context.BasePath}/?appName=" + x })
            //                .ToCollection()
            //                .SetDimension(DimensionType.UI, "{'ReadOnly':true,'OpenApp':{'ControlType':'Link'}}")
            //                .OpenForm();
        }

        #region Notifications

        public override bool OnChangeDimension(ChangeInfo changeInfo)
        {
            switch (changeInfo.CollectionName)
            {
                case "Articles":
                    {
                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var title = changeInfo.Storage
                                                          .GetDoc(Context, changeInfo.DocID)
                                                          .Value("{'Title':$}");

                                    AddNotificationToAll("Articles",
                                                          SeverityType.Important,
                                                          string.Format("Article {0} was created", title));

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    if (changeInfo.ChangeName == "Like")
                                    {
                                        var who = changeInfo.Storage
                                                            .GetDoc(Context, changeInfo.DocID)
                                                            .Value("{'Who':$}");

                                        AddNotificationToUser("Articles",
                                                              SeverityType.NotImportant,
                                                              string.Format("Your article has new like from {0}",
                                                                            changeInfo.AfterValue.ToString()),
                                                              who);
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                case "News":
                    {
                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var title = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                                       .Value("{'Title':$}");

                                    var newsInfo = GetNewsInfo(title);

                                    AddNotificationToGroups("News",
                                                    SeverityType.Important,
                                                    string.Format("News '{0}' was created", title),
                                                    new List<string> { "None" },
                                                    newsInfo.Projects,
                                                    newsInfo.Teams);

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    if (changeInfo.ChangeName == "Like")
                                    {
                                        var who = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                                       .Value("{'Who':$}");

                                        AddNotificationToUser("News",
                                                              SeverityType.NotImportant,
                                                              string.Format("Your news has new like from {0}", changeInfo.AfterValue.ToString()),
                                                              who);
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                case "Chats":
                    {
                        var name = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                          .Value("{'Name':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var chatInfo = GetChatInfo(name);

                                    AddNotificationToGroups("Chats",
                                                    SeverityType.Important,
                                                    string.Format("Chat '{0}' was created", name),
                                                    chatInfo.Users,
                                                    chatInfo.Projects,
                                                    chatInfo.Teams);

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    AddNotificationToGroups("Chats",
                                                            SeverityType.Important,
                                                            string.Format("Assigned new '{0}' chat to you", name),
                                                            changeInfo.ChangeName == "User" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" },
                                                            changeInfo.ChangeName == "Project" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" },
                                                            changeInfo.ChangeName == "Team" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" });

                                    break;
                                }

                        }

                        break;
                    }
                case "Meetings":
                    {
                        var title = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                           .Value("{'Title':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var meetingInfo = GetMeetingInfo(title);

                                    AddNotificationToGroups("Meetings",
                                                    SeverityType.Important,
                                                    string.Format("Meeting '{0}' was created", title),
                                                    meetingInfo.Participants,
                                                    meetingInfo.Projects,
                                                    meetingInfo.Teams);

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    AddNotificationToGroups("Meetings",
                                                            SeverityType.Important,
                                                            string.Format("You are added to new '{0}' meeting", title),
                                                            changeInfo.ChangeName == "User" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" },
                                                            changeInfo.ChangeName == "Project" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" },
                                                            changeInfo.ChangeName == "Team" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" });

                                    break;
                                }
                        }

                        break;
                    }
                case "Projects":
                    {
                        var title = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                           .Value("{'Title':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    AddNotificationToAll("Projects",
                                                    SeverityType.Important,
                                                    string.Format("Project '{0}' was created", title));

                                    break;
                                }
                            case ChangeType.Update:
                                {
                                    var owner = changeInfo.AfterValue.ToString();

                                    AddNotificationToUser("Projects",
                                                    SeverityType.Important,
                                                    string.Format("You are own new '{0}' project", title),
                                                    owner);

                                    break;
                                }
                        }

                        break;
                    }
                case "Sprints":
                    {
                        var title = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                           .Value("{'Title':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {

                                    AddNotificationToAll("Sprints",
                                                    SeverityType.Important,
                                                    string.Format("Sprint '{0}' was created", title));

                                    break;
                                }
                        }

                        break;
                    }
                case "Stories":
                    {
                        var title = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                           .Value("{'Title':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    AddNotificationToAll("Stories",
                                                    SeverityType.Important,
                                                    string.Format("Story '{0}' was created", title));

                                    break;
                                }
                            case ChangeType.Insert:
                            case ChangeType.Update:
                                {
                                    if (changeInfo.ChangeName == "AssignedTo")
                                    {
                                        AddNotificationToUser("Stories",
                                                        SeverityType.Important,
                                                        string.Format("You have new assigned to you '{0}' story", title),
                                                        changeInfo.AfterValue.ToString());

                                    }
                                    else if (changeInfo.ChangeName == "DefectAssignedTo")
                                    {
                                        AddNotificationToUser("Stories",
                                                        SeverityType.Important,
                                                        string.Format("You have new assigned to you defect in '{0}' story", title),
                                                        changeInfo.AfterValue.ToString());
                                    }
                                    else if (changeInfo.ChangeName == "TaskAssignedTo")
                                    {
                                        AddNotificationToUser("Stories",
                                                        SeverityType.Important,
                                                        string.Format("You have new assigned to you task in '{0}' story", title),
                                                        changeInfo.AfterValue.ToString());
                                    }
                                    else if (changeInfo.ChangeName == "TaskStatus" ||
                                             changeInfo.ChangeName == "TaskEstimated")
                                    {
                                        var sprintNumber = changeInfo.Storage
                                                                     .GetDoc(Context, changeInfo.DocID)
                                                                     .Value("{'SprintNumber':$}");

                                        if (!string.IsNullOrEmpty(sprintNumber))
                                        {
                                            var sprintInfo = Client.SetDefaultCollection("Sprints")
                                                                   .GetWhere("{'SprintNumber':@SprintNumber}",
                                                                                sprintNumber)
                                                                   .SelectOne<SprintInfo>();

                                            if (!string.IsNullOrEmpty(sprintInfo.SprintNumber) &&
                                                sprintInfo.StartDate <= DateTime.Now && DateTime.Now <= sprintInfo.EndDate)
                                            {
                                                var onDate = DateTime.Now.ToShortDateString();

                                                if (Client.SetDefaultCollection("Sprints")
                                                          .GetWhere("{'SprintNumber':@SprintNumber,'RemainedHistory':[{'OnDate':@OnDate}]}",
                                                                    sprintInfo.SprintNumber, onDate)
                                                          .Exists())
                                                {
                                                    Client.SetDefaultCollection("Sprints")
                                                          .GetWhere("{'SprintNumber':@SprintNumber,'RemainedHistory':[{'OnDate':@OnDate}]}",
                                                                    sprintInfo.SprintNumber, onDate)
                                                          .Update("{'RemainedHistory':[{'RemainedTasks':@RemainedTasks}]}", sprintInfo.RemainedTasks);
                                                }
                                                else
                                                {
                                                    Client.SetDefaultCollection("Sprints")
                                                           .GetWhere("{'SprintNumber':@SprintNumber}",
                                                                     sprintInfo.SprintNumber)
                                                           .Update("{'RemainedHistory':[Add,{'OnDate':@OnDate,'RemainedTasks':@RemainedTasks}]}",
                                                                  onDate,
                                                                  sprintInfo.RemainedTasks);
                                                }
                                            }
                                        }

                                    }

                                    break;
                                }
                            default:
                                break;
                        }

                        break;
                    }
                case "Teams":
                    {
                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var name = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                                       .Value("{'Name':$}");

                                    AddNotificationToAll("Teams",
                                                    SeverityType.Important,
                                                    string.Format("Team '{0}' was created", name));

                                    break;
                                }
                        }

                        break;
                    }
                case "Vacancies":
                    {
                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var position = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                                       .Value("{'Position':$}");

                                    var team = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                                       .Value("{'Team':$}");

                                    AddNotificationToAll("Vacancies",
                                                    SeverityType.Important,
                                                    string.Format("New vacancy with poisition '{0}' in team '{1}' is opened", position, team));

                                    break;
                                }
                        }

                        break;
                    }
                case "Specifications":
                    {
                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var name = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                                       .Value("{'Name':$}");

                                    AddNotificationToAll("Specifications",
                                                    SeverityType.Important,
                                                    string.Format("New specification '{0}' is created", name));

                                    break;
                                }
                        }

                        break;
                    }
                case "Users":
                    {
                        var userName = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                              .Value("{'Name':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    AddNotificationToAll("Users",
                                                    SeverityType.NotImportant,
                                                    string.Format("User '{0}' was registered", userName));

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    if (changeInfo.ChangeName == "Project")
                                    {
                                        AddNotificationToUser("Users",
                                                    SeverityType.Important,
                                                    string.Format("You are added to new '{0}' project", changeInfo.AfterValue.ToString()),
                                                    userName);
                                    }
                                    else if (changeInfo.ChangeName == "Team")
                                    {
                                        AddNotificationToUser("Users",
                                                SeverityType.Important,
                                                string.Format("You are added to new '{0}' team", changeInfo.AfterValue.ToString()),
                                                userName);
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                case "Environments":
                    {
                        var envName = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                              .Value("{'EnvironmentName':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    AddNotificationToAll("Environments",
                                                    SeverityType.NotImportant,
                                                    string.Format("New '{0}' environment was created", envName));

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    if (changeInfo.ChangeName == "Deployment")
                                    {
                                        AddNotificationToGroups("Environments",
                                                            SeverityType.NotImportant,
                                                            string.Format("Part of '{0}' project was deployed to '{1}' environment",
                                                                          changeInfo.AfterValue.ToString(),
                                                                          envName),
                                                            new List<string> { "None" },
                                                            new List<string> { changeInfo.AfterValue.ToString() },
                                                            new List<string> { "None" });
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                case "Votes":
                    {
                        var question = changeInfo.Storage.GetDoc(Context, changeInfo.DocID)
                                              .Value("{'Question':$}");

                        switch (changeInfo.ChangeType)
                        {
                            case ChangeType.CreateDocument:
                                {
                                    var voteInfo = GetVoteInfo(question);

                                    AddNotificationToGroups("Votes",
                                                    SeverityType.Important,
                                                    string.Format("Vote with '{0}' question was created", question),
                                                    voteInfo.Users,
                                                    voteInfo.Projects,
                                                    voteInfo.Teams);

                                    break;
                                }
                            case ChangeType.Insert:
                                {
                                    AddNotificationToGroups("Votes",
                                                            SeverityType.Important,
                                                            string.Format("You are participate in new '{0}' vote", question),
                                                            changeInfo.ChangeName == "User" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" },
                                                            changeInfo.ChangeName == "Project" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" },
                                                            changeInfo.ChangeName == "Team" ? new List<string> { changeInfo.AfterValue.ToString() } : new List<string> { "None" });

                                    break;
                                }
                        }

                        break;
                    }
                default:
                    throw new NotImplementedException();
            };

            return false;
        }

        private void AddNotificationToUser(string sectionName,
                                     SeverityType severityType,
                                     string text,
                                     string forUserName = null)
        {
            if (string.IsNullOrEmpty(forUserName))
            {
                forUserName = Context.User.Name;
            }

            Client.SetDefaultCollection("Users")
                  .GetWhere(@"{'Name':@Name,
                                   'Notification':{'Filter':{@SeverityType:true,
                                                            'Sections':[Any,@SectionName]}}}",
                                   forUserName,
                                   severityType.ToString(),
                                   sectionName)
                  .Update(@"{'Notification':{'Messages':[Add,
                                                             {'CurrentUser':@UserName,
                                                              'OnDate':@Now,
                                                              'Section':@SectionName,
                                                              'Severity':@SeverityType,
                                                              'Text':@Text,
                                                              'IsRead':false}]}}",
                                 sectionName,
                                 severityType,
                                 text);

            //send telegram message
            if (severityType == SeverityType.Important)
            {
                var receiver = Client.SetDefaultCollection("Users")
                                     .GetWhere(@"{'Name':@Name,'Contacts':{'Telegram':{'UseForNotifications':true}}}", forUserName)
                                     .Value(@"{'Contacts':{'Telegram':{'ChatId':$}}}");

                if (!string.IsNullOrEmpty(receiver))
                {
                    Client.SetDefaultCollection("Users")
                          .GetWhere(@"{'Name':@Name}",
                                       forUserName)
                          .Update(@"{'TextMessages':[Add,
                                            {'Provider':'Telegram',
                                             'Receiver':@Receiver,
                                             'Message':@Message,
                                             'IsSent':false}]}",
                                        receiver,
                                        GetLocalizedValue(text));
                }
            }
        }

        private void AddNotificationToAll(string sectionName,
                                     SeverityType severityType,
                                     string text)
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere(@"{'Notification':{'Filter':{@SeverityType:true,
                                                            'Sections':[Any,@SectionName]}}}",
                                   severityType.ToString(),
                                   sectionName)
                  .Update(@"{'Notification':{'Messages':[Add,
                                                             {'CurrentUser':@UserName,
                                                              'OnDate':@Now,
                                                              'Section':@SectionName,
                                                              'Severity':@SeverityType,
                                                              'Text':@Text,
                                                              'IsRead':false}]}}",
                                 sectionName,
                                 severityType,
                                 text);
        }

        private void AddNotificationToGroups(string sectionName,
                                     SeverityType severityType,
                                     string text,
                                     List<string> users,
                                     List<string> projects,
                                     List<string> teams)
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere("{'Projects':[Any,@Projects]}", projects)
                  .OrWhere("{'Teams':[Any,@Teams]}", teams)
                  .OrWhere("{'Name':Any(@Users)}", users)
                  .AndWhere(@"{'Notification':{'Filter':{@SeverityType:true,
                                                         'Sections':[Any,@SectionName]}}}",
                                   severityType.ToString(),
                                   sectionName)
                  .Update(@"{'Notification':{'Messages':[Add,
                                                             {'CurrentUser':@UserName,
                                                              'OnDate':@Now,
                                                              'Section':@SectionName,
                                                              'Severity':@SeverityType,
                                                              'Text':@Text,
                                                              'IsRead':false}]}}",
                                 sectionName,
                                 severityType,
                                 text);
        }

        #endregion

        public override BaseRenderForm CreateRenderForm(DOMForm form)
        {
            return new RenderForm(this);
        }
    }
}