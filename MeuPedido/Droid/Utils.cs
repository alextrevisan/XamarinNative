using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Widget;

namespace MeuPedido.Droid
{
    public class Utils
    {
        static Dictionary<string, Bitmap> ImageCache = new Dictionary<string, Bitmap>();
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            if (!ImageCache.ContainsKey(url))
            {

                var imgData = CacheManager.LoadCache(url.GetHashCode().ToString());
                if(imgData != null)
                {
                    Bitmap bitmap = BitmapFactory.DecodeByteArray(imgData, 0, imgData.Length);
                    ImageCache[url] = bitmap;
                }
                else
                {
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            ImageCache[url] = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                            CacheManager.SaveCache(url.GetHashCode().ToString(), imageBytes);
                        }
                    }
                }
            }

            return ImageCache[url];
        }

        public static void AsyncImageSet(string url, ImageView imageView)
        {
            var DownloadTask = Task.Factory.StartNew(() => {
                try
                {
                    Bitmap image = GetImageBitmapFromUrl(url);
                    imageView.SetImageBitmap(image);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
