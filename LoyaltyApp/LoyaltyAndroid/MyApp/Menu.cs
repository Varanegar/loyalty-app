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
    public class MenuItem
    {
        public string Title { get; set; }
        public ImageView Icon { get; set; }
        public MenuType Type { get; set; }
        public string Id { get; set; }
        public enum MenuType
        {
            Login,
            Logout,
            SetupPage,
            About,
            Profile,
            NonFinancialActivitues,
            FinancialActivitues,
            ClubActivities,
            Dashboard
        }
    }
    public class MenuAdapter : BaseAdapter<MenuItem>
    {
        List<MenuItem> _items;
        Activity _context;
        public MenuAdapter(Activity context, List<MenuItem> items)
        {
            if (items == null)
                _items = new List<MenuItem>();
            else
                _items = items;
            _context = context;
        }
        public override MenuItem this[int position]
        {
            get { return _items.Count > position ? _items[position] : null; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.MenuItemLayout, null);
            var titleTextView = view.FindViewById<TextView>(Resource.Id.TitleTextView);
            var iconImageView = view.FindViewById<ImageView>(Resource.Id.IconImageView);
            titleTextView.Text = _items[position].Title;
            return view;
        }
        
    }
}