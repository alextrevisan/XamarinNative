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
            app.Tap(x => x.Id("subButon"));
            
            

            app.ScrollDownTo(x => x.Text("Lavadora e Secadora WD103 10.1 kg Branca 127 V"), strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0,1,0));
            app.Tap(x => x.Text("Lavadora e Secadora WD103 10.1 kg Branca 127 V").Parent());
            app.ScrollDownTo(x => x.Id("addButon"));
            for(int i=0; i< 9;++i)
            {
                app.Tap(x => x.Id("addButon"));
            }

            app.WaitForElement("R$ 2.136,60");
            app.Tap(x => x.Id("addButon"));
            app.WaitForElement("R$ 1.922,94");
            app.WaitForElement("↓10,0%");

            for (int i = 0; i < 20; ++i)
            {
                app.Tap(x => x.Id("addButon"));
            }

            app.WaitForElement("R$ 1.495,62");
            app.WaitForElement("↓30,0%");

            app.Tap(x => x.Id("favoriteBtn"));

            if (platform == Platform.Android)
            {
                app.Tap(x => x.Text("Detalhes").Sibling());
                app.Tap(x => x.Id("buyBtn"));
            }
            else
            {
                app.Tap(x => x.Class("UITabBarButton"));
                app.Tap(x => x.Text("Carrinho"));
            }

            app.WaitForElement("R$ 1.495,62");
            app.WaitForElement("↓30,0%");
            app.WaitForElement("30 UN");
            app.WaitForElement("R$ 44.868,60");

            //app.Repl();

        }
        
    }
}
