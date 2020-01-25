using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using UIKit;

namespace MeuPedido.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            PlatformAppConfig.DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            PlatformAppConfig.FileWrite = (string name, byte[] data) =>
            {
                File.WriteAllBytes(name, data);
            };

            PlatformAppConfig.FileRead = (string name) =>
            {
                try
                {                    
                    return File.ReadAllBytes(name);
                }
                catch
                {
                    return null;
                }
            };

            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
