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
    [Activity(Label = "StartAppComponentActivity",ParentActivity =typeof(IntereactWithOtherAppActivity))]
    public class StartAppComponentActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartingAppComponentLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            Button startActivity = FindViewById<Button>(Resource.Id.startActivity);
            Button startService = FindViewById<Button>(Resource.Id.startService);
            Button startBroadcast = FindViewById<Button>(Resource.Id.startBroadcast);

            startActivity.Click += delegate {
                var intent = new Intent(this, typeof(DestinationActivity));
                intent.PutExtra(DestinationActivity.ExtraKey, "Hello World!");
                StartActivity(intent);
            };

            startService.Click += delegate {
                var intent = new Intent(this, typeof(DestinationService));
                intent.PutExtra(DestinationService.ExtraKey, "Hello World!");
                StartService(intent);
            };

            startBroadcast.Click += delegate {
                var intent = new Intent(this, typeof(DestinationBroadcastReceiver));
                intent.PutExtra(DestinationBroadcastReceiver.ExtraKey, "Hello World!");
                SendBroadcast(intent);
            };
        }
    }
}