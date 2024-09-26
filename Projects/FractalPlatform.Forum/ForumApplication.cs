using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;
using System.Text.RegularExpressions;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Forum
{
    public class ForumApplication : DashboardApplication
    {
        private string _category;

        private uint _topicID;

        private void Dashboard()
        {
            CloseIfOpenedForm("Dashboard");

            var categories = DocsOf("Categories").ToStorage("{'Title':$,'Description':$,'CountMessages':$,'CountTopics':$,'LastMessage':{'Who':$,'OnDate':$}}");

            FirstDocOf("Dashboard")
                .ToCollection()
                .RemovePartDocument("{'Categories':$}")
                .MergeToArrayPath(categories, "Categories")
                .OpenForm();
        }

        private void CategoryDashboard()
        {
            CloseIfOpenedForm("CategoryDashboard");

            var topics = DocsWhere("Topics", "{'Category':@Category}", _category).ToStorage();

            FirstDocOf("CategoryDashboard")
                .ToCollection()
                .RemovePartDocument("{'Topics':$}")
                .MergeToArrayPath(topics, "Topics", Constants.FIRST_DOC_ID, true)
                .OpenForm(result => Dashboard());
        }

        private void TopicDashboard()
        {
            CloseIfOpenedForm("TopicDashboard");

            DocsWhere("Topics", _topicID).Update("{'CountViews':Add(1)}");

            var topic = DocsWhere("Topics", _topicID).ToStorage();

            FirstDocOf("TopicDashboard")
                  .ToCollection()
                  .RemovePartDocument("{'Messages':$}")
                  .MergeToPath(topic, _topicID)
                  .OpenForm(result => CategoryDashboard());
        }

        public override bool OnOpenForm(FormInfo info)
        {
            if (info.AttrPath.FirstPath == "Categories")
            {
                _category = info.Collection
                                   .GetWhere(info.AttrPath)
                                   .Value("{'Categories':[{'Title':$}]}");

                CategoryDashboard();

                return false;
            }
            else if (info.AttrPath.FirstPath == "Topics")
            {
                _topicID = info.Collection
                                   .GetWhere(info.AttrPath)
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

        public override object OnComputedDimension(ComputedInfo info)
        {
            switch (info.Variable)
            {
                case "WhoCountMessages":
                    {
                        return DocsWhere("Users", "{'Name':@Name}",
                                         info.Collection
                                                     .GetWhere(info.AttrPath)
                                                     .Value("{'Who':{'Name':$}}")).Value("{'CountMessages':$}");
                    }
                case "WhoRegistered":
                    {
                        return DocsWhere("Users", "{'Name':@Name}",
                                         info.Collection
                                                     .GetWhere(info.AttrPath)
                                                     .Value("{'Who':{'Name':$}}")).Value("{'Registered':$}");
                    }
                case "UserMessageCountMessages":
                    {
                        return DocsWhere("Users", "{'Name':@Name}",
                                         info.Collection
                                                     .GetWhere(info.AttrPath)
                                                     .Value("{'Messages':[{'Who':$}]}")).Value("{'CountMessages':$}");
                    }
                case "UserMessageRegistered":
                    {
                        return DocsWhere("Users", "{'Name':@Name}",
                                         info.Collection
                                                     .GetWhere(info.AttrPath)
                                                     .Value("{'Messages':[{'Who':$}]}")).Value("{'Registered':$}");
                    }
                case "UserCountMessages":
                    {
                        return DocsWhere("Topics", "{'Messages':[{'Who':@User}]}",
                                         info.Collection
                                                     .GetWhere(info.AttrPath)
                                                     .Value("{'Name':$}")).Count("{'Messages':[{'Who':$}]}");
                    }
                case "Category":
                    {
                        return _category;
                    }
                case "Title":
                    {
                        return info.Collection
                                           .GetWhere(info.AttrPath)
                                           .Value("{'Title':$}");
                    }
                case "DescriptionShort":
                    {
                        var description = info.Collection
                                                      .GetWhere(info.AttrPath)
                                                      .Value("{'Description':$}") ?? string.Empty;

                        return description.Contains("\n") ? description.Substring(0, description.IndexOf("\n")) : description;
                    }
                case "DescriptionHtml":
                    {
                        var description = info.Collection
                                                      .GetWhere(info.AttrPath)
                                                      .Value("{'Description':$}") ?? string.Empty;

                        description = description.Replace("\n", "<br>");

                        description = ReplaceUrls(description);

                        return description;
                    }
                case "MessageHtml":
                    {
                        var message = info.Collection
                                                  .GetWhere(info.AttrPath)
                                                  .Value("{'Messages':[{'Message':$}]}") ?? string.Empty;

                        message = message.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br>");

                        message = ReplaceUrls(message);

                        return Regex.Replace(message, "\\[QUOTE=(?<Name>[a-zA-Z0-9]+)\\]", m => $"<div style='border-style:ridge;'>{m.Groups["Name"].Value} writes:<br/>")
                                    .Replace("[/QUOTE]", "</div>");
                    }
                default:
                    return base.OnComputedDimension(info);
            }
        }

        public override bool OnEventDimension(EventInfo info)
        {
            switch (info.Action)
            {
                case "Users":
                    {
                        ModifyDocsOf("Users").OpenForm();

                        return true;
                    }
                case "EditCategories":
                    {
                        ModifyDocsOf("Categories").OpenForm(result => Dashboard());

                        return true;
                    }
                case "EditTopics":
                    {
                        ModifyDocsWhere("Topics", "{'Category':@Category}", _category).OpenForm(result => CategoryDashboard());

                        return true;
                    }
                case "EditTopic":
                    {
                        ModifyDocsWhere("Topics", _topicID).OpenForm(result => TopicDashboard());

                        return true;
                    }
                case "Register":
                    {
                        Register();

                        return true;
                    }
                case "LoginButton":
                    {
                        var loginAndPass = info.Collection
                                                    .GetFirstDoc()
                                                    .Values("{'Login':$,'Password':$}");

                        if (TryLogin(loginAndPass[0], loginAndPass[1]))
                        {
                            if (info.Collection.Name == "CategoryDashboard")
                                CategoryDashboard();
                            else if (info.Collection.Name == "TopicDashboard")
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
                        CreateNewDocFor("NewTopic", "Topics")
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
                        CreateNewDocForArray("NewMessage", "Topics", "{'Messages':[$]}", _topicID)
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
                        var whoAndMessage = info.Collection
                                                     .GetWhere(info.AttrPath)
                                                     .Values("{'Messages':[{'Who':$,'Message':$}]}");

                        CreateNewDocForArray("NewMessage", "Topics", "{'Messages':[$]}", _topicID)
                              .ExtendDocument(DQL("{'Message':@Message}", $"[QUOTE={whoAndMessage[0]}]{whoAndMessage[1]}[/QUOTE]"))
                              .OpenForm(result => TopicDashboard());

                        return true;
                    }
                case "Who":
                case "Name":
                    {
                        DocsWhere("Users", "{'Name':@Name}", info.AttrValue).OpenForm();

                        return true;
                    }
                default:
                    return base.OnEventDimension(info);
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