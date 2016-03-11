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
using Java.Lang;

namespace XamarinCookbook
{
    [Activity(Label = "ScheduleTaskActivity", ParentActivity =typeof(IntereactWithOtherAppActivity))]
    public class ScheduleTaskActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            SetContentView(Resource.Layout.ScheduleTaskLayout);

            Button setAlarm = FindViewById<Button>(Resource.Id.setAlarm);

            setAlarm.Click += delegate {
                var ringTime =
                    JavaSystem.CurrentTimeMillis() +
                    (long)TimeSpan.FromSeconds(5).TotalMilliseconds;
                var intent = new Intent(AlarmReceiver.AlarmIntent);
                var alarm = PendingIntent.GetBroadcast(this, 0, intent, 0);
                var manager = AlarmManager.FromContext(this);
                manager.Set(AlarmType.RtcWakeup, ringTime, alarm);
            };
        }
    }
}