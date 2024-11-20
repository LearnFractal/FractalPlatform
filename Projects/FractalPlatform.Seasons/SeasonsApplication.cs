using System;
using System.Collections.Generic;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Seasons
{
    public class SeasonsApplication : DashboardApplication
    {
        private void Dashboard(Collection collection = null)
        {
            CloseIfOpenedForm("Dashboard");

            var query = DocsOf("Movies");

            if (Context.HasUrlTag)
            {
                if (Context.UrlTag.Contains(':'))
                {
                    query = query.AndWhere("{'Name':@Name}", Context.UrlTag.Split(':')[1]);
                }
                else
                {
                    query = query.AndWhere("{'Type':@Type}", Context.UrlTag);
                }
            }

            List<string> filters = null;

            if (collection != null)
            {
                filters = collection.GetFirstDoc()
                                    .Values("{'Genre':$,'Year':$,'Country':$}");

                if (filters[0] != "Жанр") query = query.AndWhere("{'Genre':@Genre}", filters[0]);
                if (filters[1] != "Рік") query = query.AndWhere("{'Year':@Year}", filters[1]);
                if (filters[2] != "Країна") query = query.AndWhere("{'Countries':[Any,@Country]}", filters[2]);
            }

            var movies = query.OrderByDesc("{'Rating':$}")
                              .ToStorage();

            FirstDocOf("Dashboard").ToCollection()
                                   .IfTrue(collection != null, c => c.ExtendDocument(DQL("{'Genre':@Genre,'Year':@Year,'Country':@Country}", filters[0], filters[1], filters[2])))
                                   .MergeToArrayPath(movies, "Movies")
                                   .OpenForm();
        }

        private void UsersDashboard(string movie = null)
        {
            CloseIfOpenedForm("UsersDashboard");

            Storage users;

            if (movie != null)
            {
                users = DocsWhere("Users", "{'PlayList':[Any,{'Name':@Name}]}", movie)
                        .OrWhere("{'BestMovies':[Any,{'Name':@Name}]}", movie)
                        .ToStorage();
            }
            else
            {
                users = DocsOf("Users").ToStorage();
            }

            FirstDocOf("UsersDashboard")
            .ToCollection()
            .MergeToArrayPath(users, "Users")
            .OpenForm();
        }

        private void MovieDashboard(string name)
        {
            CloseIfOpenedForm("MovieDashboard");

            DocsWhere("Movies", "{'Name':@Name}", name).Update("{'Views':Add(1)}");

            var movie = DocsWhere("Movies", "{'Name':@Name}", name).ToStorage();

            var recommends = DocsOf("Movies")
                             .OrderByDesc("{'Rating':$}")
                             .Take(6)
                             .ToStorage();

            FirstDocOf("MovieDashboard")
            .ToCollection()
            .MergeToPath(movie)
            .MergeToArrayPath(recommends, "Recommends")
            .OpenForm();
        }

        private void ViewUser(AttrPath attrPath)
        {
            var name = DocsWhere("Users", attrPath).Value("{'Name':$}");

            ViewUser(name);
        }

        private void ViewUser(string name)
        {
            CloseIfOpenedForm("User");

            DocsWhere("Users", "{'Name':@Name}", name)
            .SetDimension(DimensionType.UI, "{'Layout':'User','IsRawPage':true}")
            .OpenForm();
        }

        public override bool OnOpenForm(FormInfo info)
        {
            if (info.Collection.Name == "Dashboard" &&
                info.AttrPath.FirstPath == "Movies")
            {
                var name = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'Movies':[{'Name':$}]}");

                MovieDashboard(name);

                return false;
            }
            else if (info.Collection.Name == "MovieDashboard" &&
                     info.AttrPath.FirstPath == "Recommends")
            {
                var name = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'Recommends':[{'Name':$}]}");

                MovieDashboard(name);

                return false;
            }
            else if (info.Collection.Name == "Users" &&
                     info.AttrPath.FirstPath == "PlayList")
            {
                var name = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'PlayList':[{'Name':$}]}");

                MovieDashboard(name);

                return false;
            }
            else if (info.Collection.Name == "Users" &&
                     info.AttrPath.FirstPath == "BestMovies")
            {
                var name = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'BestMovies':[{'Name':$}]}");

                MovieDashboard(name);

                return false;
            }
            else if (info.Collection.Name == "Users" &&
                     info.AttrPath.FirstPath == "GoodMovies")
            {
                var name = info.Collection
                                  .GetWhere(info.AttrPath)
                                  .Value("{'GoodMovies':[{'Name':$}]}");

                MovieDashboard(name);

                return false;
            }
            else if (info.Collection.Name == "UsersDashboard" &&
                     info.AttrPath.FirstPath == "Users")
            {
                var name = info.Collection
                                   .GetWhere(info.AttrPath)
                                   .Value("{'Users':[{'Name':$}]}");

                ViewUser(name);

                return false;
            }

            return true;
        }

        public override object OnComputedDimension(ComputedInfo info)
        {
            switch (info.Variable)
            {
                case "CountUsers":
                    return DocsOf("Users").Count();
                case "CountMovies":
                    return DocsOf("Movies").Count();
                case "CurrentFilms":
                    return !Context.HasUrlTag || Context.UrlTag == "films" ? "current" : "";
                case "CurrentSeries":
                    return Context.UrlTag == "series" ? "current" : "";
                case "CurrentCartoons":
                    return Context.UrlTag == "cartoons" ? "current" : "";
                case "PlayListCount":
                    return DocsWhere("Users", info.AttrPath).Count("{'PlayList':[{'Name':$}]}");
                case "BestMoviesCount":
                    return DocsWhere("Users", info.AttrPath).Count("{'BestMovies':[{'Name':$}]}");
                case "CommentsCount":
                    var user = DocsWhere("Users", info.AttrPath).Value("{'Name':$}");

                    return DocsWhere("Movies", "{'Comments':[{'Name':@User}]}", user)
                           .Count("{'Comments':[{'Name':$}]}");
                case "OnDateLabel":
                    var dateAgo = DateTime.Now.Subtract(info.Collection
                                                                    .GetWhere(info.AttrPath)
                                                                    .DateTimeValue("{'PlayList':[{'OnDate':$}]}"));

                    if (dateAgo.Days > 0) return $"{dateAgo.Days} днів тому";
                    if (dateAgo.Hours > 0) return $"{dateAgo.Hours} годин тому";
                    if (dateAgo.Minutes > 0) return $"{dateAgo.Minutes} хвилин тому";
                    else return $"{dateAgo.Seconds} секунд тому";
                default:
                    return base.OnComputedDimension(info);
            }
        }

        public override bool OnEventDimension(EventInfo info)
        {
            switch (info.Action)
            {
                case "Genre":
                case "Country":
                case "Year":
                case "ChangeType":
                    {
                        Dashboard(info.Collection);

                        return true;
                    }
                case "LoginButton":
                    {
                        var loginAndPass = info.Collection
                                                    .GetFirstDoc()
                                                    .Values("{'Login':$,'Password':$}");

                        if (TryLogin(loginAndPass[0], loginAndPass[1]))
                        {
                            Dashboard();
                        }
                        else
                        {
                            MessageBox("Wrong credentials");
                        }

                        return true;
                    }
                case "RegisterButton":
                    {
                        Register();

                        return true;
                    }
                case "MyUser":
                    {
                        ViewUser(Context.User.Name);

                        return true;
                    }
                case "ViewUser":
                    {
                        var name = info.Collection
                                            .GetWhere(info.AttrPath)
                                            .Value("{'Comments':[{'Name':$}]}");

                        ViewUser(name);

                        return true;
                    }
                case "NewMovie":
                    {
                        CreateNewDocFor("NewMovie", "Movies").OpenForm();

                        return true;
                    }
                case "AddComment":
                    {
                        var movie = info.FindFirstValue("Name");

                        var text = info.FindFirstValue("Comment");

                        DocsWhere("Movies", "{'Name':@Name}", movie)
                        .Update("{'Comments':[Add,{'Name':@Name,'Avatar':@Avatar,'Text':@Text, 'ViewUser':'View User'}]}", Context.User.Name, Context.User.Avatar, text);

                        MovieDashboard(movie); //reload movie dashboard

                        return true;
                    }
                case "EditMovies":
                    {
                        ModifyDocsOf("Movies")
                        .ExtendUIDimension("{'Layout':'','IsRawPage':false}")
                        .OpenForm();

                        return true;
                    }
                case "EditActors":
                    {
                        ModifyDocsOf("Actors").OpenForm();

                        return true;
                    }
                case "EditUsers":
                    {
                        ModifyDocsOf("Users")
                        .ExtendUIDimension("{'Layout':'','IsRawPage':false}")
                        .OpenForm();

                        return true;
                    }
                case "EditUser":
                    {
                        ModifyDocsWhere("Users", "{'Name':@Name}", Context.User.Name)
                        .ExtendUIDimension("{'Layout':'','IsRawPage':false,'PlayList':{'Visible':false},'BestMovies':{'Visible':false},'GoodMovies':{'Visible':false}}")
                        .OpenForm(result => result.NeedReloadData = true);

                        return true;
                    }
                case "AllUsers":
                    {
                        UsersDashboard();

                        return true;
                    }
                case "AddToPlayList":
                case "ViewUsers":
                    {
                        string name;

                        if (info.Collection.Name == "MovieDashboard")
                        {
                            if (info.AttrPath.FirstPath == "Recommends")
                            {
                                name = info.Collection
                                                .GetWhere(info.AttrPath)
                                                .Value("{'Recommends':[{'Name':$}]}");
                            }
                            else
                            {
                                name = info.Collection
                                                .GetWhere(info.AttrPath)
                                                .Value("{'Name':$}");
                            }
                        }
                        else
                        {
                            name = info.Collection
                                            .GetWhere(info.AttrPath)
                                            .Value("{'Movies':[{'Name':$}]}");
                        }

                        if (info.Action == "AddToPlayList")
                        {
                            if (Client.SetDefaultCollection("Users")
                                       .GetWhere("{'Name':@UserName,'PlayList':[Any,{'Name':@Name}]}", name)
                                       .Any() ||
                                Client.SetDefaultCollection("Users")
                                       .GetWhere("{'Name':@UserName,'BestMovies':[Any,{'Name':@Name}]}", name)
                                       .Any() ||
                                Client.SetDefaultCollection("Users")
                                       .GetWhere("{'Name':@UserName,'GoodMovies':[Any,{'Name':@Name}]}", name)
                                       .Any())
                            {
                                MessageBox($"Фільм {name} вже є в вашому списку перегляду.", MessageBoxButtonType.Ok);
                            }
                            else
                            {
                                DocsWhere("Users", "{'Name':@UserName}")
                                .Update("{'PlayList':[Add,{'Name':@Name,'OnDate':@Now,'BestMovie':'Best Movie','GoodMovie':'Good Movie','Remove':'Remove','OnDateLabel':''}]}", name);

                                MessageBox($"Фільм {name} додано до вашого списку перегляду.", MessageBoxButtonType.Ok);
                            }
                        }
                        else //ViewUsers
                        {
                            UsersDashboard(name);
                        }

                        return true;
                    }
                case "Remove":
                    {
                        if (info.AttrPath.FirstPath == "PlayList")
                        {
                            DocsWhere("Users", info.AttrPath)
                            .Delete("{'PlayList':[$]}");
                        }
                        else if (info.AttrPath.FirstPath == "BestMovies")
                        {
                            DocsWhere("Users", info.AttrPath)
                            .Delete("{'BestMovies':[$]}");
                        }
                        else if (info.AttrPath.FirstPath == "GoodMovies")
                        {
                            DocsWhere("Users", info.AttrPath)
                            .Delete("{'GoodMovies':[$]}");
                        }

                        ViewUser(info.AttrPath);

                        return true;
                    }
                case "Share":
                    {
                        string movie;

                        if (info.AttrPath.FirstPath == "BestMovies")
                        {
                            movie = DocsWhere("Users", info.AttrPath)
                                       .Value("{'BestMovies':[{'Name':$}]}");
                        }
                        else //if (info.AttrPath.FirstPath == "GoodMovies")
                        {
                            movie = DocsWhere("Users", info.AttrPath)
                                       .Value("{'GoodMovies':[{'Name':$}]}");
                        }

                        new
                        {
                            Link = "https://fraplat.com/jupiter/Seasons/?tag=Movie:" + movie
                        }
                        .ToCollection("Share link")
                        .SetUIDimension("{'ControlType':'Link','ReadOnly':true}")
                        .OpenForm();

                        return true;
                    }
                case "BestMovie":
                case "GoodMovie":
                    {
                        var movie = DocsWhere("Users", info.AttrPath)
                                    .Value("{'PlayList':[{'Name':$}]}");

                        if (info.Action == "BestMovie")
                        {
                            DocsWhere("Users", info.AttrPath)
                            .Update("{'BestMovies':[Add,{'Name':@Movie,'OnDate':@Now,'Share':'Share','Remove':'Remove'}]}", movie);
                        }
                        else //GoodMovie
                        {
                            DocsWhere("Users", info.AttrPath)
                            .Update("{'GoodMovies':[Add,{'Name':@Movie,'OnDate':@Now,'Share':'Share','Remove':'Remove'}]}", movie);
                        }

                        DocsWhere("Users", info.AttrPath)
                        .Delete("{'PlayList':[$]}");

                        ViewUser(info.AttrPath);

                        return true;
                    }
                default:
                    return base.OnEventDimension(info);
            }
        }

        public override bool OnSecurityDimension(SecurityInfo info) => info.FindFirstValue("Name") == Context.User.Name; //@MyProfile variable

        public override void OnStart()
        {
            if (!TryAutoLogin())
            {
                FirstDocOf("Intro").OpenForm();
            }
            else
            {
                Dashboard();
            }
        }
    }
}