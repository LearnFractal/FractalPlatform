using FractalPlatform.Common.Clients;

namespace FractalPlatform.Examples.Tests
{
	public class ConsoleLogger : ILog
	{
		public void Log(string message, params object[] prms)
		{ 
			Console.WriteLine(message, prms);
		}
	}

	[TestClass]
	public class GeminiTests
	{
		[TestMethod]
		public void Test1()
		{
			var logger = new ConsoleLogger();

			var client = new RESTClient(logger);

			var aiClient = new AIClient(client, logger);

			var response = aiClient.Generate("How many 2+2 ?", AIModel.Gemini);

			Assert.IsTrue(response.Text.Contains("4"));
		}
	}
}
