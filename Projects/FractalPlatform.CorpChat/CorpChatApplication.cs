using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;

namespace FractalPlatform.CorpChat
{
    public class CorpChatApplication : BaseApplication
    {
        public override void OnStart()
        {
        
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
