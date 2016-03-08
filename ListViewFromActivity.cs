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
    [Activity(Label = "ListViewFromActivity")]
    public class ListViewFromActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // "create" the data for a simple example
            IEnumerable<int> numbers = Enumerable.Range(1, 1000);
            IEnumerable<string> strings = numbers.Select(i => string.Format("Item Number {0} Here!", i));
            List<string> data = new List<string>(strings);

            // create the adapter
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, data);

            // assign the adapter to the list
            ListAdapter = adapter;

            // add a click/select event
            ListView.ItemClick += (sender, e) => {
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