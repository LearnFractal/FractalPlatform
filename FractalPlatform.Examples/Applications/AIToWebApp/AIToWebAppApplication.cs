using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.AIToWebApp
{
	public class AIToWebAppApplication : BaseApplication
	{
		private void Dashboard(string question) =>
			FirstDocOf("Dashboard")
				  .ExtendDocument(DQL("{'Question':@Question}", question))
				  .OpenForm(result =>
				  {
					  var question = result.Collection
									   .GetFirstDoc()
									   .Value("{'Question':$}");

					  var newQuestion = @$"Create a json where {question}.
					  Json should starts from {{ bracket.
					  Name of json attributes should start from capital letter.
					  If json has gps coordinates, than it should be in format {{'gps':'1.11,2.22'}}.";

					  var response = AI.Generate(newQuestion, "gpt-4o");

					  if (response.CodeBlocks.Count > 0)
					  {
						  var codeBlock = response.CodeBlocks[0];

						  codeBlock.Text.ToCollection("App")
								  .SetThemeDimension(ThemeType.LightBlue)
								  .SetUIDimension("{'Style':'Save:Rebuild;Type:Link'}")
								  .SetDimension(DimensionType.Sort)
								  .OpenForm(result =>
								  {
									  if (result.Result)
									  {
										  Dashboard(question);
									  }
								  });
					  }
					  else
					  {
						  MessageBox(response.Text);
					  }
				  });
		
		public override void OnStart() => Dashboard("List of most popular youtube videos with amount of views. Add link to youtube.");
	}
}