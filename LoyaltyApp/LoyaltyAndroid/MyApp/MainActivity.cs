using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Net;
using LoyaltyAndroid.Clients;
using LoyaltyAppLibrary.App;
using LoyaltyAndroid.Fragments;
using Anatoli.Framework.AnatoliBase;

namespace LoyaltyAndroid.MyApp
{
    [Activity(Label = "LoyaltyAndroid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        ListView _drawerList;
        DrawerLayout _drawerLayout;
        TextView _titleTextView;
        Button _menuButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _titleTextView = FindViewById<TextView>(Resource.Id.TitleTextView);
            _drawerList = FindViewById<ListView>(Resource.Id.drawer_list);
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _menuButton = FindViewById<Button>(Resource.Id.MenuButton);
            _menuButton.Click += delegate
            {
                if (_drawerLayout.IsDrawerOpen(_drawerList))
                    _drawerLayout.CloseDrawer(_drawerList);
                else
                    _drawerLayout.OpenDrawer(_drawerList);
            };
            if (Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Lollipop)
            {
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            }
            SetSupportActionBar(toolbar);

        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            var cn = (ConnectivityManager)GetSystemService(ConnectivityService);
            AnatoliClient.Initialize(new AndroidWebClient(cn), new AndroidSqliteClient(), new AndroidFileClient());
            Configuration.WebService.PortalAddress = "http://parastoo.varanegar.com:4444";
            Configuration.AppMobileAppInfo.Password = "1";
            MyApp.Initialize(this, _drawerList, _drawerLayout, _titleTextView);
            MyApp.GetInstance().PushFragment(new HomeFragment(), true, true);
        }
        public override void OnBackPressed()
        {
            if (MyApp.GetInstance().GetCurrentFragment() == null)
            {
                Finish();
            }
            else if (MyApp.GetInstance().GetCurrentFragment().GetType() == typeof(HomeFragment))
            {
                Finish();
            }
            else
            {
                base.OnBackPressed();
                MyApp.GetInstance().Title = (MyApp.GetInstance().GetCurrentFragment() as CustomeFragment).Title;
            }
        }
    }
}

