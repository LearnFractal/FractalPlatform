using FractalPlatform.Client.App;
using FractalPlatform.Database.Engine.REST;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Tests
{
    public abstract class BaseTests<T> where T : BaseApplication, new()
    {
        protected T _app;

        public BaseTests()
        {
            _app = new T();

            _app.REST = new RESTClient();
        }
    }
}