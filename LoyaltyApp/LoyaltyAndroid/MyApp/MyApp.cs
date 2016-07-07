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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using LoyaltyAppLibrary.App;
using LoyaltyAndroid.Fragments;
using Anatoli.Framework.AnatoliBase;

namespace LoyaltyAndroid.MyApp
{
    public class MyApp
    {
        static MyApp _instance;
        AppCompatActivity _context;
        int _currentVersionCode;
        int _oldVersionCode;
        String PREFS_NAME = "MyPrefsFile";
        String PREF_VERSION_CODE_KEY = "version_code";
        int DOESNT_EXIST = -1;
        ListView _drawerListView;
        DrawerLayout _drawerLayout;
        TextView _titleTextView;
        public static MyApp GetInstance()
        {
            if (_instance == null)
                throw new NullReferenceException();
            return _instance;
        }
        public static void Initialize(AppCompatActivity context, ListView drawerList, DrawerLayout drawerLayout, TextView titleTextView)
        {
            _instance = new MyApp(context);
            _instance._drawerListView = drawerList;
            _instance._drawerLayout = drawerLayout;
            _instance._titleTextView = titleTextView;
            GetInstance().RefreshMenu();
        }
        private MyApp(AppCompatActivity context)
        {
            try
            {
                _context = context;
                _currentVersionCode = _context.PackageManager.GetPackageInfo(_context.PackageName, 0).VersionCode;
                var prefs = _context.GetSharedPreferences(PREFS_NAME, FileCreationMode.Private);
                _oldVersionCode = prefs.GetInt(PREF_VERSION_CODE_KEY, DOESNT_EXIST);
                if (_currentVersionCode == _oldVersionCode)
                {
                    // This is just a normal run
                }
                else if (_oldVersionCode == DOESNT_EXIST)
                {
                    // TODO This is a new install (or the user cleared the shared preferences)
                    AnatoliClient.GetInstance().DbClient.Create();
                    prefs.Edit().PutInt(PREF_VERSION_CODE_KEY, _currentVersionCode).Commit();
                }
                else if (_currentVersionCode > _oldVersionCode)
                {
                    // TODO This is an upgrade
                    AnatoliClient.GetInstance().DbClient.Upgrade(_currentVersionCode, _oldVersionCode);
                    prefs.Edit().PutInt(PREF_VERSION_CODE_KEY, _currentVersionCode).Commit();
                }
                prefs.Edit().PutInt(PREF_VERSION_CODE_KEY, _currentVersionCode).Commit();
            }
            catch
            {
            }
        }
        public int CurrentVersion
        {
            get
            {
                return _currentVersionCode;
            }
        }
        public int OldVersion
        {
            get
            {
                return _oldVersionCode;
            }
        }
        public void PushFragment(CustomeFragment fragment, Tuple<string, string> parameter, bool force = false, bool allowStateLoss = false)
        {
            if (fragment == null)
            {
                throw new ArgumentNullException("fragment is Null");
            }
            Bundle bundle = new Bundle();
            bundle.PutString(parameter.Item1, parameter.Item2);
            fragment.Arguments = bundle;
            PushFragment(fragment, force, allowStateLoss);
        }
        public void PushFragment(CustomeFragment fragment, bool force = false, bool allowStateLoss = false)
        {
            if (fragment == null)
            {
                throw new ArgumentNullException();
            }
            var curr = GetCurrentFragment();
            if (curr != null)
            {
                if ((fragment.GetType() == curr.GetType()) && !force)
                {
                    return;
                }
            }
            var transaction = (_context as AppCompatActivity).SupportFragmentManager.BeginTransaction();
            transaction.SetCustomAnimations(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
            transaction.Replace(Resource.Id.content_frame, fragment, fragment.Tag);
            transaction.AddToBackStack(null);
            if (allowStateLoss)
                transaction.CommitAllowingStateLoss();
            else
                transaction.Commit();
            Title = fragment.Title;
        }
        public Android.Support.V4.App.Fragment GetCurrentFragment()
        {
            try
            {
                var f = _context.SupportFragmentManager.FindFragmentById(Resource.Id.content_frame);
                return f;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string Title
        {
            set { _titleTextView.Text = value; }
        }

        public bool IsUserLoggedIn { get { return true; } }

        List<MenuItem> _menuItems;
        public void RefreshMenu()
        {

            _menuItems = new List<MenuItem>();
            if (IsUserLoggedIn)
            {
                _menuItems.Add(new MenuItem()
                {
                    Title = _context.Resources.GetString(Resource.String.Profile),
                    Type = MenuItem.MenuType.Profile
                });
                _menuItems.Add(new MenuItem()
                {
                    Title = _context.Resources.GetString(Resource.String.NonFinancialActivitues),
                    Type = MenuItem.MenuType.NonFinancialActivitues
                });
                _menuItems.Add(new MenuItem()
                {
                    Title = _context.Resources.GetString(Resource.String.FinancialActivitues),
                    Type = MenuItem.MenuType.FinancialActivitues
                });
                _menuItems.Add(new MenuItem()
                {
                    Title = _context.Resources.GetString(Resource.String.ClubActivities),
                    Type = MenuItem.MenuType.ClubActivities
                });
                _menuItems.Add(new MenuItem()
                {
                    Title = _context.Resources.GetString(Resource.String.Dashboard),
                    Type = MenuItem.MenuType.Dashboard
                });
            }
            else
                _menuItems.Add(new MenuItem()
                {
                    Title = _context.Resources.GetString(Resource.String.Login),
                    Type = MenuItem.MenuType.Login
                });
            _menuItems.Add(new MenuItem()
            {
                Title = "خروج",
                Type = MenuItem.MenuType.Logout
            });
            _drawerListView.Adapter = new MenuAdapter(_context, _menuItems);
            _drawerListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                MenuItem item = null;
                if (_menuItems.Count > e.Position)
                    item = _menuItems[e.Position];
                if (item == null)
                    return;
                if (item.Type == MenuItem.MenuType.Logout)
                {
                    var alert = new Android.App.AlertDialog.Builder(_context);
                    alert.SetMessage(Resource.String.AreYouSure);
                    alert.SetPositiveButton(Resource.String.Yes, delegate
                    {
                        //TODO : implement logout
                        _context.Finish();
                    });
                    alert.SetNegativeButton(Resource.String.Cancel, delegate { _drawerLayout.CloseDrawer(_drawerListView); });
                    alert.Show();
                }
                else if (item.Type == MenuItem.MenuType.SetupPage)
                {

                }
                else if (item.Type == MenuItem.MenuType.Login)
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                    LoginDialogFragment loginFragment = new LoginDialogFragment();
                    loginFragment.Show(_context.SupportFragmentManager, "login");
                }
                else if (item.Type == MenuItem.MenuType.Profile)
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                    ProfileFragment profileFragment = new ProfileFragment();
                    PushFragment(profileFragment);
                }
                else if (item.Type == MenuItem.MenuType.FinancialActivitues)
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                    FinancialActivitiesFragment financialActivitiesFragment = new FinancialActivitiesFragment();
                    PushFragment(financialActivitiesFragment);
                }
                else if (item.Type == MenuItem.MenuType.NonFinancialActivitues)
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                    NonFinancialActivitiesFragment nonFinancialActivitiesFragment = new NonFinancialActivitiesFragment();
                    PushFragment(nonFinancialActivitiesFragment);
                }
                else if (item.Type == MenuItem.MenuType.ClubActivities)
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                    ClubActivitiesFragment culbActivitiesFragment = new ClubActivitiesFragment();
                    PushFragment(culbActivitiesFragment);
                }
                else if (item.Type == MenuItem.MenuType.About)
                {
                    _drawerLayout.CloseDrawer(_drawerListView);
                }

            };
        }

    }
}