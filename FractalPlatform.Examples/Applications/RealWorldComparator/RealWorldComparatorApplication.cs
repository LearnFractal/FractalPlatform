using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.RealWorldComparator
{
	public class RealWorldComparatorApplication : BaseApplication
	{
		private class Repo
		{
			public string Name { get; set; }
			public string Repository { get; set; }
			public string Language { get; set; }
		}

		private class FileType
		{
			public string Extension { get; set; }
			public int Lines { get; set; }
			public long Length { get; set; }
		}

		public override List<string> OnEnumDimension(EnumInfo info)
		{
			uint docID = info.Variable switch
			{
				"Frontend" => 1,
				"Backend" => 2,
				"Fullstack" => 3,
				_ => 1
			};

			var list = DocsWhere("Repos", docID)
						.Select<Repo>("{'Projects':[!$]}")
						.Select(x => $"{x.Name} | {x.Language} | {x.Repository}")
						.ToList();

			list.Insert(0, "None");

			return list;
		}

		private List<FileType> Report(string alias)
		{
			if (alias == "None")
				return new List<FileType>();

			var parts = alias.Split('|');

			var url = parts[2].Trim() + "/archive/refs/heads/master.zip";

			var zipData = REST.GetBytes(url);

			return GetFilesFromZip(zipData);
		}

		private List<FileType> GetFilesFromZip(byte[] zipData)
		{
			var files = new List<FileType>();

			using (MemoryStream zipStream = new MemoryStream(zipData))
			{
				using (ZipArchive archive = new ZipArchive(zipStream))
				{
					foreach (ZipArchiveEntry entry in archive.Entries)
					{
						var ext = Path.GetExtension(entry.FullName);

						if (!string.IsNullOrEmpty(ext) &&
							ext != ".png" &&
							ext != ".jpg" &&
							ext != ".jpeg" &&
							ext != ".gif" &&
							ext != ".bmp" &&
							ext != ".webp" &&
							ext != ".md" &&
							ext != ".gitignore" &&
							ext != ".ico")
						{
							using (var stream = entry.Open())
							{
								using (var sr = new StreamReader(stream))
								{
									var lines = sr.ReadToEnd()
												  .Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries)
												  .Where(x => !string.IsNullOrEmpty(x));

									files.Add(new FileType
									{
										Extension = ext,
										Lines = lines.Count(),
										Length = entry.Length
									});
								}
							}
						}
					}
				}
			}

			return files.GroupBy(x => x.Extension,
								 (k, g) => new FileType
								 {
									 Extension = k,
									 Length = g.Sum(x => x.Length)
								 })
								 .OrderByDescending(x => x.Length)
								 .ToList();
		}

		private string GetPart(string parts, int index) => parts.Split('|')[index].Trim();

		public override void OnStart()
		{
			FirstDocOf("Compare")
				.OpenForm(result =>
			{
				var frontend = result.Collection.FindFirstValue("Frontend");
				var backend = result.Collection.FindFirstValue("Backend");
				var fullstack = result.Collection.FindFirstValue("Fullstack");
				var fractalPlatform = result.Collection.FindFirstValue("FractalPlatform");

				if ((fullstack == "None" && frontend != "None" && backend != "None") ||
				   (fullstack != "None" && frontend == "None" && backend == "None"))
				{
					var frontendFiles = Report(frontend);
					var backendFiles = Report(backend);
					var fullstackFiles = Report(fullstack);
					var fractalPlatformFiles = Report(fractalPlatform);
					var secondTotal = fractalPlatformFiles.Sum(x => x.Length);

					object obj;

					if (fullstack == "None")
					{
						var firstTotal = frontendFiles.Sum(x => x.Length) +
										 backendFiles.Sum(x => x.Length);

						obj = new
						{
							Result = $"Fractal Platform has less code (in {firstTotal / secondTotal} times)",
							First = new
							{
								Frontend = new
								{
									Name = GetPart(frontend, 0),
									Language = GetPart(frontend, 1),
									Files = frontendFiles,
									Sources = GetPart(frontend, 2)
								},
								Backend = new
								{
									Name = GetPart(backend, 0),
									Language = GetPart(backend, 1),
									Files = backendFiles,
									Sources = GetPart(backend, 2)
								},
								Total = $"{firstTotal / 1024} kb"
							},
							Second = new
							{
								FractalPlatform = new
								{
									Name = GetPart(fractalPlatform, 0),
									Language = GetPart(fractalPlatform, 1),
									Files = fractalPlatformFiles,
									Sources = GetPart(fractalPlatform, 2)
								},
								Total = $"{secondTotal / 1024} kb"
							}
						};
					}
					else
					{
						var firstTotal = fullstackFiles.Sum(x => x.Length);

						obj = new
						{
							Result = $"Fractal Platform has less code (in {firstTotal / secondTotal} times)",
							First = new
							{
								Fullstack = new
								{
									Name = GetPart(fullstack, 0),
									Language = GetPart(fullstack, 1),
									Files = fullstackFiles,
									Sources = GetPart(fullstack, 2)
								},
								Total = $"{firstTotal / 1024} kb"
							},
							Second = new
							{
								FractalPlatform = new
								{
									Name = GetPart(fractalPlatform, 0),
									Language = GetPart(fractalPlatform, 1),
									Files = fractalPlatformFiles,
									Sources = GetPart(fractalPlatform, 2)
								},
								Total = $"{secondTotal / 1024} kb"
							}
						};
					}

					obj.ToCollection("Report")
					   .SetUIDimension(@"{'ReadOnly':true,
										  'First': {
											'Frontend':{'Sources':{'ControlType':'Link'}},
											'Backend':{'Sources':{'ControlType':'Link'}},
											'Fullstack':{'Sources':{'ControlType':'Link'}}
										  },
										  'Second': {
											'FractalPlatform':{'Sources':{'ControlType':'Link'}}
										  },
										  'Result':{'ControlType':'Label'}}")
					   .OpenForm();
				}
				else
				{
					MessageBox("You should choose: Frontend + Backend project OR fullstack project", MessageBoxButtonType.Ok);
				}
			});
		}
	}
}
