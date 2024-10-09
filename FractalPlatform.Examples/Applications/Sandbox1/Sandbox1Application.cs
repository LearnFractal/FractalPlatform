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
		private void GetFilesFromZip(Stream zipStream)
		{
			using (ZipArchive archive = new ZipArchive(zipStream))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					var ext = Path.GetExtension(entry.FullName);

					using (var stream = entry.Open())
					{
						using (var tr = new StreamReader(stream, System.Text.Encoding.GetEncoding("windows-1251")))
						{
							var lines = new List<string>();

							while (!tr.EndOfStream)
							{
								var line = tr.ReadLine();

								lines.Add(line);
							}

							File.AppendAllLines(@"C:\ffmpeg\test.txt", lines);

						}
					}
				}
			}

		}

		public override void OnStart()
		{
			var bytes = File.OpenRead(@"C:\Users\tuzvy\Downloads\20241004163046-56.zip");

			GetFilesFromZip(bytes);

		}
	}
}
