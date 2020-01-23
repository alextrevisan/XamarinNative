using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using MeuPedido;

namespace Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("Primeira tela");
            app.Tap(x => x.Id("addButon"));
            app.WaitForElement("1 UN");
            app.Tap(x => x.Id("addButon"));
            app.WaitForElement("2 UN");
            app.Tap(x => x.Id("subButon"));
            app.WaitForElement("1 UN");
            if (platform == Platform.Android)
            {
                app.Tap(x => x.Id("buyBtn"));
                app.WaitForElement("1 UN");
            }

        }
        
    }
}
