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
using Android.Support.V7.App;

namespace XamarinCookbook
{
    [Activity(Label = "AlertFragmentActivity",ParentActivity =typeof(UserNotificationActivity), Theme = "@style/Theme.AppCompat")]
    public class AlertFragmentActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AlertFragmentLayout);

            Button showAlert = FindViewById<Button>(Resource.Id.showAlert);

            // get the first fragment in
            FrameLayout content = FindViewById<FrameLayout>(Resource.Id.content);
            SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.content, new HomeFragment())
                .Commit();

            showAlert.Click += delegate {
                // show the dialog fragment
                AlertFragment frag = new AlertFragment();
                frag.Show(SupportFragmentManager, "AlertFragment");
            };
        }
    }
}