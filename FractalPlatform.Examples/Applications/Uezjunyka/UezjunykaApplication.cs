using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.Uezjunyka
{
    public class UezjunykaApplication : BaseApplication
    {
        public override void OnStart() =>
            CreateNewDocFor("NewUezjunyka", "Uezjunyka")
                  .OpenForm(result => {
                        MessageBox("Спасибо, Вы зарегестрированы !", "Регистрация",
                                   MessageBoxButtonType.Ok,
                                   result => DocsOf("Uezjunyka").OpenForm());
                  });
    }
}