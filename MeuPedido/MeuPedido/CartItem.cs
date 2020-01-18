using System;
namespace MeuPedido
{
    public class CartItem
    {
        public long Product_Id { get; set; }
        public long Quantity { get; set; }
        public double Discount { get; set; }
        public double SalePrice { get; set; }
        public CartItem(long id, long quantity, double discount, double price)
        {
            Product_Id = id;
            Quantity = quantity;
            Discount = discount;
            SalePrice = price;
        }
    }
}
