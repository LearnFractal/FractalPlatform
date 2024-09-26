using FractalPlatform.Examples.Applications.MyElectro;

namespace FractalPlatform.Examples.Tests
{
    [TestClass]
    public class MyElectroTests : BaseTests<MyElectroApplication>
    {
        [TestMethod]
        public void Test1()
        {
            _app.OnStart();
        }
    }
}