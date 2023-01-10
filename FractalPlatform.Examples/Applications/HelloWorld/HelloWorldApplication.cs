using System;
using BigDoc.Client.App;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.HelloWorld
{
    public class HelloWorldApplication : BaseApplication
    {
        public HelloWorldApplication(Guid sessionId,
                                     BigDocInstance instance,
                                     IFormFactory formFactory) : base(sessionId,
                                                                      instance,
                                                                      formFactory)
        {
        }

        public override void OnStart(Context context)
        {
            MessageBox("Hello Fractal World !");
        }
    }
}
