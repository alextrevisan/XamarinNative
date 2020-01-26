using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using DrawerLayout = Android.Support.V4.Widget.DrawerLayout;
using Android.Views;
using Android.Support.Design.Widget;
using System;
using Android.Support.V4.View;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;

namespace MeuPedido.Droid
{
    [Activity(Label = "ProductDetailActivity", Theme = "@style/Theme.AppCompat.Light")]
    public class ProductDetailActivity : AppCompatActivity
    {
        private Product product;
        public override bool OnSupportNavigateUp()
        {
            Finish();
            return base.OnSupportNavigateUp();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_product_detail);

            SupportActionBar.SetTitle(Resource.String.detail);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var id = Intent.GetLongExtra("product_id", -1);
            if (id < 0)
            {
                Finish();
            }
            product = AppData.Products.Find(x => x.Id == id);

            ImageView productImage = FindViewById<ImageView>(Resource.Id.detailProductImage);


            Utils.AsyncImageSet(product.Photo, productImage);

            TextView productTitle = FindViewById<TextView>(Resource.Id.detailProductName);
            TextView productDescription = FindViewById<TextView>(Resource.Id.detailProductDescription);
            TextView productDiscount = FindViewById<TextView>(Resource.Id.detailProductSale);
            TextView productValue = FindViewById<TextView>(Resource.Id.detailProductPrice);
            TextView itemCountText = FindViewById<TextView>(Resource.Id.detailQuantity);
            RelativeLayout discountLayout = FindViewById<RelativeLayout>(Resource.Id.detailSaleLayout);


            var discount = AppData.CurrentCart.DiscountFor(product);
            var price = AppData.CurrentCart.PriceFor(product);
            var quantity = AppData.CurrentCart.QuantityFor(product);


            productTitle.Text = product.Name;
            productDescription.Text = product.Description;
            itemCountText.Text = string.Format("{0} UN", quantity);
            productValue.Text = price.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            itemCountText.Text = string.Format("{0} UN", quantity);
            discountLayout.Visibility = discount <= 0.0 ? ViewStates.Invisible : ViewStates.Visible;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");


            Button addButton = FindViewById<Button>(Resource.Id.addButon);
            Button subButton = FindViewById<Button>(Resource.Id.subButon);

            addButton.Click += AddButton_Click;
            subButton.Click += SubButton_Click; ;

            ImageButton favButton = FindViewById<ImageButton>(Resource.Id.favoriteBtn);
            favButton.Click += FavButton_Click;
            favButton.SetImageResource(FavoritesManager.GetInstance().IsFavorite(product) ? Resource.Mipmap.ic_star : Resource.Mipmap.ic_star_border);

        }

        private void FavButton_Click(object sender, EventArgs e)
        {
            if (FavoritesManager.GetInstance().IsFavorite(product))
            {
                FavoritesManager.GetInstance().RemoveFavorite(product);
            }
            else
            {
                FavoritesManager.GetInstance().AddFavorite(product);
            }

            ImageButton favButton = FindViewById<ImageButton>(Resource.Id.favoriteBtn);
            favButton.SetImageResource(FavoritesManager.GetInstance().IsFavorite(product) ? Resource.Mipmap.ic_star : Resource.Mipmap.ic_star_border);
            UpdateData();
        }

        private void SubButton_Click(object sender, EventArgs e)
        {
            AppData.CurrentCart.Remove(product);
            UpdateData();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AppData.CurrentCart.Add(product);
            UpdateData();
        }

        private void UpdateData()
        {
            var discount = AppData.CurrentCart.DiscountFor(product);
            var price = AppData.CurrentCart.PriceFor(product);
            var quantity = AppData.CurrentCart.QuantityFor(product);

            TextView productDiscount = FindViewById<TextView>(Resource.Id.detailProductSale);
            TextView productValue = FindViewById<TextView>(Resource.Id.detailProductPrice);
            TextView itemCountText = FindViewById<TextView>(Resource.Id.detailQuantity);
            RelativeLayout discountLayout = FindViewById<RelativeLayout>(Resource.Id.detailSaleLayout);

            itemCountText.Text = string.Format("{0} UN", quantity);
            discountLayout.Visibility = discount <= 0.0 ? ViewStates.Invisible : ViewStates.Visible;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");
            productValue.Text = price.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));

            RunOnUiThread(() =>
            {
                FragmentCatalog.UpdateBuyButton();
                FragmentCart.UpdateCart();
                CatalogListAdapter.UpdateCatalog();
            });
            
        }
    }
}

