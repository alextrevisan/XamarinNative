using System;
using System.Collections.Generic;

namespace MeuPedido
{
    public class Cart
    {
        public Dictionary<long, CartItem> CurrentCart;
        public Cart()
        {
            CurrentCart = new Dictionary<long, CartItem>();
        }

        public long Add(Product product)
        {
            if(!CurrentCart.ContainsKey(product.Id))
            {
                var discount = CalculateDiscount(product, 1);
                var price = CalculatePrice(product, discount);
                CurrentCart[product.Id] = new CartItem(product.Id, 1, discount, price);
            }
            else
            {
                CurrentCart[product.Id].Quantity++;
                var discount = CalculateDiscount(product, CurrentCart[product.Id].Quantity);
                var price = CalculatePrice(product, discount);
                CurrentCart[product.Id].SalePrice = price;
                CurrentCart[product.Id].Discount = discount;
            }
            return CurrentCart[product.Id].Quantity;
        }

        public long Remove(Product product)
        {
            if (!CurrentCart.ContainsKey(product.Id))
            {
                return 0;
            }
            CurrentCart[product.Id].Quantity = Math.Max(CurrentCart[product.Id].Quantity -1, 0);
            var discount = CalculateDiscount(product, CurrentCart[product.Id].Quantity);
            var price = CalculatePrice(product, discount);

            CurrentCart[product.Id].SalePrice = price;
            CurrentCart[product.Id].Discount = discount;

            return CurrentCart[product.Id].Quantity;
        }

        public double DiscountFor(Product product)
        {
            if (CurrentCart.ContainsKey(product.Id))
                return CurrentCart[product.Id].Discount;

            return 0;
        }

        public double PriceFor(Product product)
        {
            if (CurrentCart.ContainsKey(product.Id))
            {
                return CurrentCart[product.Id].SalePrice;
            }
            return product.Price; 
        }

        public long QuantityFor(Product product)
        {
            if (CurrentCart.ContainsKey(product.Id))
            {
                return CurrentCart[product.Id].Quantity;
            }
            return 0;
        }

        public List<Product> Products()
        {
            return AppData.Products.FindAll(x =>
            {
                return CurrentCart.ContainsKey(x.Id) && CurrentCart[x.Id].Quantity > 0;
            });
        }

        private double CalculateDiscount(Product product, long quantity)
        {
            var currentSale = AppData.Sales.Find(x => x.Category_id == product.Category_id);
            
            if (currentSale != null)
            {
                currentSale.Policies.Sort((x, y) => x.Min > y.Min ? -1 : 1);
                var currentPolicy = currentSale.Policies.Find(x => x.Min <= quantity);
                if (currentPolicy != null)
                {
                    return currentPolicy.Discount;
                }
            }
            return 0;
        }

        private double CalculatePrice(Product product, double discount)
        {
            return product.Price - (product.Price * discount) / 100;
        }
    }
}
