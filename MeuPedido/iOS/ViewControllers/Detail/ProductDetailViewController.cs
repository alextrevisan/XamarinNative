using Foundation;
using System;
using System.Globalization;
using UIKit;

namespace MeuPedido.iOS
{
    public partial class ProductDetailViewController : UIViewController
    {
        private Product product;
        public ProductDetailViewController (IntPtr handle) : base (handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UpdateProductData();
            UpdateFavorite();
            CreateButtonEvents();            
        }

        public void SetProduct(Product product)
        {
            this.product = product;
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
            var quantity = AppData.CurrentCart.Add(product);

            itemCountText.Text = quantity + " UN";
            UpdateProductData();
        }

        void OnSubButtonClicked(object sender, EventArgs args)
        {
            var quantity = AppData.CurrentCart.Remove(product);

            itemCountText.Text = quantity + " UN";
            UpdateProductData();
        }

        void OnFavoriteButtonClicked(object sender, EventArgs args)
        {
            if (FavoritesManager.GetInstance().IsFavorite(product))
            {
                FavoritesManager.GetInstance().RemoveFavorite(product);
            }
            else
            {
                FavoritesManager.GetInstance().AddFavorite(product);
            }
            UpdateFavorite();
        }

        void UpdateFavorite()
        {
            if (FavoritesManager.GetInstance().IsFavorite(product))
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

        void UpdateProductData()
        {
            var discount = AppData.CurrentCart.DiscountFor(product);
            var price = AppData.CurrentCart.PriceFor(product);
            var quantity = AppData.CurrentCart.QuantityFor(product);


            productName.Text = product.Name;
            productImage.Image = Utils.UIImageFromUrl(product.Photo);
            productDescription.Text = product.Description;

            productDiscount.Hidden = discount <= 0.0;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");
            //@TODO corrigir a formatacão do preço
            productValue.Text = price.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            itemCountText.Text = quantity + " UN";
        }
    }
}