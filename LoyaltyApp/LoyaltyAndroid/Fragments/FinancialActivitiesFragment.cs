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
using LoyaltyAppLibrary.Model;
using Anatoli.Framework.AnatoliBase;
using BarChart;

namespace LoyaltyAndroid.Fragments
{
    public class FinancialActivitiesFragment : CustomeFragment
    {
        ListView mItemsListView;
        public FinancialActivitiesFragment()
        {
            Title = "فعالیت های مالی من";
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.FinancialActivitiesLayout, container, false);
            mItemsListView = view.FindViewById<ListView>(Resource.Id.ItemsListView);
            FinancialActivityModel item1 = new FinancialActivityModel() { Date = DateTime.Now, Location = "aaa", Price = 2000.0, Activity = "dewfwefwf" };
            FinancialActivityModel item2 = new FinancialActivityModel() { Date = DateTime.Now, Location = "dwqed", Price = 20000.0, Activity = "dewfwewewefwf" };
            FinancialActivityModel item3 = new FinancialActivityModel() { Date = DateTime.Now, Location = "asdcaa", Price = 206500.4, Activity = "fewfwef" };
            FinancialActivityModel item4 = new FinancialActivityModel() { Date = DateTime.Now, Location = "attthtgrefaa", Price = 545.67, Activity = "jyujuj" };
            FinancialActivityModel item5 = new FinancialActivityModel() { Date = DateTime.Now, Location = "aafcrea", Price = 65.6, Activity = "fsgth" };
            FinancialActivityModel item6 = new FinancialActivityModel() { Date = DateTime.Now, Location = "acaa", Price = 65657.5, Activity = "hytjtjrhrt" };
            FinancialActivityModel item7 = new FinancialActivityModel() { Date = DateTime.Now, Location = "csdc", Price = 65.66, Activity = "egrergrehyjiujyyujtjt" };
            List<FinancialActivityModel> items = new List<FinancialActivityModel>();
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
            FinancialActivitiesAdapter adapter = new FinancialActivitiesAdapter(Activity, items);
            mItemsListView.Adapter = adapter;
            
            mItemsListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                if (adapter.Items[e.Position] != null)
                {
                    var item = adapter.Items[e.Position];
                    FinancialActivityDetailFragment detailDialog = new FinancialActivityDetailFragment(item);
                    detailDialog.Show(FragmentManager, "FinancialActivityDetailFragment");
                }
            };

            var data = new[] { 1f, 2f, 4f, 8f, 16f, 32f };
            var chart = new BarChartView(Activity)
            {
                ItemsSource = Array.ConvertAll(data, v => new BarModel { Value = v })
            };

            var relativeLayout1 = view.FindViewById<RelativeLayout>(Resource.Id.relativeLayout1);
            var layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            layoutParams.AddRule(LayoutRules.CenterInParent);
            relativeLayout1.AddView(chart, layoutParams);
            return view;
        }
    }

    public class FinancialActivitiesAdapter : BaseAdapter<FinancialActivityModel>
    {
        public List<FinancialActivityModel> Items;
        Activity _context;
        public FinancialActivitiesAdapter(Activity context, List<FinancialActivityModel> items)
        {
            Items = items;
            _context = context;
        }
        public override FinancialActivityModel this[int position]
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
            var view = _context.LayoutInflater.Inflate(Resource.Layout.FinancialItemsSummaryLayout, null);
            var itemDateTextView = view.FindViewById<TextView>(Resource.Id.ItemDateTextView);
            var itemActivityTextView = view.FindViewById<TextView>(Resource.Id.ItemActivityTextView);
            var itemPriceTextView = view.FindViewById<TextView>(Resource.Id.ItemPriceTextView);
            var itemLocationTextView = view.FindViewById<TextView>(Resource.Id.ItemLocationTextView);
            if (Items[position] != null)
            {
                var item = Items[position];
                itemDateTextView.Text = item.Date.ToShortDateString();
                itemActivityTextView.Text = item.Activity;
                itemPriceTextView.Text = item.Price.ToCurrency();
                itemLocationTextView.Text = item.Location;
            }
            return view;
        }
    }
}