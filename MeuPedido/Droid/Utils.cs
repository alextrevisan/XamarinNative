using System;
using System.Collections.Generic;
using System.Net;
using Android.Graphics;

namespace MeuPedido.Droid
{
    public class Utils
    {
        static Dictionary<string, Bitmap> ImageCache = new Dictionary<string, Bitmap>();
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            if (!ImageCache.ContainsKey(url))
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        ImageCache[url] = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
            }

            return ImageCache[url];
        }
    }
}
