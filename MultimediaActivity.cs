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
    [Activity(Label = "MultimediaActivity",ParentActivity =typeof(MainActivity))]
    public class MultimediaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.MultimediaLayout);

            // Create your application here
            var buttonUsingCamera = FindViewById<Button>(Resource.Id.UsingCamera);
            buttonUsingCamera.Click += buttonUsingCamera_Click;

            var buttonCreatingCameraApp = FindViewById<Button>(Resource.Id.CreatingCameraApp);
            buttonCreatingCameraApp.Click += buttonCreatingCameraApp_Click;
        }

        private void buttonUsingCamera_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(UsingCameraActivity));
        }
        private void buttonCreatingCameraApp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CreateCameraAppActivity));
        }
    }
}