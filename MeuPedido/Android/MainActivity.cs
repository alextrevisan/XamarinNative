using System;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Globalization;
using System.Collections.Generic;

namespace MeuPedido.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, ListView.IOnItemClickListener
    {
        private ListView catalogListView;
        public static Button buyButton;
        private CatalogListAdapter adapter;

        private Dictionary<int, Category> categoryItems = new Dictionary<int, Category>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetTitle(Resource.String.catalog);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);


            SetConfig();
            

            catalogListView = FindViewById<ListView>(Resource.Id.catalog_list_view);
            buyButton = FindViewById<Button>(Resource.Id.buyBtn);

            catalogListView.OnItemClickListener = this;

            adapter = new CatalogListAdapter(this, AppData.Products);
            catalogListView.Adapter = adapter;

            buyButton.Click += BuyBtn_Click;

            LoadData();
        }

        private void SetConfig()
        {
            PlatformAppConfig.DocumentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            PlatformAppConfig.FileWrite = (string name, byte[] data) =>
            {
                File.WriteAllBytes(name, data);
            };

            PlatformAppConfig.FileRead = (string name) =>
            {
                try
                {
                    return File.ReadAllBytes(name);
                }
                catch
                {
                    return null;
                }
            };
        }

        private async void LoadData()
        {
            await AppData.GetInstance().UpdateData();
            InvalidateOptionsMenu();
            adapter = new CatalogListAdapter(this, AppData.Products);
            catalogListView.Adapter = adapter;
        }

        private void BuyBtn_Click(object sender, EventArgs e)
        {
            var activity = new Intent(this, typeof(CartActivity));
            StartActivity(activity);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            /*MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;*/

            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            //Propriedade showAsActions não funciona no XML, setando manual
            menu.GetItem(0).SetShowAsAction(ShowAsAction.Always);

            var id = View.GenerateViewId();
            categoryItems[id] = null;
            menu.GetItem(0).SubMenu.Add(0, id, 0, "Todas as categorias");

            AppData.Categories.ForEach(x =>
            {
                var id = View.GenerateViewId();
                categoryItems[id] = x;
                menu.GetItem(0).SubMenu.Add(0, id, (int)x.Id, x.Name);
            });
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (categoryItems.ContainsKey(item.ItemId))
            {
                AppData.Categories.ForEach(x => {
                    x.Selected = false;
                });

                if (categoryItems[item.ItemId] != null)
                {
                    categoryItems[item.ItemId].Selected = true;
                }
                CatalogListAdapter.UpdateCatalog();
            }
            switch (item.ItemId)
            {
                /*case Android.Resource.Id.Home:
                    if (viewPager == null) { return true; }
                    if (viewPager.CurrentItem == 0)
                    {
                        drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    }
                    else
                    {
                        viewPager.SetCurrentItem(0, true);
                    }

                    return true;*/
                case Resource.Id.action_filter:

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_catalog)
            {
                
            }
            else if (id == Resource.Id.nav_cart)
            {
                var activity = new Intent(this, typeof(CartActivity));
                StartActivity(activity);
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var activity = new Intent(this, typeof(ProductDetailActivity));

            Product item = adapter[position];
            activity.PutExtra("product_id", item.Id);
            StartActivity(activity);
        }

        public static void UpdateBuyButton()
        {
            double valueTotal = 0;
            AppData.CurrentCart.Products().ForEach(x => {
                var quantity = AppData.CurrentCart.QuantityFor(x);
                var price = AppData.CurrentCart.PriceFor(x);
                valueTotal += quantity * price;
            });

            buyButton.Text = "Comprar ➤ " + valueTotal.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}

