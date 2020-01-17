using System;
namespace MeuPedido
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Boolean Selected { get; set; } = true;
    }
}
