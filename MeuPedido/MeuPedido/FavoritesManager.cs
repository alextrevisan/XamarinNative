using System;
using System.IO;
using SQLite;

namespace MeuPedido
{
    public delegate void FileWriteCallback(string filename, byte[] data);
    public delegate byte[] FileReadCallback(string filename);

    public static class PlatformAppConfig
    {
        public static string DocumentsPath { get; set; }
        public static FileWriteCallback FileWrite;
        public static FileReadCallback FileRead;
    }
    //https://docs.microsoft.com/pt-br/xamarin/android/data-cloud/data-access/using-sqlite-orm
    public class FavoritesManager
    {
        private static FavoritesManager instance;
        private readonly SQLiteConnection db;
        private FavoritesManager()
        {
            string dbPath = Path.Combine(PlatformAppConfig.DocumentsPath, "meupedido.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Favorites>();
        }

        static public FavoritesManager GetInstance()
        {
            if(instance == null)
            {
                instance = new FavoritesManager();
            }
            return instance;
        }

        public void AddFavorite(Product product)
        {
            var favorite = from f in db.Table<Favorites>()
                           where f.Product_id == product.Id
                           select f;
            if(favorite.FirstOrDefault() == null)
            {
                Favorites newfavorite = new Favorites();
                newfavorite.Product_id = product.Id;
                db.Insert(newfavorite);
            }
        }

        public void RemoveFavorite(Product product)
        {
            var favorite = from f in db.Table<Favorites>()
                           where f.Product_id == product.Id
                           select f;
            var current = favorite.FirstOrDefault();
            if (current != null)
            {
                db.Delete<Favorites>(current.Id);
            }
        }

        public bool IsFavorite(Product product)
        {
            var favorite = from f in db.Table<Favorites>()
                           where f.Product_id == product.Id
                           select f;
            return favorite.FirstOrDefault() != null;
        }
    }

    [Table("Favorites")]
    internal class Favorites
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Unique]
        public long Product_id { get; set; }
    }
}
