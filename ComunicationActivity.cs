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
    [Activity(Label = "ComunicationActivity",ParentActivity =typeof(MainActivity))]
    public class ComunicationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ComunicationLayout);

            Button buttonRestHttpClient = FindViewById<Button>(Resource.Id.RestHttpClient);
            buttonRestHttpClient.Click += ButtonRestHttpClient_Click;

            Button buttonCheckNetworkStatusActivity = FindViewById<Button>(Resource.Id.checkNetworkStatus);
            buttonCheckNetworkStatusActivity.Click += ButtonCheckNetworkStatusActivity_Click;
        }

        private void ButtonCheckNetworkStatusActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CheckNetworkStatusActivity));
        }

        private void ButtonRestHttpClient_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RestHttpClientActivity));
        }
    }
}