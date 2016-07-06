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

namespace LoyaltyAndroid.Fragments
{
    public class ProfileAccountFragment : CustomeFragment
    {
        public ProfileAccountFragment()
        {
            Title = "حساب کاربری";
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.ProfileAccountLayout, container, false);
            var changePassTextView = view.FindViewById<TextView>(Resource.Id.ChangePassTextView);
            changePassTextView.Click += delegate
            {
                ChangePasswordDialogFragment fragment = new ChangePasswordDialogFragment();
                fragment.Show(FragmentManager, "ChangePasswordDialogFragment");
            };
            return view;
        }
    }
}