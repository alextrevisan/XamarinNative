using System.Collections.Generic;
using Foundation;
using UIKit;
class Utils
{
    static Dictionary<string, UIImage> ImageCache = new Dictionary<string, UIImage>();
    public static UIImage UIImageFromUrl(string uri)
    {
        if(!ImageCache.ContainsKey(uri))
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                ImageCache[uri] = UIImage.LoadFromData(data);
        }

        return ImageCache[uri];
    }
}