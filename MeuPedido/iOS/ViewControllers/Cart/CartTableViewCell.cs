using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            UpdateCellData();
        }

        void UpdateCellData()
        {
            var discount = AppData.CurrentCart.DiscountFor(CurrentProduct);
            var price = AppData.CurrentCart.PriceFor(CurrentProduct);
            var quantity = AppData.CurrentCart.QuantityFor(CurrentProduct);


            productTitle.Text = CurrentProduct.Name;
            productImage.Image = Utils.UIImageFromUrl(CurrentProduct.Photo);

            productDiscount.Hidden = discount <= 0.0;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");
            //@TODO corrigir a formatacão do preço
            productValue.Text = price.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")); 
            itemCountText.Text = quantity + " UN";
        }
    }
}