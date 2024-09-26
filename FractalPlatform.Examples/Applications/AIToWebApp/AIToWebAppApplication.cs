using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.AIToWebApp
{
	public class AIToWebAppApplication : BaseApplication
	{
		public void OpenApp(Collection collection)
		{
			collection.SetThemeDimension(ThemeType.LightBlue)
					.SetUIDimension(@"{'Style':'Save:Rebuild;Type:Link',
									   'AppName':{'Visible':false},
									   'Question':{'Visible':false}}")
					.SetDimension(DimensionType.Sort)
					.OpenForm(result =>
					{
						if (result.Result)
						{
							var appName = collection.FindFirstValue("AppName");
							var question = collection.FindFirstValue("Question");

							Dashboard(appName, question);
						}
					});
		}

		public override bool OnOpenForm(FormInfo info)
		{
			if (info.AttrPath.FirstPath == "ExistingApps")
			{
				var docID = info.Collection.GetWhere(info.AttrPath).UIntValue("{'ExistingApps':[{'DocID':$}]}");

				var collection = DocsWhere("Apps", docID).ToCollection();
				
				OpenApp(collection);

				return false;
			}
			else
			{
				return base.OnOpenForm(info);
			}
		}

		private void Dashboard(string appName, string question)
		{
			var apps = DocsOf("Apps").ToStorage("{'AppName':$,'OnDate':$}");

			FirstDocOf("Dashboard")
				  .ToCollection()
				  .MergeToArrayPath(apps, "ExistingApps", Constants.FIRST_DOC_ID, true)
				  .ExtendDocument(DQL("{'NewApp':{'AppName':@AppName,'Question':@Question}}", appName, question))
				  .SetThemeDimension(ThemeType.LightGreen)
				  .OpenForm(result =>
				  {
					  Collection collection;

					  var appAndQuestion = result.Collection
												 .GetFirstDoc()
												 .Values("{'NewApp':{'AppName':$,'Question':$}}");

					  var query = DocsWhere("Apps", "{'Question':@Question}", appAndQuestion[1]);

					  if (query.Any())
					  {
						  appAndQuestion = query.Values("{'AppName':$,'Question':$}");

						  collection = query.ToCollection();
					  }
					  else
					  {
						  var newQuestion = @$"Create a json where {appAndQuestion[1]}.
							Json should starts from {{ bracket.
							Name of json attributes should start from capital letter.
							If json has gps coordinates, than it should be in format {{'gps':'1.11,2.22'}}.";

						  var response = AI.Generate(newQuestion, "gpt-4o");

						  if (response.CodeBlocks.Count > 0)
						  {
							  var codeBlock = response.CodeBlocks[0];

							  collection = codeBlock.Text
												    .ToCollection(appAndQuestion[0])
													.ExtendDocument(DQL("{'AppName':@AppName,'Question':@Question,'OnDate':@Now}", appAndQuestion[0], appAndQuestion[1]));

							  AddDoc("Apps", collection.ToJson());
						  }
						  else
						  {
							  MessageBox(response.Text);

							  return;
						  }
					  }

					  OpenApp(collection);
				  });
		}

		public override void OnStart() => Dashboard("Popular youtube videos", "List of most popular youtube videos with amount of views. Add link to youtube.");
	}
}