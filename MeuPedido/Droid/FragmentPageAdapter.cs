using System;
using System.Collections.Generic;
using Android.Support.V4.App;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace MeuPedido.Droid
{
    public class FragmentPageAdapter : FragmentStatePagerAdapter
    {
        private List<Fragment> fragmentList = new List<Fragment>();
        private List<string> fragmentTitleList = new List<string>();
        public FragmentPageAdapter(FragmentManager fm)
            :base(fm)
        {

        }

        public void AddFragment(Fragment fragment, string title)
        {
            fragmentList.Add(fragment);
            fragmentTitleList.Add(title);
        }

        public override int Count => fragmentList.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return fragmentList[position];
        }

    }
}
