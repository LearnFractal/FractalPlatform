using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Clients;

namespace FractalPlatform.Examples.Applications.AIWhatYouSee
{
	public class AIWhatYouSeeApplication : BaseApplication
	{
		public override void OnStart() =>
			FirstDocOf("Dashboard").OpenForm(result =>
			{
				if (result.Result)
				{
					var question = result.Collection.FindFirstValue("Question");
					var image = result.Collection.FindFirstValue("Image");

					var bytes = ReadFileBytes(image);

					var answer = AI.Generate(question, AIModel.Llava, AIImage.FromBytes(bytes));

					MessageBox(answer.Text, MessageBoxButtonType.Ok);
				}
			});
	}
}