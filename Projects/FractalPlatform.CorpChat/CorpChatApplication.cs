using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine.Query;

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
