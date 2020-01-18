using System;
namespace MeuPedido
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public long? Category_id { get; set; }
        public bool Favorited { get; set; } = false;
    }
}
