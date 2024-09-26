using System;
using System.Linq;
using System.Collections.Generic;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Common.Enums;

namespace FractalPlatform.RealWorld
{
    public class RealWorldApplication : DashboardApplication
    {
		private string _dashboardTag;

		private string _profileTag;

		private class Article
		{
			public string Who { get; set; }
			public string Avatar { get; set; }
			public DateTime OnDate { get; set; }
			public string Title { get; set; }
			public string Text { get; set; }
			public List<string> Tags { get; set; }
			public List<string> Likes { get; set; }
			public int CountLikes => Likes.Count;
		}

		private void OpenArticle(Collection collection, AttrPath attrPath)
		{
			var title = collection.GetWhere(attrPath)
								  .Value(DQL("{@FirstPath:[{'Title':$}]}", attrPath.FirstPath));

			DocsWhere("Articles", "{'Title':@Title}", title).OpenForm();
		}

		public override bool OnOpenForm(FormInfo info)
		{
			if (info.AttrPath.FirstPath == "Posts")
			{
				OpenArticle(info.Collection, info.AttrPath);

				return false;
			}
			else if (info.AttrPath.FirstPath == "PopularTags")
			{
				var tag = info.Collection
							  .GetWhere(info.AttrPath)
							  .Value("{'PopularTags':[$]}");

				_dashboardTag = tag;

				Dashboard();

				return false;
			}
			else
			{
				return true;
			}
		}

		public override bool OnSecurityDimension(SecurityInfo info)
		{
			if (Context.User.IsGuest)
			{
				return false;
			}

			if (info.OperationType != OperationType.Read)
			{
				return true;
			}

			var who = info.Collection.GetDoc(info.DocID).Value("{'Who':$}");

			if (string.IsNullOrEmpty(who))
			{
				return true;
			}

			return info.Variable switch
			{
				"FollowUser" => !DocsWhere("Users", "{'Name':@Who}", who)
									.AndWhere("{'Followers':[Any,@UserName]}")
									.Any() && who != Context.User.Name,
				"UnfollowUser" => DocsWhere("Users", "{'Name':@Who}", who)
									.AndWhere("{'Followers':[Any,@UserName]}")
									.Any() && who != Context.User.Name,
				"AddLike" => !DocsWhere("Articles", info.DocID)
								 .AndWhere("{'Likes':[Any,@UserName]}")
								 .Any() && who != Context.User.Name,
				"RemoveLike" => DocsWhere("Articles", info.DocID)
									.AndWhere("{'Likes':[Any,@UserName]}")
									.Any() && who != Context.User.Name,
				"RemoveComment" => DocsWhere("Articles", info.AttrPath)
										.Value("{'Comments':[{'Who':$}]}") == Context.User.Name,
				"EditArticle" => who == Context.User.Name,
				"RemoveArticle" => who == Context.User.Name,
				_ => true
			};
		}

		public override object OnComputedDimension(ComputedInfo info)
		{
			switch (info.Variable)
			{
				case "CountLikes":
					return DocsWhere("Articles", info.DocID).Values("{'Likes':[$]}").Count;
				case "CountFollowers":
					{
						var who = info.Collection.GetDoc(info.DocID).Value("{'Who':$}");

						return DocsWhere("Users", "{'Name':@Name}", who).Values("{'Followers':[$]}").Count;
					}
				case "GlobalFeedActive":
					return string.IsNullOrEmpty(_dashboardTag) || _dashboardTag == "Global" ? "active" : "";
				case "YourFeedActive":
					return _dashboardTag == "Your" ? "active" : "";
				case "MyPostsActive":
					return string.IsNullOrEmpty(_profileTag) || _profileTag == "MyPosts" ? "active" : "";
				case "LikedPostsActive":
					return _profileTag == "LikedPosts" ? "active" : "";
				case "TagFeedVisible":
					return string.IsNullOrEmpty(_dashboardTag) || _dashboardTag == "Global" || _dashboardTag == "Your" ? "style='display:none'" : "";
				case "TagFeedValue":
					return _dashboardTag;
				default:
					return base.OnComputedDimension(info);
			}
		}

		public override bool OnEventDimension(EventInfo info)
		{
			switch (info.Action)
			{
				case "Register":
					return CreateNewDocFor("Register", "Users")
						.OpenForm(result =>
						{
							if (result.Result)
							{
								var nameAndPass = result.Collection.FindFirstValues("Name", "Password");

								TryLogin(nameAndPass[0], nameAndPass[1]);

								Dashboard();
							}
						});
				case "Login":
					return Login();
				case "Logout":
					return Logout();
				case "AddArticle":
					return CreateNewDocFor("Post", "Articles").OpenForm(result => Dashboard());
				case "Settings":
					return ModifyDocsWhere("Users", "{'Name':@UserName}").OpenForm();
				case "AddLike":
					return DocsWhere("Articles", info.DocID).Update("{'Likes':[Add,@UserName]}");
				case "RemoveLike":
					return DocsWhere("Articles", info.DocID).Delete("{'Likes':[@UserName]}");
				case "FollowUser":
					{
						var who = info.Collection.FindFirstValue("Who");

						return DocsWhere("Users", "{'Name':@Who}", who)
								.Update("{'Followers':[Add,@UserName]}");
					}
				case "UnfollowUser":
					{
						var who = info.Collection.FindFirstValue("Who");

						return DocsWhere("Users", "{'Name':@Who}", who)
								.Delete("{'Followers':[@UserName]}");
					}
				case "AddComment":
					{
						var comment = info.Collection.FindFirstValue("Comment");

						return DocsWhere("Articles", info.DocID)
								.Update("{'Comments':[Add,{'Who':@UserName,'Avatar':@UserAvatar,'OnDate':@Now,'Text':@Text,'RemoveComment':''}]}", comment);
					}
				case "RemoveComment":
					{
						return DocsWhere("Articles", info.AttrPath)
								.Delete("{'Comments':[$]}");
					}
				case "EditArticle":
					{
						return ModifyDocsWhere("Articles", info.DocID)
								.ExtendUIDimension("{'Layout':'Post'}")
								.OpenForm(result => OpenArticle(info.Collection, info.AttrPath));
					}
				case "RemoveArticle":
					return DocsWhere("Articles", info.DocID).Remove();
				case "GlobalFeed":
					{
						_dashboardTag = "Global";

						return Dashboard();
					}
				case "YourFeed":
					{
						_dashboardTag = "Your";

						return Dashboard();
					}
				case "Who":
					{
						Context.UrlTag = info.AttrValue.ToString();

						_profileTag = "MyPosts";

						return Profile();
					}
				case "MyPosts":
					{
						_profileTag = "MyPosts";

						return Profile();
					}
				case "LikedPosts":
					{
						_profileTag = "LikedPosts";

						return Profile();
					}
				default:
					return base.OnEventDimension(info);
			}
		}

		private bool Login()
		{
			return FirstDocOf("Login").OpenForm(result =>
			{
				if (result.Result)
				{
					var nameAndPass = result.Collection.FindFirstValues("Name", "Password");

					if (TryLogin(nameAndPass[0], nameAndPass[1]))
					{
						Dashboard();
					}
					else
					{
						MessageBox("Wrong credentials.", "Login", MessageBoxButtonType.Ok, result => Login());
					}
				}
			});
		}

		private bool Profile()
		{
			CloseIfOpenedForm("Profile");

			var userName = Context.UrlTag;

			var posts = _profileTag == "MyPosts" ? DocsWhere("Articles", "{'Who':@Name}", userName).ToStorage() :
												   DocsWhere("Articles", "{'Likes':[Any,@Name]}", userName).ToStorage();

			var user = DocsWhere("Users", "{'Name':@Name}", userName).ToStorage();

			return FirstDocOf("Profile")
					.ToCollection()
					.ExtendDocument(DQL("{'Who':@Who}", userName))
					.ExtendDocument(user.ToJson())
					.MergeToArrayPath(posts, "Posts")
					.SetDimension(DimensionType.Pagination, "{'Posts':{'Page':{'Size':10}}}")
					.OpenForm();
		}

		private bool Dashboard()
		{
			CloseIfOpenedForm("Dashboard");

			var globalFeed = DocsOf("Articles")
									.Select<Article>()
									.OrderByDescending(x => x.OnDate);

			IEnumerable<Article> posts;

			if (string.IsNullOrEmpty(_dashboardTag) || _dashboardTag == "Global")
			{
				posts = globalFeed;
			}
			else if (_dashboardTag == "Your")
			{
				var followUsers = DocsWhere("Users", "{'Followers':[Any,@UserName]}").Values("{'Name':$}");

				posts = globalFeed.Where(x => followUsers.Any(y => y == x.Who));
			}
			else //tag
			{
				posts = globalFeed.Where(x => x.Tags.Contains(_dashboardTag));
			}

			var allTags = globalFeed.SelectMany(x => x.Tags);

			var popularTags = allTags.Distinct()
									 .Select(x => new
									 {
										 Tag = x,
										 Count = allTags.Count(y => y == x)
									 })
									 .OrderByDescending(x => x.Count)
									 .Take(10)
									 .Select(x => x.Tag);

			return FirstDocOf("Dashboard")
					.ToCollection()
					.MergeToArrayPath(posts, "Posts")
					.MergeToArrayPath(popularTags, "PopularTags")
					.SetDimension(DimensionType.Pagination, "{'Posts':{'Page':{'Size':10}}}")
					.OpenForm();
		}

		public override void OnStart()
		{
			TryAutoLogin();

			_dashboardTag = Context.UrlTag;

			Dashboard();
		}

		public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
	}
}