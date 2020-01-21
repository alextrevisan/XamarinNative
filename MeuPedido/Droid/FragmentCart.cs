
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace MeuPedido.Droid
{
    public class FragmentCart : Fragment
    {
        private ListView cartListView;
        private static TextView cartTotalItems;
        private static TextView cartTotalValue;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_cart, container, false);

            cartListView = view.FindViewById<ListView>(Resource.Id.catalog_list_view);
            cartListView.Adapter = new CartListAdapter(Activity, AppData.CurrentCart.Products());

            cartTotalItems = view.FindViewById<TextView>(Resource.Id.cartTotalItems);
            cartTotalValue = view.FindViewById<TextView>(Resource.Id.cartTotalValue);

            return view;
        }

        public static void UpdateCart()
        {
            CartListAdapter.UpdateCart();

            long itemCount = 0;
            double valueTotal = 0;
            AppData.CurrentCart.Products().ForEach(x => {
                var quantity = AppData.CurrentCart.QuantityFor(x);
                var price = AppData.CurrentCart.PriceFor(x);
                itemCount += quantity;
                valueTotal += quantity * price;
            });

            cartTotalItems.Text = itemCount + " UN";
            cartTotalValue.Text = String.Format("R$ {0:0.00}", valueTotal).Replace(".", ",");
        }
    }
}
