using System;
using System.IO;

namespace MeuPedido
{
    public static class CacheManager
    {
        public static void SaveCache(string itemName, byte[] data)
        {
            string filename = Path.Combine(PlatformAppConfig.DocumentsPath, itemName);
            PlatformAppConfig.FileWrite(filename, data);
        }

        public static byte[] LoadCache(string itemName)
        {
            string filename = Path.Combine(PlatformAppConfig.DocumentsPath, itemName);
            return PlatformAppConfig.FileRead(filename);
        }

    }
}
