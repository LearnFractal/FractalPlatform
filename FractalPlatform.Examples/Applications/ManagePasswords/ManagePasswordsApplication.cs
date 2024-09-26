using System;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ManagePasswords
{
    public class ManagePasswordsApplication : BaseApplication
    {
        public override void OnStart() =>
            this.InputBox("EncryptPassword",
                          null,
                          result =>
                          {
                              if (result.Result)
                              {
                                  var password = result.Collection
                                                   .GetFirstDoc()
                                                   .Value("{'EncryptPassword':$}");

                                  if (password?.Length >= 6)
                                  {
                                      Client.SetDefaultCollection("Passwords")
                                            .SetDefaultDimension(DimensionType.Encryption)
                                            .GetBaseDoc()
                                            .Update("{'EncryptPassword':@Password}", password);

                                      ModifyFirstDocOf("Passwords")
                                            .OpenForm();
                                  }
                                  else
                                  {
                                      MessageBox("Password should be more than 6 symbols.",
                                                 "Wrong password length",
                                                 MessageBoxButtonType.Cancel,
                                                 result => OnStart());
                                  }

                              }
                          });

        public override bool OnMenuDimension(MenuInfo info)
        {
            switch (info.Action)
            {
                case "CopyPassword":
                    var password = DocsWhere("Passwords", info.AttrPath)
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
