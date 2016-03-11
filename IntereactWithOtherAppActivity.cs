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
    [Activity(Label = "IntereactWithOtherAppActivity",ParentActivity =typeof(MainActivity))]
    public class IntereactWithOtherAppActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.IntereactWithOtherAppLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            var buttonStartAppComponent = FindViewById<Button>(Resource.Id.StartAppComponent);
            buttonStartAppComponent.Click += buttonStartAppComponent_Click;

            // Create your application here
            var buttonLaunchingOtherApp = FindViewById<Button>(Resource.Id.LaunchingOtherApp);
            buttonLaunchingOtherApp.Click += buttonLaunchingOtherApp_Click;

            var buttonobtainDataFromActivity = FindViewById<Button>(Resource.Id.obtainDataFromActivity);
            buttonobtainDataFromActivity.Click += buttonobtainDataFromActivity_Click;

            var buttonScheduleTask = FindViewById<Button>(Resource.Id.ScheduleTask);
            buttonScheduleTask.Click += buttonScheduleTask_Click;
        }

        private void buttonScheduleTask_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ScheduleTaskActivity));
        }
        private void buttonobtainDataFromActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ObtainDataFromActivityActivity));
        }

        private void buttonLaunchingOtherApp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LaunchingOtherAppActivity));
        }

        private void buttonStartAppComponent_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(StartAppComponentActivity));
        }
    }
}