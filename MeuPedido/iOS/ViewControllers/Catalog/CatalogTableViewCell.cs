using Foundation;
using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;
using static MeuPedido.Sale;

namespace MeuPedido.iOS
{
    public partial class CatalogTableViewCell : UITableViewCell
    {
        private Product CurrentProduct;

        public CatalogTableViewCell (IntPtr handle) : base (handle)
        {

        }

        public void SetData(Product product)
        {
            CurrentProduct = product;

            UpdateCellData();
            UpdateFavorite();
            CreateButtonEvents(); 
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
            productValue.Text = String.Format("R$ {0:0.00}", price).Replace(".", ",");
            itemCountText.Text = quantity + " UN";
        }

        void UpdateFavorite()
        {
            if(CurrentProduct.Favorited)
            {
                favoritedBtn.SetTitle("★", UIControlState.Normal);
                favoritedBtn.TintColor = UIColor.Gray;
            }
            else
            {
                favoritedBtn.SetTitle("☆", UIControlState.Normal);
                favoritedBtn.TintColor = UIColor.LightGray;
            }
        }

        private void CreateButtonEvents()
        {
            addItemBtn.TouchUpInside -= OnAddButtonClicked;
            subItemBtn.TouchUpInside -= OnSubButtonClicked;
            favoritedBtn.TouchUpInside -= OnFavoriteButtonClicked;

            addItemBtn.TouchUpInside += OnAddButtonClicked;
            subItemBtn.TouchUpInside += OnSubButtonClicked;
            favoritedBtn.TouchUpInside += OnFavoriteButtonClicked;
        }

        void OnAddButtonClicked(object sender, EventArgs args)
        {
            var quantity = AppData.CurrentCart.Add(CurrentProduct);

            itemCountText.Text = quantity + " UN";
            UpdateCellData();
        }

        void OnSubButtonClicked(object sender, EventArgs args)
        {
            var quantity = AppData.CurrentCart.Remove(CurrentProduct);

            itemCountText.Text = quantity + " UN";
            UpdateCellData();
        }

        void OnFavoriteButtonClicked(object sender, EventArgs args)
        {
            CurrentProduct.Favorited = !CurrentProduct.Favorited;
            UpdateFavorite();
        }
    }
}