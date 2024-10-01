using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Clients;
using FractalPlatform.Database.Engine.Info;
using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.AIWhatYouSee
{
	public class AIWhatYouSeeApplication : BaseApplication
	{
		public override bool OnUploadFiles(IEnumerable<UploadFileInfo> fileInfos)
		{
			return base.OnUploadFiles(fileInfos);
		}

		public override void OnStart() =>
			FirstDocOf("Dashboard").OpenForm(result =>
			{
				if (result.Result)
				{
					var question = result.Collection.FindFirstValue("Question");
					var image = result.Collection.FindFirstValue("Image");

					var bytes = ReadFileBytes(image);

					var answer = AI.Generate(question, AIModel.GPT4oMini, AIImage.FromBytes(bytes));

					MessageBox(answer.Text, MessageBoxButtonType.Ok);
				}
			});
	}
}