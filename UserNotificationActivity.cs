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
    [Activity(Label = "UserNotificationActivity", ParentActivity =typeof(MainActivity))]
    public class UserNotificationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserNotificationLayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            // Create your application here
            var buttonToast = FindViewById<Button>(Resource.Id.toast);
            buttonToast.Click += buttonToast_Click;

            var buttonAlert = FindViewById<Button>(Resource.Id.alert);
            buttonAlert.Click += buttonAlert_Click;

            var buttonAlertFragment = FindViewById<Button>(Resource.Id.alertFragment);
            buttonAlertFragment.Click += buttonAlertFragment_Click;

            var buttonSelectionInAlert = FindViewById<Button>(Resource.Id.selectionAlert);
            buttonSelectionInAlert.Click += buttonSelectionInAlert_Click;

            var buttonLongTaskNotification = FindViewById<Button>(Resource.Id.longtaskNotification);
            buttonLongTaskNotification.Click += buttonLongTaskNotification_Click;

            var buttonOngoingkNotification = FindViewById<Button>(Resource.Id.OngoingNotification);
            buttonOngoingkNotification.Click += buttonOngoingkNotification_Click;

            var buttonCustomViewNotification = FindViewById<Button>(Resource.Id.CustomViewNotification);
            buttonCustomViewNotification.Click += buttonCustomViewNotification_Click;

            var buttonPushNotification = FindViewById<Button>(Resource.Id.PushNotification);
            buttonPushNotification.Click += buttonPushNotification_Click;
        }

        private void buttonPushNotification_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PushNotificationActivity));
        }
        private void buttonCustomViewNotification_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CustomNotificationActivity));
        }
        private void buttonOngoingkNotification_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(OngoingNotificationActivity));
        }
        private void buttonLongTaskNotification_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LongTaskNotificationActivity));
        }
        private void buttonSelectionInAlert_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SelectionInAlertActivity));
        }
        private void buttonAlertFragment_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AlertFragmentActivity));
        }


        private void buttonAlert_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AlertDialogActivity));
        }

        private void buttonToast_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ToastActivity));
        }
    }
}