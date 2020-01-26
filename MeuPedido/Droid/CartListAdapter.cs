using System;
using System.Collections.Generic;
using System.Globalization;
using Android.App;
using Android.Views;
using Android.Widget;

namespace MeuPedido.Droid
{
    public class CartListAdapter : BaseAdapter<Product>
    {
        private static List<Product> curLists = new List<Product>();
        static Activity myContext;
        private static CartListAdapter instance;

        public override Product this[int position]
        {
            get
            {
                return curLists[position];
            }
        }

        public static void UpdateCart()
        {
            curLists = AppData.CurrentCart.Products();
            instance.NotifyDataSetInvalidated();
            instance.NotifyDataSetChanged();
        }

        public CartListAdapter(Activity context, List<Product> inpLists) : base()
        {
            myContext = context;
            curLists = inpLists;
            instance = this;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return curLists.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = myContext.LayoutInflater.Inflate(Resource.Layout.fragment_cart_item, null);

            var product = curLists[position];

            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;

            view.SetMinimumHeight(88 * pixelToDp);

            ImageView productImage = view.FindViewById<ImageView>(Resource.Id.cartProductImage);

            Utils.AsyncImageSet(product.Photo, productImage);

            TextView productTitle = view.FindViewById<TextView>(Resource.Id.cartProductTitle);
            TextView productDiscount = view.FindViewById<TextView>(Resource.Id.cartProductSale);
            TextView productValue = view.FindViewById<TextView>(Resource.Id.cartProductPrice);
            TextView itemCountText = view.FindViewById<TextView>(Resource.Id.cartQuantity);
            RelativeLayout discountLayout = view.FindViewById<RelativeLayout>(Resource.Id.cartSaleLayout);


            var discount = AppData.CurrentCart.DiscountFor(product);
            var price = AppData.CurrentCart.PriceFor(product);
            var quantity = AppData.CurrentCart.QuantityFor(product);


            productTitle.Text = product.Name;
            itemCountText.Text = string.Format("{0} UN", quantity);
            productValue.Text = price.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            itemCountText.Text = string.Format("{0} UN", quantity);
            discountLayout.Visibility = discount <= 0.0 ? ViewStates.Invisible : ViewStates.Visible;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");

            return view;
        }

    }
}
    