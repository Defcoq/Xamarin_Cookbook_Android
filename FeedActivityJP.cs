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
using XamarinCodeSharing.Core;

namespace XamarinCookbook
{
    [Activity(Label = "Feed", Icon = "@drawable/icon",ParentActivity =typeof(MainActivity))]
    public class FeedActivity : ListActivity
    {
        private const string TitleKey = "title";
        private const string SubtitleKey = "subtitle";

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            var progress = new ProgressDialog(this);
            progress.SetTitle("Downloading Blog...");
            progress.SetMessage("Please wait while we download the Xamarin blog...");
            progress.Show();

            XamarinBlog blog = await XamarinBlog.Download();
            var items = from item in blog.Items
                        select new JavaDictionary<string, object>
                {
                    {TitleKey, item.Title},
                    {SubtitleKey, item.Description}
                };

            ListAdapter = new SimpleAdapter(
                this,
                items.Cast<IDictionary<string, object>>().ToList(),
                Android.Resource.Layout.SimpleListItem2,
                new[] { TitleKey, SubtitleKey },
                new[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });

            progress.Dismiss();
            progress.Dispose();
        }
    }
}