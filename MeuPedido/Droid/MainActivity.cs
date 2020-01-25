using Android.App;
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
using System.IO;

namespace MeuPedido.Droid
{
    [Activity(Label = "MeuPedido", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity,
        Android.Support.Design.Widget.NavigationView.IOnNavigationItemSelectedListener,
        Android.Support.V4.View.ViewPager.IOnPageChangeListener
    {
        
        private SupportToolbar supportToolbar;
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        private FragmentPageAdapter fragmentPageAdapter;
        private ViewPager viewPager;

        private Dictionary<int, Category> categoryItems = new Dictionary<int, Category>();

        protected override void OnCreate(Bundle savedInstanceState)
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

            base.OnCreate(savedInstanceState);
            //RequestWindowFeature(WindowFeatures.ActionBar);
            SetContentView(Resource.Layout.activity_main);
            ConnectControls();

            //SetHasOptionsMenu(true);

            LoadData();

            

            
            //supportToolbar.ShowContextMenu();
        }

        private async void LoadData()
        {
            await AppData.GetInstance().UpdateData();
            SetupViewPager();
            InvalidateOptionsMenu();
        }

        private void SetupViewPager()
        {
            fragmentPageAdapter = new FragmentPageAdapter(SupportFragmentManager);
            viewPager = FindViewById<ViewPager>(Resource.Id.main_fragment);

            fragmentPageAdapter.AddFragment(new FragmentCatalog(), "Catalog");
            fragmentPageAdapter.AddFragment(new FragmentCart(), "Cart");
            viewPager.Adapter = fragmentPageAdapter;
            viewPager.AddOnPageChangeListener(this);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        private void ConnectControls()
        {
            supportToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            supportToolbar.SetTitleTextColor(Color.White.ToArgb());
            SetSupportActionBar(supportToolbar);

            SupportActionBar.SetTitle(Resource.String.catalog);
            SupportActionBar.SetHomeButtonEnabled(true);
            
            SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);

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
            Console.WriteLine(item.ItemId);
            if(categoryItems.ContainsKey(item.ItemId))
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
                case Android.Resource.Id.Home:
                    if(viewPager == null) { return true; }
                    if(viewPager.CurrentItem == 0)
                    {
                        drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    }
                    else
                    {
                        viewPager.SetCurrentItem(0, true);
                    }
                    
                    return true;
                case Resource.Id.action_filter:

                default:
                    return base.OnOptionsItemSelected(item);
            }
            
            
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Resource.Id.catalog:
                    drawerLayout.CloseDrawers();
                    viewPager.SetCurrentItem(0, true);
                    return true;
                case Resource.Id.cart:
                    drawerLayout.CloseDrawers();
                    viewPager.SetCurrentItem(1, true);
                    return true;
                default:
                    return false;
            }
        }

        public void SetViewPager(int fragmentNumber)
        {
            viewPager.SetCurrentItem(fragmentNumber, true);
        }

        public void OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            switch(position)
            {
                case 1:
                    SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_arrow_back);
                    SupportActionBar.SetTitle(Resource.String.cart);
                    return;
                default:
                    SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu);
                    SupportActionBar.SetTitle(Resource.String.catalog);
                    return;
            }
        }
    }
}

