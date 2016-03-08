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
    [Activity(Label = "ListViewBaseAdapterActivity",ParentActivity =typeof(ListViewActivity))]
    public class ListViewBaseAdapterActivity : Activity
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
            SetContentView(Resource.Layout.ListViewBaseAdapterLayout);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            // "create" the data for the example
            List<Person> data = new List<Person>();
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int first = rnd.Next(firstNames.Length);
                int last = rnd.Next(lastNames.Length);
                string name = string.Join(" ", firstNames[first], lastNames[last]);
                string status = string.Format("{0} is {1}", name, statuses[rnd.Next(statuses.Length)]);
                data.Add(new Person
                {
                    Id = i,
                    IsMale = first % 2 == 0,
                    Name = name,
                    Status = status
                });
            }

            // create and assign the adapter
            PeopleAdapter adapter = new PeopleAdapter(this, data);
            listView.Adapter = adapter;
        }
    }
}