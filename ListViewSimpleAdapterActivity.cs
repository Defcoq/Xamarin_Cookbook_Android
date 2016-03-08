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
    [Activity(Label = "ListViewSimpleAdapterActivity", ParentActivity =typeof(ListViewActivity))]
    public class ListViewSimpleAdapterActivity : Activity
    {
        private string[] firstNames = { "Shaun", "Linda", "Matthew", "Maxine", "Reuben", "Ruth", "Benjamin", "Sarah", "Stephen", "Hannah" };
        private string[] lastNames = { "Smith", "Parker", "Banner", "Stark", "Wayne", "Cain" };
        private string[] statuses = { "feeling happy", "saving the city", "overworked", "busy", "feeling like a bomb" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            // Create your application here

            SetContentView(Resource.Layout.ListViewSimpleAdapterLayout);

            Button showName = FindViewById<Button>(Resource.Id.showName);
            Button showStatus = FindViewById<Button>(Resource.Id.showStatus);
            Button showChecks = FindViewById<Button>(Resource.Id.showChecks);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            // "create" the data for a simple example
            IList<IDictionary<string, object>> data = new JavaList<IDictionary<string, object>>();
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int first = rnd.Next(firstNames.Length);
                int last = rnd.Next(lastNames.Length);
                string name = string.Join(" ", firstNames[first], lastNames[last]);
                string status = string.Format("{0} is {1}", name, statuses[rnd.Next(statuses.Length)]);
                data.Add(new JavaDictionary<string, object> {
                    { "name", name },
                    { "status", status }
                });
            }

            // create the adapters
            SimpleAdapter singleLineAdapter = new SimpleAdapter(
                this, data, Android.Resource.Layout.SimpleListItem1,
                new[] { "name" }, new[] { Android.Resource.Id.Text1 });
            SimpleAdapter doubleLineAdapter = new SimpleAdapter(
                this, data, Android.Resource.Layout.SimpleListItem2,
                new[] { "name", "status" }, new[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });
            SimpleAdapter checkedSingleLineAdapter = new SimpleAdapter(
                this, data, Android.Resource.Layout.SimpleListItemChecked,
                new[] { "name" }, new[] { Android.Resource.Id.Text1 });

            // set the initial state
            listView.Adapter = singleLineAdapter;

            // add a means to toggle the item view
            showName.Click += delegate {
                listView.ChoiceMode = ChoiceMode.None;
                listView.Adapter = singleLineAdapter;
            };
            showStatus.Click += delegate {
                listView.ChoiceMode = ChoiceMode.None;
                listView.Adapter = doubleLineAdapter;
            };
            showChecks.Click += delegate {
                listView.ChoiceMode = ChoiceMode.Single;
                listView.Adapter = checkedSingleLineAdapter;
            };
        }
    }
}