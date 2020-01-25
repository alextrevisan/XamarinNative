using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using MeuPedido;
using UIKit;
class Utils
{
    static Dictionary<string, UIImage> ImageCache = new Dictionary<string, UIImage>();
    public static UIImage UIImageFromUrl(string uri)
    {
        var imgData = CacheManager.LoadCache(uri.GetHashCode().ToString());
        if (imgData != null)
        {
            UIImage bitmap = UIImage.LoadFromData(NSData.FromArray(imgData));
            ImageCache[uri] = bitmap;
        }
        else
        {
            if (!ImageCache.ContainsKey(uri))
            {
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                {
                    ImageCache[uri] = UIImage.LoadFromData(data);
                    CacheManager.SaveCache(uri.GetHashCode().ToString(), data.ToArray());
                }
            }
        }

        return ImageCache[uri];
    }

    public static void AsyncImageSet(string url, Action<UIImage> callback)
    {
        var DownloadTask = Task.Factory.StartNew(() => {
            try
            {
                callback(UIImageFromUrl(url));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        });
    }
}