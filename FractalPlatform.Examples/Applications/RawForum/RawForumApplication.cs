using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System.Text.RegularExpressions;

namespace FractalPlatform.Examples.Applications.RawForum
{
    public class RawForumApplication : DashboardApplication
    {
        private string _category;

        private uint _topicID;

        private void Dashboard()
        {
            CloseIfOpenedForm("Dashboard");

            var categories = Client.SetDefaultCollection("Categories")
                                   .GetAll()
                                   .ToStorage("{'Title':$,'Description':$,'CountMessages':$,'CountTopics':$,'LastMessage':{'Who':$,'OnDate':$}}");

            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .ToCollection()
                  .RemovePartDocument("{'Categories':$}")
                  .MergeToArrayPath(categories, "Categories")
                  .OpenForm();
        }

        private void CategoryDashboard()
        {
            CloseIfOpenedForm("CategoryDashboard");

            var topics = Client.SetDefaultCollection("Topics")
                               .GetWhere("{'Category':@Category}", _category)
                               .ToStorage();

            Client.SetDefaultCollection("CategoryDashboard")
                  .GetFirstDoc()
                  .ToCollection()
                  .RemovePartDocument("{'Topics':$}")
                  .MergeToArrayPath(topics, "Topics", Constants.FIRST_DOC_ID, true)
                  .OpenForm(result => Dashboard());
        }

        private void TopicDashboard()
        {
            CloseIfOpenedForm("TopicDashboard");

            Client.SetDefaultCollection("Topics")
                  .GetDoc(_topicID)
                  .Update("{'CountViews':Add(1)}");

            var topic = Client.SetDefaultCollection("Topics")
                              .GetDoc(_topicID)
                              .ToStorage();

            Client.SetDefaultCollection("TopicDashboard")
                  .GetFirstDoc()
                  .ToCollection()
                  .RemovePartDocument("{'Messages':$}")
                  .MergeToPath(topic, _topicID)
                  .OpenForm(result => CategoryDashboard());
        }

        public override bool OnOpenForm(FormInfo formInfo)
        {
            if (formInfo.AttrPath.FirstPath == "Categories")
            {
                _category = formInfo.Collection
                                   .GetWhere(formInfo.AttrPath)
                                   .Value("{'Categories':[{'Title':$}]}");

                CategoryDashboard();

                return false;
            }
            else if (formInfo.AttrPath.FirstPath == "Topics")
            {
                _topicID = formInfo.Collection
                                   .GetWhere(formInfo.AttrPath)
                                   .UIntValue("{'Topics':[{'DocID':$}]}");

                TopicDashboard();

                return false;
            }
            else
            {
                return true;
            }
        }

        private string ReplaceUrls(string message)
        {
            var matches = Regex.Matches(message, "https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)");

            foreach (Match match in matches)
            {
                message = message.Replace(match.Value, $"<a href=\"{match.Value}\">{match.Value}</a>");
            }

            return message;
        }

        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            switch (computedInfo.Variable)
            {
                case "WhoCountMessages":
                    {
                        return Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Name':@Name}", computedInfo.Collection
                                                                             .GetWhere(computedInfo.AttrPath)
                                                                             .Value("{'Who':{'Name':$}}"))
                                     .Value("{'CountMessages':$}");
                    }
                case "WhoRegistered":
                    {
                        return Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Name':@Name}", computedInfo.Collection
                                                                             .GetWhere(computedInfo.AttrPath)
                                                                             .Value("{'Who':{'Name':$}}"))
                                     .Value("{'Registered':$}");
                    }
                case "UserMessageCountMessages":
                    {
                        return Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Name':@Name}", computedInfo.Collection
                                                                             .GetWhere(computedInfo.AttrPath)
                                                                             .Value("{'Messages':[{'Who':$}]}"))
                                     .Value("{'CountMessages':$}");
                    }
                case "UserMessageRegistered":
                    {
                        return Client.SetDefaultCollection("Users")
                                     .GetWhere("{'Name':@Name}", computedInfo.Collection
                                                                             .GetWhere(computedInfo.AttrPath)
                                                                             .Value("{'Messages':[{'Who':$}]}"))
                                     .Value("{'Registered':$}");
                    }
                case "UserCountMessages":
                    {
                        return Client.SetDefaultCollection("Topics")
                                     .GetWhere("{'Messages':[{'Who':@User}]}", computedInfo.Collection
                                                                                           .GetWhere(computedInfo.AttrPath)
                                                                                           .Value("{'Name':$}"))
                                     .Count("{'Messages':[{'Who':$}]}");
                    }
                case "Category":
                    {
                        return _category;
                    }
                case "Title":
                    {
                        return computedInfo.Collection
                                           .GetWhere(computedInfo.AttrPath)
                                           .Value("{'Title':$}");
                    }
                case "DescriptionShort":
                    {
                        var description = computedInfo.Collection
                                                      .GetWhere(computedInfo.AttrPath)
                                                      .Value("{'Description':$}") ?? string.Empty;

                        return description.Substring(0, description.IndexOf("\n"));
                    }
                case "DescriptionHtml":
                    {
                        var description = computedInfo.Collection
                                                      .GetWhere(computedInfo.AttrPath)
                                                      .Value("{'Description':$}") ?? string.Empty;

                        description = description.Replace("\n", "<br>");

                        description = ReplaceUrls(description);

                        return description;
                    }
                case "MessageHtml":
                    {
                        var message = computedInfo.Collection
                                                  .GetWhere(computedInfo.AttrPath)
                                                  .Value("{'Messages':[{'Message':$}]}") ?? string.Empty;

                        message = message.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br>");

                        message = ReplaceUrls(message);

                        return Regex.Replace(message, "\\[QUOTE=(?<Name>[a-zA-Z0-9]+)\\]", m => $"<div style='border-style:ridge;'>{m.Groups["Name"].Value} writes:<br/>")
                                    .Replace("[/QUOTE]", "</div>");
                    }
                default:
                    return base.OnComputedDimension(computedInfo);
            }
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Users":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetAll()
                              .WantModifyExistingDocuments()
                              .OpenForm();

                        return true;
                    }
                case "EditCategories":
                    {
                        Client.SetDefaultCollection("Categories")
                              .GetAll()
                              .WantModifyExistingDocuments()
                              .OpenForm(result => Dashboard());

                        return true;
                    }
                case "EditTopics":
                    {
                        Client.SetDefaultCollection("Topics")
                              .GetWhere("{'Category':@Category}", _category)
                              .WantModifyExistingDocuments()
                              .OpenForm(result => CategoryDashboard());

                        return true;
                    }
                case "EditTopic":
                    {
                        Client.SetDefaultCollection("Topics")
                              .GetDoc(_topicID)
                              .WantModifyExistingDocuments()
                              .OpenForm(result => TopicDashboard());

                        return true;
                    }
                case "Register":
                    {
                        Register();

                        return true;
                    }
                case "LoginButton":
                    {
                        var loginAndPass = eventInfo.Collection
                                                    .GetFirstDoc()
                                                    .Values("{'Login':$,'Password':$}");

                        if (TryLogin(loginAndPass[0], loginAndPass[1]))
                        {
                            if (eventInfo.Collection.Name == "CategoryDashboard")
                                CategoryDashboard();
                            else if (eventInfo.Collection.Name == "TopicDashboard")
                                TopicDashboard();
                            else
                                Dashboard();
                        }
                        else
                        {
                            MessageBox("Wrong credentials");
                        }

                        return true;
                    }
                case "NewTopic":
                    {
                        Client.SetDefaultCollection("NewTopic")
                              .WantCreateNewDocumentFor("Topics")
                              .OpenForm(result => {
                                  if (result.Result)
                                  {
                                      Client.SetDefaultCollection("Categories")
                                            .GetWhere("{'Title':@Title}", _category)
                                            .Update("{'LastMessage':{'Who':@UserName,'OnDate':@Now},'CountTopics':Add(1)}");

                                      CategoryDashboard();
                                  }
                              });

                        return true;
                    }
                case "NewMessage":
                    {
                        Client.SetDefaultCollection("NewMessage")
                              .WantCreateNewDocumentForArray("Topics", "{'Messages':[$]}", _topicID)
                              .OpenForm(result => {
                                  if (result.Result)
                                  {
                                      Client.SetDefaultCollection("Categories")
                                            .GetWhere("{'Title':@Title}", _category)
                                            .Update("{'LastMessage':{'Who':@UserName,'OnDate':@Now},'CountMessages':Add(1)}");

                                      Client.SetDefaultCollection("Topics")
                                            .GetDoc(_topicID)
                                            .Update("{'LastMessage':{'Who':@UserName,'OnDate':@Now},'CountMessages':Add(1)}");

                                      TopicDashboard();
                                  }
                              });

                        return true;
                    }
                case "NewQuoteMessage":
                    {
                        var whoAndMessage = eventInfo.Collection
                                                     .GetWhere(eventInfo.AttrPath)
                                                     .Values("{'Messages':[{'Who':$,'Message':$}]}");

                        Client.SetDefaultCollection("NewMessage")
                              .WantCreateNewDocumentForArray("Topics", "{'Messages':[$]}", _topicID)
                              .ExtendDocument(DQL("{'Message':@Message}", $"[QUOTE={whoAndMessage[0]}]{whoAndMessage[1]}[/QUOTE]"))
                              .OpenForm(result => TopicDashboard());

                        return true;
                    }
                case "Who":
                case "Name":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetWhere("{'Name':@Name}", eventInfo.AttrValue)
                              .OpenForm();

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

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
