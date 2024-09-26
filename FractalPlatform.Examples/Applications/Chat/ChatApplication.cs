﻿using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.Chat
{
    public class ChatApplication : BaseApplication
    {
        public override void OnStart() =>
            FirstDocOf("Chats")
                .OpenForm(result =>
                   {
                       if (result.Result)
                       {
                           FirstDocOf("Chats")
                                 .Update("{'Messages':[Add,{'OnDate':@Now,'Who':@Who,'Message':@Message}]}",
                                         result.Collection
                                               .GetFirstDoc()
                                               .Values("{'Who':$,'Message':$}")
                                               .ToArray());
                       }

                       OnStart();
                   });
        
        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
