using FractalPlatform.Client.App;
using FractalPlatform.Common.Clients;

namespace FractalPlatform.Examples.Tests
{
    public abstract class BaseTests<T> where T : BaseApplication, new()
    {
        protected T _app;

        public BaseTests()
        {
            _app = new T();

            _app.REST = new RESTClient(null);
        }
    }
}