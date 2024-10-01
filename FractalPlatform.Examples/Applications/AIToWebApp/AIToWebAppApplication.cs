using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Clients;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.AIToWebApp
{
	public class AIToWebAppApplication : BaseApplication
	{
		private bool OpenApp(uint docID) =>
			DocsWhere("Apps", docID).OpenForm(result =>
			 {
				 if (result.Result)
				 {
					 var appName = result.Collection.FindFirstValue("AppName");
					 var question = result.Collection.FindFirstValue("Question");

					 Dashboard(appName, question);
				 }
			 });

		public override bool OnOpenForm(FormInfo info)
		{
			if (info.Collection.Name == "Apps" &&
				info.DocID != Constants.ANY_DOC_ID &&
				info.AttrPath.IsEmpty)
			{
				OpenApp(info.DocID);

				return false;
			}

			return base.OnOpenForm(info);
		}

		public override bool OnEventDimension(EventInfo info)
		{
			if (info.Action == "ExistingApps")
			{
				return DocsOf("Apps")
						.SetUIDimension("{'ReadOnly':true,'Style':'Type:Link'}")
						.OpenForm("{'AppName':$,'Question':$,'OnDate':$}");
			}

			return base.OnEventDimension(info);
		}

		private void Dashboard(string appName, string question)
		{
			FirstDocOf("Dashboard")
				  .ToCollection()
				  .ExtendDocument(DQL("{'AppName':@AppName,'Question':@Question}", appName, question))
				  .SetThemeDimension(ThemeType.LightGreen)
				  .OpenForm(result =>
				  {
					  uint docID;

					  var appAndQuestion = result.Collection
												 .GetFirstDoc()
												 .Values("{'AppName':$,'Question':$}");

					  var query = DocsWhere("Apps", "{'Question':@Question}", appAndQuestion[1]);

					  if (query.Any())
					  {
						  docID = query.GetFirstID();
					  }
					  else
					  {
						  var newQuestion = @$"Create a json where {appAndQuestion[1]}.
							Json should starts from {{ bracket.
							Name of json attributes should start from capital letter.
							If json has gps coordinates, than it should be in format {{'gps':'1.11,2.22'}}.";

						  var response = AI.Generate(newQuestion, AIModel.GPT4o);

						  if (response.CodeBlocks.Count > 0)
						  {
							  var codeBlock = response.CodeBlocks[0];

							  var collection = codeBlock.Text
												    .ToCollection(appAndQuestion[0])
													.ExtendDocument(DQL("{'AppName':@AppName,'Question':@Question,'OnDate':@Now}", appAndQuestion[0], appAndQuestion[1]));

							  docID = AddDoc("Apps", collection.ToJson());
						  }
						  else
						  {
							  MessageBox(response.Text);

							  return;
						  }
					  }

					  OpenApp(docID);
				  });
		}

		public override void OnStart() => Dashboard("Popular youtube videos", "List of most popular youtube videos with amount of views. Add link to youtube.");
	}
}