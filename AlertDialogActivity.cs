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
    [Activity(Label = "AlertDialogActivity", ParentActivity =typeof(UserNotificationActivity))]
    public class AlertDialogActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            SetContentView(Resource.Layout.AlertDialogLayout);

            Button showAlert = FindViewById<Button>(Resource.Id.showAlert);
            Button showCustom = FindViewById<Button>(Resource.Id.showCustom);

            showAlert.Click += delegate {
                // show a normal alert dialog
                using (var dialog = new AlertDialog.Builder(this))
                {
                    dialog.SetTitle("Alert Title");
                    dialog.SetMessage("Alert message text here...");
                    dialog.SetPositiveButton("Close", delegate {
                        // do something cool here
                    });
                    dialog.Show();
                }
            };

            showCustom.Click += delegate {
                // show a normal alert dialog
                using (var dialog = new AlertDialog.Builder(this))
                using (var image = new ImageView(this))
                {
                    image.SetImageResource(Resource.Drawable.Icon);
                    dialog.SetTitle("Alert Title");
                    dialog.SetView(image);
                    dialog.SetMessage("Alert message text here...");
                    dialog.SetPositiveButton("Close", delegate {
                        // do something cool here
                    });
                    dialog.Show();
                }
            };
        }
    }
}