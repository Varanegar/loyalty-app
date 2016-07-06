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
using LoyaltyAndroid.MyApp;
using Android.Support.V4.App;
using Java.Lang;

namespace LoyaltyAndroid.Fragments
{
    public class ProfileFragment : CustomeFragment
    {
        public ProfileFragment()
        {
            Title = "مشحصات من";
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ProfileLayout, container, false);
            var profileViewPager = view.FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.ProfileViewPager);
            var profileTabLayout = view.FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.ProfileTabLayout);

            var fragmentAdapter = new ProfileFragmentAdapter(ChildFragmentManager);
            profileViewPager.Adapter = fragmentAdapter;
            profileViewPager.CurrentItem = 2;
            //profileTabLayout.AddTab(profileTabLayout.NewTab().SetText("Profile1"), true);
            //profileTabLayout.AddTab(profileTabLayout.NewTab().SetText("Profile2"));
            //profileTabLayout.TabSelected += (object sender, Android.Support.Design.Widget.TabLayout.TabSelectedEventArgs e) => {
            //    if (e.Tab.Text == "Profile1")
            //        profileViewPager.CurrentItem = 0;
            //    else
            //        profileViewPager.CurrentItem = 1;
            //};
            profileTabLayout.SetupWithViewPager(profileViewPager);
            return view;
        }
    }

    public class ProfileFragmentAdapter : FragmentStatePagerAdapter
    {
        List<CustomeFragment> _fragments;
        public ProfileFragmentAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
        {
            _fragments = new List<CustomeFragment>();
            _fragments.Add(new ProfileAccountFragment());
            _fragments.Add(new ProfileAddressFragment());
            _fragments.Add(new ProfileOtherInfoFragment());
            _fragments.Add(new ProfileOriginalInfoFragment());
        }
        public override int Count
        {
            get
            {
                return _fragments.Count;
            }
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(_fragments[position].Title);
        }
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _fragments[position];
        }
    }
}