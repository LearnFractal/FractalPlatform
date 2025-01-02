using System.Linq;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System;
using FractalPlatform.Common.Enums;

namespace FractalPlatform.Diary
{
	public class DiaryApplication : BaseApplication
	{
		private int Calculate(uint docID, Collection collection)
		{
			var points = DocsOf("Points")
							.ToCollection();

			var sumPoints = 0;

			collection
				.ResetDimension(DimensionType.LifeTime)
				.ScanKeysAndValues((attrPath, attrValue) =>
			{
				if (attrValue.GetBoolValue())
				{
					var currAttrPath = attrPath.Clone();

					currAttrPath.DocID = Constants.FIRST_DOC_ID;

					sumPoints += points.GetValueByPath(currAttrPath).GetIntValue();
				}

				return true;
			},
			docID);

			return sumPoints;
		}

		public override object OnComputedDimension(ComputedInfo info) =>
			info.Variable switch
			{
				"Points" => Calculate(info.DocID, info.Collection),
				_ => base.OnComputedDimension(info)
			};


		private void Dashboard() => FirstDocOf("Dashboard").OpenForm();

		public override void OnStart()
		{
			const string password = "77";

			if (Context.UrlTag == password)
			{
				Dashboard();

				return;
			}

			InputBox("Password", "Enter password", result =>
			{
				if (result.Collection
						  .GetFirstDoc()
						  .IsEquals("{'Password':$}", password))
				{
					Context.UrlTag = password;

					Dashboard();
				}
				else
				{
					MessageBox("Wrong password");
				}
			});
		}

		public override bool OnEventDimension(EventInfo info)
		{

			var path = info.AttrPath.ToString();

			switch (path)
			{
				case @"Days":
					{
						ModifyDocsOf("Days").OpenForm();

						break;
					}
				case @"NewDay":
					{
						CreateNewDocFor("NewDay", "Days")
									.OpenForm(result =>
									{
										if (result.Result)
										{
											var points = Calculate(result.DocID, result.Collection);

											MessageBox($"Today you have {points} points.", MessageBoxButtonType.Ok);
										}
									});

						break;
					}
				case @"Points":
					{
						ModifyDocsOf("Points").OpenForm();

						break;
					}
				case @"Report":
					{
						var number = 0;

						var points = DocsOf("Days")
									.Values("{'Points':$}")
									.Select(val => new {
										X = ++number,
										Y = double.Parse(val)
									})
									.ToList();
						new
						{
							Control = new
							{
								Title = new
								{
									Name = "My Points",
									X = "DocID",
									Y = "Points"
								},
								Lines = new[]
								{
								new
								{
									Name = "Days",
									Points = points
								}
							}
							}
						}
						.ToCollection("Report")
						.SetUIDimension("{'ReadOnly':true,'Control':{'ControlType':'Chart','Style':'Type:LineGraphs'}}")
						.OpenForm();

						break;
					}
				default:
					{
						return base.OnEventDimension(info);
					}
			}

			return true;
		}

		public override BaseRenderForm CreateRenderForm(DOMForm form) =>
						new ExtendedRenderForm(this, form);
	}
}