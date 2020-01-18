using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MeuPedido.iOS
{
    public partial class CartTableViewCell : UITableViewCell
    {
        private Product CurrentProduct;
        private List<Sale> CurrentSales;
        public CartTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public void SetData(Product product, List<Sale> sales)
        {
            CurrentProduct = product;
            CurrentSales = sales;

            productTitle.Text = product.Name;
            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            productImage.Image = Utils.UIImageFromUrl(product.Photo);





            UpdateSale();

        }

        void UpdateSale()
        {
            var currentSale = CurrentSales.Find(x => x.Category_id == CurrentProduct.Category_id);
            var currentDiscount = LoadCurrentDiscount(currentSale);
            if (currentDiscount > 0.0)
            {
                productDiscount.Hidden = false;
                CurrentProduct.SalePrice = CurrentProduct.Price - (CurrentProduct.Price * currentDiscount) / 100.0;
                productDiscount.Text = String.Format("↓{0:0.0}%", currentDiscount).Replace(".", ",");
                //@TODO corrigir a formatacão do preço
                productValue.Text = String.Format("R$ {0:0.00}", CurrentProduct.SalePrice).Replace(".", ",");
            }
            else
            {
                CurrentProduct.SalePrice = CurrentProduct.Price;
                productDiscount.Hidden = true;
                //@TODO corrigir a formatacão do preço
                productValue.Text = String.Format("R$ {0:0.00}", CurrentProduct.SalePrice).Replace(".", ",");
            }
        }

        private double LoadCurrentDiscount(Sale currentSale)
        {
            if (currentSale != null)
            {
                currentSale.Policies.Sort((x, y) => x.Min > y.Min ? -1 : 1);
                var currentPolicy = currentSale.Policies.Find(x => x.Min <= CurrentProduct.ItemCount);
                if (currentPolicy != null)
                {
                    return currentPolicy.Discount;
                }
            }
            else
            {
                productDiscount.Hidden = true;
            }
            return 0;
        }
    }
}