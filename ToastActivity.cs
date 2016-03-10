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
    [Activity(Label = "ToastActivity")]
    public class ToastActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ToastLayout);

            Button showToast = FindViewById<Button>(Resource.Id.showToast);
            Button showAnother = FindViewById<Button>(Resource.Id.showAnother);
            Button showCustom = FindViewById<Button>(Resource.Id.showCustom);

            showToast.Click += delegate {
                // show a normal toast
                using (var toast = Toast.MakeText(this, "This is a toast...", ToastLength.Short))
                {
                    toast.Show();
                }
            };

            showAnother.Click += delegate {
                // show a toast slightly differently
                using (var toast = Toast.MakeText(this, "This is another toast...", ToastLength.Short))
                {
                    toast.SetGravity(GravityFlags.Center, 0, 0);
                    toast.SetMargin(24, 24);
                    toast.Show();
                }
            };

            showCustom.Click += delegate {
                // show a custom view toast
                using (var toast = new Toast(ApplicationContext))
                using (var image = new ImageView(this))
                {
                    image.SetImageResource(Resource.Drawable.Icon);

                    toast.Duration = ToastLength.Short;
                    toast.View = image;
                    toast.Show();
                }
            };
        }
    }
}