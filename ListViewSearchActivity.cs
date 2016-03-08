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
using SearchView = Android.Support.V7.Widget.SearchView;

namespace XamarinCookbook
{
    [MetaData("android.app.default_searchable", Value = "com.xamarincookbook.SearchActivity")]
    [Activity(Label = "ListViewSearchActivity", ParentActivity =typeof(ListViewActivity), Theme = "@style/Theme.AppCompat")]
    public class ListViewSearchActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // ActionBar.SetDisplayHomeAsUpEnabled(true);
           
            SetContentView(Resource.Layout.ListViewSearchLayout);

            var searchDialog = FindViewById<Button>(Resource.Id.searchDialog);
            searchDialog.Click += delegate
            {
                // show the search dialog/overlay
                OnSearchRequested();
            };
         }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // inflate the action bar
            MenuInflater.Inflate(Resource.Menu.ActionBar, menu);

            // get the search box
            var searchItem = menu.FindItem(Resource.Id.searchItem);
            var searchView = searchItem.ActionView.JavaCast<SearchView>();

            // get the search settings
            var manager = SearchManager.FromContext(this);
            var info = manager.GetSearchableInfo(ComponentName);

            // give the search settings to the search box
            searchView.SetSearchableInfo(info);

            return true;
        }
    }
}