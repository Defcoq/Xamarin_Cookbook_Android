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
using Android.Net;
using System.Net.Http;

namespace XamarinCookbook
{
    [Activity(Label = "CheckNetworkStatusActivity", ParentActivity =typeof(ComunicationActivity))]
    public class CheckNetworkStatusActivity : Activity
    {

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            SetContentView(Resource.Layout.CheckNetworkStatusLayout);
            TextView textView = FindViewById<TextView>(Resource.Id.textView);

            // get the connectivity manager
            textView.Text += "Getting Connectivity Manager...\n";
            var manager = ConnectivityManager.FromContext(this);

            // check for any connectivity
            textView.Text += "Getting Current Connection...\n";
            var networkInfo = manager.ActiveNetworkInfo;
            if (networkInfo != null)
            {
                textView.Text += "Active Connection:\n";
                textView.Text += string.Format("  Is Connected: {0}\n", networkInfo.IsConnected);
                textView.Text += string.Format("  Connection Type: {0}\n", networkInfo.Type);
                textView.Text += string.Format("  Connection Sub-Type: {0}\n", networkInfo.Subtype);
            }
            else
            {
                textView.Text += "No Active Connection!\n";
            }

            // are we on wifi
            var wifiInfo = manager.GetNetworkInfo(ConnectivityType.Wifi);
            var isWiFi = wifiInfo != null && wifiInfo.IsConnected;
            textView.Text += string.Format("\n{0} WiFi Connection!\n", isWiFi ? "Active" : "No");

            // are we on wifi
            var mobileInfo = manager.GetNetworkInfo(ConnectivityType.Mobile);
            var isMobile = mobileInfo != null && mobileInfo.IsConnected;
            textView.Text += string.Format("\n{0} Mobile Connection!\n", isMobile ? "Active" : "No");

            // start operation on a new thread using async/await
            if (networkInfo != null && networkInfo.IsConnected)
            {
                textView.Text += "\nWe are connected, so we can download...\n";
                using (var client = new HttpClient())
                {
                    // send the GET request
                    var uri = "http://jsonplaceholder.typicode.com/posts/1";
                    var result = await client.GetStringAsync(uri);
                }
                textView.Text += "Download complete.\n";
            }
            else
            {
                textView.Text += "\nWe are not connected, so we can't download.\n";
            }
        }
    }
}