﻿
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
    public class FragmentCatalog : Fragment, ListView.IOnItemClickListener
    {
        private ListView catalogListView;
        public static Button buyButton;
        private CatalogListAdapter adapter;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_catalog, container, false);
            catalogListView = view.FindViewById<ListView>(Resource.Id.catalog_list_view);
            buyButton = view.FindViewById<Button>(Resource.Id.buyBtn);

            catalogListView.OnItemClickListener = this;

            adapter = new CatalogListAdapter(Activity, AppData.Products);
            catalogListView.Adapter = adapter;

            buyButton.Click += BuyBtn_Click;


            UpdateBuyButton();

            return view;
        }

        private void BuyBtn_Click(object sender, EventArgs e)
        {
            MainActivity activity = Activity as MainActivity;
            activity.SetViewPager(1);
        }

        public static void UpdateBuyButton()
        {
            double valueTotal = 0;
            AppData.CurrentCart.Products().ForEach(x => {
                var quantity = AppData.CurrentCart.QuantityFor(x);
                var price = AppData.CurrentCart.PriceFor(x);
                valueTotal += quantity * price;
            });

            buyButton.Text = string.Format("Comprar ➤ R$ {0:0.00}", valueTotal).Replace(".", ",");
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Console.WriteLine("ITEMMMM");
            var activity = new Intent(Activity, typeof(ProductDetailActivity));

            Product item = adapter[position];
            activity.PutExtra("product_id", item.Id);
            StartActivity(activity);
        }
    }
}
