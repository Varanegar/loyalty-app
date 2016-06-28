using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoyaltyAndroid.MyApp
{
    public class CustomeFragment : Android.Support.V4.App.Fragment
    {
        public string Title;
        public override void OnStart()
        {
            base.OnStart();
            MyApp.GetInstance().Title = Title;
        }

    }
}