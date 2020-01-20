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

namespace MeuPedido.Droid
{
    [Activity(Label = "MeuPedido", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity, Android.Support.Design.Widget.NavigationView.IOnNavigationItemSelectedListener
    {
        private SupportToolbar supportToolbar;
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        private FragmentPageAdapter fragmentPageAdapter;
        private ViewPager viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //RequestWindowFeature(WindowFeatures.ActionBar);
            SetContentView(Resource.Layout.activity_main);
            ConnectControls();

            

            LoadData();

            

            
            //supportToolbar.ShowContextMenu();
        }

        private async void LoadData()
        {
            await AppData.GetInstance().UpdateData();
            SetupViewPager();
        }

        private void SetupViewPager()
        {
            fragmentPageAdapter = new FragmentPageAdapter(SupportFragmentManager);
            viewPager = FindViewById<ViewPager>(Resource.Id.main_fragment);

            fragmentPageAdapter.AddFragment(new FragmentCatalog(), "Catalog");
            fragmentPageAdapter.AddFragment(new FragmentCart(), "Cart");
            fragmentPageAdapter.AddFragment(new FragmentSales(), "Sales");
            viewPager.Adapter = fragmentPageAdapter;
            navigationView.SetNavigationItemSelectedListener(this);
        }

        private void ConnectControls()
        {
            supportToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            supportToolbar.SetTitleTextColor(Color.White.ToArgb());
            SetSupportActionBar(supportToolbar);


            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Console.WriteLine("ITEM ID = " + item.ItemId);
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Console.WriteLine("ITEM ID = " + item.ItemId);
            switch (item.ItemId)
            {
                case Resource.Id.catalog:
                    drawerLayout.CloseDrawers();
                    viewPager.SetCurrentItem(0, true);
                    return true;
                case Resource.Id.sales:
                    drawerLayout.CloseDrawers();
                    viewPager.SetCurrentItem(1, true);
                    return true;
                case Resource.Id.cart:
                    drawerLayout.CloseDrawers();
                    viewPager.SetCurrentItem(2, true);
                    return true;
                default:
                    return false;
            }
        }

        public void SetViewPager(int fragmentNumber)
        {
            viewPager.SetCurrentItem(fragmentNumber, true);
        }
    }
}

