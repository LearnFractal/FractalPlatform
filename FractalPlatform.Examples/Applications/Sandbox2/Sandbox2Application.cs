using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.Sandbox2
{
    public class Sandbox2Application : BaseApplication
    {
        private void Ollama(string question, string answer)
        {
			new
			{
				Question = question,
				Answer = answer
			}
			.ToCollection("Ollama")
            .SetUIDimension("{'Style':'Save:Send;Cancel:false','Answer':{'ControlType':'RichTextBox','ReadOnly':true}}")
            .SetThemeDimension(ThemeType.LightBlue)
			.OpenForm(result =>
			{
                if (result.Result)
                {
                    var question = result.Collection.FindFirstValue("Question");

                    var answer = AI.Generate(question).Text;

                    Ollama(question, answer);
                }
			});
		}

		public override void OnStart() => Ollama(string.Empty, string.Empty);
    }
}
