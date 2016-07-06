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
using LoyaltyAppLibrary.Model;

namespace LoyaltyAndroid.Fragments
{
    public class FinancialActivityDetailFragment : Android.Support.V4.App.DialogFragment
    {
        private FinancialActivityModel _item;

        public FinancialActivityDetailFragment(FinancialActivityModel item)
        {
            _item = item;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.FinancialActivityDetailLayout, container, false);
            view.FindViewById<TextView>(Resource.Id.DateTextView).Text = _item.Date.ToShortDateString();
            view.FindViewById<TextView>(Resource.Id.ActivityTextView).Text = _item.Activity;
            view.FindViewById<TextView>(Resource.Id.ActivityLocTextView).Text = _item.Location;

            return view;
        }
    }
}