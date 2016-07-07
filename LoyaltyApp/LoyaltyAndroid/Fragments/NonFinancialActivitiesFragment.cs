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
using LoyaltyAndroid.MyApp;

namespace LoyaltyAndroid.Fragments
{
    public class NonFinancialActivitiesFragment : CustomeFragment
    {
        public NonFinancialActivitiesFragment()
        {
            Title = "فعالیت های غیر مالی من";
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.NonFinancialActivitiesLayout, container, false);
            var itemsListView = view.FindViewById<ListView>(Resource.Id.ItemsListView);
            NonFinancialActivityModel item1 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "aaa", Activity = "dewfwefwf" };
            NonFinancialActivityModel item2 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "dwqed", Activity = "dewfwewewefwf" };
            NonFinancialActivityModel item3 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "asdcaa", Activity = "fewfwef" };
            NonFinancialActivityModel item4 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "attthtgrefaa", Activity = "jyujuj" };
            NonFinancialActivityModel item5 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "aafcrea", Activity = "fsgth" };
            NonFinancialActivityModel item6 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "acaa", Activity = "hytjtjrhrt" };
            NonFinancialActivityModel item7 = new NonFinancialActivityModel() { Date = DateTime.Now, Location = "csdc", Activity = "egrergrehyjiujyyujtjt" };
            List<NonFinancialActivityModel> items = new List<NonFinancialActivityModel>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);
            items.Add(item7);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);
            items.Add(item7);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);
            items.Add(item6);
            items.Add(item7);
            NonFinancialActivitiesAdapter adapter = new NonFinancialActivitiesAdapter(Activity, items);
            itemsListView.Adapter = adapter;
            itemsListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                if (adapter.Items[e.Position] != null)
                {
                    NonFinancialActivityDetailFragment detailFragment = new NonFinancialActivityDetailFragment(adapter.Items[e.Position]);
                    detailFragment.Show(FragmentManager, "NonFinancialActivityDetailFragment");

                }
            };
            return view;
        }
    }
    public class NonFinancialActivitiesAdapter : BaseAdapter<NonFinancialActivityModel>
    {
        public List<NonFinancialActivityModel> Items;
        Activity _context;
        public NonFinancialActivitiesAdapter(Activity context, List<NonFinancialActivityModel> items)
        {
            Items = items;
            _context = context;
        }
        public override NonFinancialActivityModel this[int position]
        {
            get
            {
                return Items == null ? null : Items[position];
            }
        }

        public override int Count
        {
            get
            {
                return Items == null ? 0 : Items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = _context.LayoutInflater.Inflate(Resource.Layout.NonFinancialActivitySummaryLayout, null);
            var rowNumberTextView = view.FindViewById<TextView>(Resource.Id.RowNumberTextView);
            var dateTextView = view.FindViewById<TextView>(Resource.Id.DateTextView);
            var activityTextView = view.FindViewById<TextView>(Resource.Id.ActivityTextView);
            var locationTextView = view.FindViewById<TextView>(Resource.Id.LocationTextView);
            if (Items[position] != null)
            {
                var item = Items[position];
                rowNumberTextView.Text = position.ToString();
                dateTextView.Text = item.Date.ToShortDateString();
                activityTextView.Text = item.Activity;
                locationTextView.Text = item.Location;
            }
            return view;
        }
    }
}