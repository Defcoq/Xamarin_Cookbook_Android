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
using System.Threading;
using System.Threading.Tasks;

namespace XamarinCookbook
{
    [Activity(Label = "OngoingNotificationActivity", ParentActivity =typeof(UserNotificationActivity))]
    public class OngoingNotificationActivity : Activity
    {
        private const int ProgressId = 12;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OngoingNotificationLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // get the notification manager
            var manager = NotificationManager.FromContext(this);

            // for the task cancelation token
            CancellationTokenSource cts = null;

            Button showProgress = FindViewById<Button>(Resource.Id.showProgress);
            showProgress.Click += async delegate {
                cts = new CancellationTokenSource();

                // create a simple notification
                var notification = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Android.Resource.Drawable.StatNotifySync)
                    .SetContentTitle("Simple Notification")
                    .SetContentText("Starting Work!")
                    .SetProgress(0, 0, true)
                    .SetOngoing(true);

                // Creates an intent for an Activity in our app
                var intent = new Intent(this, typeof(NotificationActivity2));
                // assign the intent to the notification for when the user taps it
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);
                notification.SetContentIntent(pendingIntent);

                // post the notification
                manager.Notify(ProgressId, notification.Build());

                try
                {
                    // do work on another thread
                    await Task.Run(delegate {
                        cts.Token.ThrowIfCancellationRequested();

                        const int max = 100;
                        for (int i = 0; i < max; i++)
                        {
                            cts.Token.ThrowIfCancellationRequested();

                            Thread.Sleep(100);

                            cts.Token.ThrowIfCancellationRequested();

                            // update the notification with progress
                            notification.SetProgress(max, i, false);
                            manager.Notify(ProgressId, notification.Build());
                        }

                        // remove the notification
                        manager.Cancel(ProgressId);
                    }, cts.Token);
                }
                catch (System.OperationCanceledException)
                {
                    // when we cancel, remove the notification
                    manager.Cancel(ProgressId);
                }
            };

            Button cancelProgress = FindViewById<Button>(Resource.Id.cancelProgress);
            cancelProgress.Click += delegate {
                // cancel the work, and this will cancel the notification
                if (cts != null)
                {
                    cts.Cancel();
                }
            };
        }
    }
}