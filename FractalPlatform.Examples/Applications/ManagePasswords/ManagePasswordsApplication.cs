using System;
using BigDoc.Client;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ManagePasswords
{
    public class ManagePasswordsApplication : BaseApplication
    {
        public ManagePasswordsApplication(Guid sessionId,
                               BigDocInstance instance,
                               IFormFactory formFactory) : base(sessionId,
                                                               instance,
                                                               formFactory,
                                                               "ManagePasswords")
        {
        }

        public override void OnStart(Context context)
        {
            this.InputBox("EncryptPassword",
                          null,
                          result =>
                          {
                              if (result.Result)
                              {
                                  var password = result.Collection
                                                   .GetFirstDoc(context)
                                                   .Value("{'EncryptPassword':$}");

                                  if (password?.Length >= 6)
                                  {
                                      Client.SetDefaultCollection("Passwords")
                                            .SetDefaultDimension(DimensionType.Encryption)
                                            .GetBaseDoc()
                                            .Update("{'EncryptPassword':@Password}", password);

                                      Client.SetDefaultCollection("Passwords")
                                            .GetFirstDoc()
                                            .WantModifyExistingDocuments()
                                            .OpenForm();
                                  }
                                  else
                                  {
                                      MessageBox("Password should be more than 6 symbols.",
                                                 "Wrong password length",
                                                 result => OnStart(context));
                                  }

                              }
                          });


        }

        public override bool OnMenuDimension(Context context,
                                             MenuInfo menuInfo)
        {
            switch (menuInfo.Action)
            {
                case "CopyPassword":
                    var password = Client.SetDefaultCollection("Passwords")
                                         .GetWhere(menuInfo.AttrPath)
                                         .Value("{'Passwords':[{'Password':$}]}");

                    //Clipboard.SetText(password);

                    break;
                default:
                    throw new NotImplementedException();
            }

            return true;
        }
    }
}
