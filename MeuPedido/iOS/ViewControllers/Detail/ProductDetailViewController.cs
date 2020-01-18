using Foundation;
using System;
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
            product.Favorited = !product.Favorited;
            UpdateFavorite();
        }

        void UpdateFavorite()
        {
            if (product.Favorited)
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
            productValue.Text = String.Format("R$ {0:0.00}", price).Replace(".", ",");
            itemCountText.Text = quantity + " UN";
        }
    }
}