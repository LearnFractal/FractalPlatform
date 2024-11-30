using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Sandbox
{
    public class ServiceBus : IServiceBus
    {
        public bool SendMessage(MessageInfo info)
        {
            return false;
        }
    }
}
