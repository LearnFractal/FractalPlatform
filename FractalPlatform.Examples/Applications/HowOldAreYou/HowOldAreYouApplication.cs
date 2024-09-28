﻿using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Clients;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.HowOldAreYou
{
	public class HowOldAreYouApplication : BaseApplication
	{
		public override void OnStart() =>
			FirstDocOf("Dashboard")
			.OpenForm(result =>
			{
				if (result.Result)
				{
					var question = result.Collection.FindFirstValue("Question");
					var photo = result.Collection.FindFirstValue("Photo");

					var bytes = ReadFileBytes(photo);

					var answer = AI.Generate(question, AIModel.GPT4o, AIImage.FromBytes(bytes));

					new
					{
						Age = $"Your age by photo: {answer.Text}",
						Photo = photo
					}
					.ToCollection("Photo")
					.SetThemeDimension(ThemeType.LightBlue)
					.SetUIDimension("{'ReadOnly':true,'Age':{'ControlType':'Label'},'Image':{'ControlType':'Picture'}}")
					.OpenForm();
				}
			});
	}
}