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
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace XamarinCookbook
{
    [Register("com.xamarincookbook.LongTaskNotificationActivity")]
    [Activity(Label = "LongTaskNotificationActivity",ParentActivity =typeof(UserNotificationActivity))]
    public class LongTaskNotificationActivity : Activity
    {
        private const int SimpleId = 32;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LongTaskNotificationLayout);

            // get the notification manager
            var manager = NotificationManager.FromContext(this);

            Button showSingle = FindViewById<Button>(Resource.Id.showSingle);
            showSingle.Click += delegate {
                // create a simple notification
                var notification = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Android.Resource.Drawable.StatNotifySync)
                    .SetContentTitle("Simple Notification")
                    .SetContentText("Hello World!")
                    .SetAutoCancel(true);

                // Creates an intent for an Activity in our app
                var intent = new Intent(this, typeof(NotificationActivity));
                // assign the intent to the notification for when the user taps it
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);
                notification.SetContentIntent(pendingIntent);

                // post the notification
                manager.Notify(SimpleId, notification.Build());
            };

            Button showbackStack = FindViewById<Button>(Resource.Id.showbackStack);
            showbackStack.Click += delegate {
                // create a simple notification
                var notification = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Android.Resource.Drawable.StatNotifySync)
                    .SetContentTitle("Simple Notification")
                    .SetContentText("Hello World with a custom back stack!")
                    .SetAutoCancel(true);

                // Creates an intent for an Activity in our app
                var intent = new Intent(this, typeof(NotificationActivity));
                // create a custom back stackfromthe metadata
                var backStack = TaskStackBuilder.Create(this)
                    .AddNextIntentWithParentStack(intent);
                // assign the intent to the notification for when the user taps it
                var pendingIntent = backStack.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
                notification.SetContentIntent(pendingIntent);

                // post the notification
                manager.Notify(SimpleId, notification.Build());
            };

            Button updateSingle = FindViewById<Button>(Resource.Id.updateSingle);
            updateSingle.Click += delegate {
                // create a simple notification
                var notification = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Android.Resource.Drawable.StatNotifySync)
                    .SetContentTitle("Simple Notification")
                    .SetContentText("Updated Now!")
                    .SetAutoCancel(true);

                // Creates an intent for an Activity in our app
                var intent = new Intent(this, typeof(NotificationActivity));
                // assign the intent to the notification for when the user taps it
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);
                notification.SetContentIntent(pendingIntent);

                // post the notification
                manager.Notify(SimpleId, notification.Build());
            };
        }
    }
}