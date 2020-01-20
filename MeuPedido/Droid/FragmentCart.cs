
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

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_cart, container, false);
            cartListView = view.FindViewById<ListView>(Resource.Id.catalog_list_view);

            cartListView.Adapter = new CartListAdapter(Activity, AppData.CurrentCart.Products());

            return view;
        }

        public static void UpdateCart()
        {
            CartListAdapter.UpdateCart();
        }
    }
}
