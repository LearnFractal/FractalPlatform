using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System;

namespace FractalPlatform.Examples.Applications.Diary
{
    public class DiaryApplication : BaseApplication
    {
        private int Calculate(uint docID, Collection collection)
        {
            var points = DocsOf("Points").ToCollection();
            
            var sumPoints = 0;

            collection.ScanKeysAndValues((attrPath, attrValue) =>
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
        
        public override bool OnEventDimension(EventInfo info) =>
            info.Action switch
            {
                "NewDay" => CreateNewDocFor("NewDay", "Days")
                                .OpenForm(result =>
                                {
                                    if (result.Result)
                                    {
                                        var points = Calculate(result.DocID, result.Collection);

                                        MessageBox($"Today you have {points} points.", MessageBoxButtonType.Ok);
                                    }
                                }),
                "Days" => ModifyDocsOf("Days").OpenForm(),
                "Points" => ModifyDocsOf("Points").OpenForm(),
                _ => throw new ArgumentException($"{info.Action} event handler not found")
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
    }
}