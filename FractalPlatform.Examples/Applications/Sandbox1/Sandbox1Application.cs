using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine;
using Newtonsoft.Json;
using FractalPlatform.Common.Clients;
using System;

namespace FractalPlatform.Examples.Applications.Sandbox1
{
	public class Sandbox1Application : BaseApplication
	{
		public override void OnStart()
		{
			/*
			@"Сгенерируй два джисона. Один описывает кратко пользователя. 
					Другой описывает валидацию полей. 
					Используй только атрибуты для валидации: MinLen, MaxLen, IsRequired 
					Пример: Джисон {'Name':'John'}
					Валидация: {'Name':{'MinLen':3,'MaxLen':256}}"
			*/

			//var xx = AI.Generate("Create a html to represent data of User. Data in this html should be marked by tag <control attr='name'></control> where name is name of user. Mark other Users props by this rule");

			//foreach (var item in xx.CodeBlocks)
			//{
			//	Console.WriteLine(item);
			//}


			
		}
	}
}
