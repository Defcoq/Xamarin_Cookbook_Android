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
using Android.Support.V4.App;

namespace XamarinCookbook
{
    [Activity(Label = "CustomNotificationActivity",ParentActivity =typeof(UserNotificationActivity))]
    public class CustomNotificationActivity : Activity
    {
        private const int SimpleId = 32;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CustomNotificationViewLayout);
            Button showCustom = FindViewById<Button>(Resource.Id.showCustom);
            Button updateCustom = FindViewById<Button>(Resource.Id.updateCustom);

            // get the notification manager
            var manager = NotificationManager.FromContext(this);

            // create the notification contents
            var view = new RemoteViews(PackageName, Resource.Layout.NotificationLayout);

            // Creates an intent for an Activity in our app
            var intent = new Intent(this, typeof(MainActivity));
            // assign the intent to the notification for when the user taps it
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);

            // create a simple notification
            var builder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Android.Resource.Drawable.StatNotifySync)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .SetContent(view);

            showCustom.Click += delegate {
                view.SetTextViewText(Resource.Id.title, "Simple Notification");
                view.SetTextViewText(Resource.Id.subtitle, "Hello World!");
                // post the notification
                var notification = builder.Build();
                if (Build.VERSION.SdkInt < BuildVersionCodes.Honeycomb)
                {
                    notification.ContentView = view;
                }
                manager.Notify(SimpleId, notification);
            };
            updateCustom.Click += delegate {
                view.SetTextViewText(Resource.Id.title, "Updated!");
                view.SetTextViewText(Resource.Id.subtitle, "Updated!");
                // update the notification
                var notification = builder.Build();
                if (Build.VERSION.SdkInt < BuildVersionCodes.Honeycomb)
                {
                    notification.ContentView = view;
                }
                manager.Notify(SimpleId, notification);
            };
        }
    }
}