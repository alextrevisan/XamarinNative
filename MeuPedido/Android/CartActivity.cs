
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace MeuPedido.Droid
{
    [Activity(Label = "CartActivity")]
    public class CartActivity : AppCompatActivity
    {
        private ListView cartListView;
        private TextView cartTotalItems;
        private TextView cartTotalValue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_cart);

            SupportActionBar.SetTitle(Resource.String.detail);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            

            cartListView = FindViewById<ListView>(Resource.Id.catalog_list_view);
            cartListView.Adapter = new CartListAdapter(this, AppData.CurrentCart.Products());

            cartTotalItems = FindViewById<TextView>(Resource.Id.cartTotalItems);
            cartTotalValue = FindViewById<TextView>(Resource.Id.cartTotalValue);

            UpdateCartDetails();

        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return base.OnSupportNavigateUp();
        }

        private void UpdateCartDetails()
        {
            long itemCount = 0;
            double valueTotal = 0;
            AppData.CurrentCart.Products().ForEach(x => {
                var quantity = AppData.CurrentCart.QuantityFor(x);
                var price = AppData.CurrentCart.PriceFor(x);
                itemCount += quantity;
                valueTotal += quantity * price;
            });

            cartTotalItems.Text = itemCount + " UN";
            cartTotalValue.Text = valueTotal.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}
