using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.AIToWebApp
{
	public class AIToWebAppApplication : BaseApplication
	{
		private void Dashboard(string question)
		{
			var apps = DocsOf("Apps").ToStorage("{'AppName':$,'Question':$}");

			FirstDocOf("Dashboard")
				  .ToCollection()
				  .MergeToArrayPath(apps, "Apps")
				  .ExtendDocument(DQL("{'Question':@Question}", question))
				  .OpenForm(result =>
				  {
					  List<string> appAndQuestion;
					  Collection collection;

					  var query = DocsWhere("Apps", "{'Question':@Question}", question);

					  if (query.Any())
					  {
						  appAndQuestion = query.Values("{'AppName':$,'Question':$}");

						  collection = query.ToCollection();
					  }
					  else
					  {
						  appAndQuestion = result.Collection
												 .GetFirstDoc()
												 .Values("{'AppName':$,'Question':$}");

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
													.ExtendDocument(DQL("{'AppName':@AppName,'Question':@Question}", appAndQuestion[0], appAndQuestion[1]));

							  Client.AddDoc(collection.ToJson());
						  }
						  else
						  {
							  MessageBox(response.Text);

							  return;
						  }
					  }

					  collection.SetThemeDimension(ThemeType.LightBlue)
								.SetUIDimension(@"{'Style':'Save:Rebuild;Type:Link',
												   'AppName':{'Visible':false},
												   'Question':{'Visible':false}}")
								.SetDimension(DimensionType.Sort)
								.OpenForm(result =>
								{
									  if (result.Result)
									  {
										  Dashboard(question);
									  }
								});
				  });
		}

		public override void OnStart() => Dashboard("List of most popular youtube videos with amount of views. Add link to youtube.");
	}
}