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
    [Activity(Label = "ListViewLayoutEx1Activity", ParentActivity =typeof(ListViewActivity))]
    public class ListViewLayoutEx1Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            // Create your application here
            SetContentView(Resource.Layout.ListViewLayoutEx1);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            // "create" the data for a simple example
            IEnumerable<int> numbers = Enumerable.Range(1, 1000);
            IEnumerable<string> strings = numbers.Select(i => string.Format("Item Number {0} Here!", i));
            List<string> data = new List<string>(strings);

            // create the adapter
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, data);

            // assign the adapter to the list
            listView.Adapter = adapter;

            // add a click/select event
            listView.ItemClick += (sender, e) => {
                using (var dialog = new AlertDialog.Builder(this))
                {
                    int position = e.Position;
                    string value = data[position];
                    dialog.SetTitle("Item Selection");
                    dialog.SetMessage(value);
                    dialog.Show();
                }
            };
        }
    }
}