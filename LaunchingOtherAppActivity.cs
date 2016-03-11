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
using Uri = Android.Net.Uri;

namespace XamarinCookbook
{
    [IntentFilter(new[] { Intent.ActionSend }, DataMimeType = "text/plain", Categories = new[] { Intent.CategoryDefault })]
    [IntentFilter(new[] { Intent.ActionView }, DataScheme = "geo", Categories = new[] { Intent.CategoryDefault })]
    [IntentFilter(new[] { Intent.ActionView }, DataScheme = "geo", DataMimeType = "text/plain", Categories = new[] { Intent.CategoryDefault })]
    [Activity(Label = "LaunchingOtherAppActivity", ParentActivity =typeof(IntereactWithOtherAppActivity))]
    public class LaunchingOtherAppActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LaunchingOtherAppLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            Button intentWithData = FindViewById<Button>(Resource.Id.intentWithData);
            Button intentWithType = FindViewById<Button>(Resource.Id.intentWithType);
            Button intentWithDataAndType = FindViewById<Button>(Resource.Id.intentWithDataAndType);
            Button checkIntent = FindViewById<Button>(Resource.Id.checkIntent);
            Button startChooser = FindViewById<Button>(Resource.Id.startChooser);

            Console.WriteLine("Data received: '{0}'.", Intent.DataString);
            Console.WriteLine("Text received: '{0}'.", Intent.GetStringExtra(Intent.ExtraText));

            intentWithData.Click += delegate {
                var uri = Uri.Parse("geo:37.797786,-122.401855");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };

            intentWithType.Click += delegate {
                var intent = new Intent(Intent.ActionSend);
                intent.SetType("text/plain");
                intent.PutExtra(Intent.ExtraEmail, new[] { "username@example.com" });
                intent.PutExtra(Intent.ExtraSubject, "Email Subject");
                intent.PutExtra(Intent.ExtraText, "Email message text here...");
                StartActivity(intent);
            };

            intentWithDataAndType.Click += delegate {
                var uri = Uri.Parse("geo:37.797786,-122.401855");
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, "text/plain");
                intent.PutExtra(Intent.ExtraText, "Email message text here...");
                StartActivity(intent);
                // this will probably launch this app as there 
                // will be no other app that can handle this intent
            };

            checkIntent.Click += delegate {
                var uri = Uri.Parse("geo:37.797786,-122.401855");
                var intent = new Intent(Intent.ActionView, uri);
                var activities = PackageManager.QueryIntentActivities(intent, 0);
                Console.WriteLine("Found {0} activities:", activities.Count);
                foreach (var activity in activities)
                {
                    var name = activity.ActivityInfo.Name;
                    Console.WriteLine("  '{0}'", name);
                }
            };

            startChooser.Click += delegate {
                var uri = Uri.Parse("geo:37.797786,-122.401855");
                var intent = new Intent(Intent.ActionView, uri);
                var chooser = Intent.CreateChooser(intent, "View location with");
                StartActivity(chooser);
            };
        }
    }
}