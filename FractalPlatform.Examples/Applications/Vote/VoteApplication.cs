using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.Vote
{
    public class VoteApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Questionary")
                  .WantMergeDocumentFor("Report")
                  .OpenForm(result => 
                  {
                      if(result.Result)
                      {
                          Client.SetDefaultCollection("Report")
                                .OpenForm();
                      }
                  });
        }
    }
}
