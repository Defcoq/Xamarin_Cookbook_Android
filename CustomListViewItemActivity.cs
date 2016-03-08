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
    [Activity(Label = "CustomListViewItemActivity", ParentActivity =typeof(ListViewActivity))]
    public class CustomListViewItemActivity : Activity
    {
        private string[] firstNames = { "Shaun", "Linda", "Matthew", "Maxine", "Reuben", "Ruth", "Benjamin", "Sarah", "Stephen", "Hannah" };
        private string[] lastNames = { "Smith", "Parker", "Banner", "Stark", "Wayne", "Cain" };
        private string[] statuses = { "feeling happy", "saving the city", "overworked", "busy", "feeling like a bomb" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            // Create your application here
            // load the view from the layout
            SetContentView(Resource.Layout.CustomListViewItemLayout);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            // "create" the data for a simple example
            IList<IDictionary<string, object>> data = new JavaList<IDictionary<string, object>>();
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int first = rnd.Next(firstNames.Length);
                int last = rnd.Next(lastNames.Length);
                bool isMale = first % 2 == 0;
                string name = string.Join(" ", firstNames[first], lastNames[last]);
                string status = string.Format("{0} is {1}", name, statuses[rnd.Next(statuses.Length)]);
                int icon = isMale ? Resource.Drawable.male : Resource.Drawable.female;
                data.Add(new JavaDictionary<string, object> {
                    { "name", name },
                    { "status", status },
                    { "icon", icon }
                });
            }

            // create the adapters
            SimpleAdapter adapter = new SimpleAdapter(
                this, data, Resource.Layout.ListItemLayout,
                new[] { "name", "status", "icon" },
                new[] { Resource.Id.firstRow, Resource.Id.secondRow, Resource.Id.icon });

            // set the initial state
            listView.Adapter = adapter;
        }
    }
}