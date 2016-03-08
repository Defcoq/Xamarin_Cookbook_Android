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

namespace XamarinCookbook
{
    [Activity(Label = "ListViewActivity", ParentActivity =typeof(MainActivity))]
    public class ListViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListViewLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            var buttonEx1 = FindViewById<Button>(Resource.Id.listviewxml);
            buttonEx1.Click += ButtonEx1_Click;

            var buttonEx1FromActivity = FindViewById<Button>(Resource.Id.listviewFromActivity);
            buttonEx1FromActivity.Click += ButtonEx1FromActivity_Click; ;

            var buttonlistViewSimpleAdapter = FindViewById<Button>(Resource.Id.listViewSimpleAdapter);
            buttonlistViewSimpleAdapter.Click += ButtonlistViewSimpleAdapter_Click;

            var buttoncustomListViewItem = FindViewById<Button>(Resource.Id.customListViewItem);
            buttoncustomListViewItem.Click += ButtoncustomListViewItem_Click;

            var buttonlistViewBaseAdapter = FindViewById<Button>(Resource.Id.listViewBaseAdapter);
            buttonlistViewBaseAdapter.Click += buttonlistViewBaseAdapter_Click;

            var buttonlistViewCursorAdapter = FindViewById<Button>(Resource.Id.listViewCursorAdapter);
            buttonlistViewCursorAdapter.Click += buttonlistViewCursorAdapter_Click;

            var buttonlistViewSearch = FindViewById<Button>(Resource.Id.listViewSearch);
            buttonlistViewSearch.Click += buttonlistViewSearch_Click;

            var buttonlistViewRecyclerViewer = FindViewById<Button>(Resource.Id.listViewRecyclerViewer);
            buttonlistViewRecyclerViewer.Click += buttonbuttonlistViewRecyclerViewer_Click;
        }

        private void buttonbuttonlistViewRecyclerViewer_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RecycleViewerActivity));
        }

        private void buttonlistViewSearch_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListViewSearchActivity));
        }

        private void buttonlistViewCursorAdapter_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListviewCursorAdapterActivity));
        }
        private void buttonlistViewBaseAdapter_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListViewBaseAdapterActivity));
        }

        private void ButtoncustomListViewItem_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CustomListViewItemActivity));
        }

        private void ButtonlistViewSimpleAdapter_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListViewSimpleAdapterActivity));
        }

        private void ButtonEx1FromActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListViewFromActivity));
        }

        private void ButtonEx1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ListViewLayoutEx1Activity));
        }
    }
}