using BigDoc.Client.App;
using BigDoc.Client.UI.DOM;
using BigDoc.Database.Engine;

namespace FractalPlatform.Applications.Portal
{
    public class RenderForm : ExtendedRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base (application, form)
        {

        }

        public override string RenderStyles(DOMForm form)
        {
            if (form.Context.User.Theme == ThemeType.Dark ||
                form.Context.User.Theme == ThemeType.White)
            {
                return ".dashboard { border: 1px solid #AFAFAF; }" + base.RenderStyles(form);
            }
            else
            {
                return base.RenderStyles(form);
            }
        }
    }

}
