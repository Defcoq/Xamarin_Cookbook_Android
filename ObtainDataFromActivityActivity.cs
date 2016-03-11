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
    [Activity(Label = "ObtainDataFromActivityActivity",ParentActivity =typeof(IntereactWithOtherAppActivity))]
    public class ObtainDataFromActivityActivity : Activity
    {
        public const int ResultAction = 1234;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ObtainDataFromActivityLayout);

            Button launchResultActivity = FindViewById<Button>(Resource.Id.launchResultActivity);
            launchResultActivity.Click += delegate {
                // ask the user for a selection
                var intent = new Intent(this, typeof(ResultsActivity));
                StartActivityForResult(intent, ResultAction);
            };
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == ResultAction)
            {
                if (resultCode == Result.Ok)
                {
                   Console.WriteLine("User selected '{0}'.", data.GetStringExtra(ResultsActivity.ExtraKey));
                }
                else {
                    Console.WriteLine("User canceled operation.");
                }
            }
        }
    }
}