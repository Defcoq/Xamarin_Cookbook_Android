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
    [Activity(Label = "SelectionInAlertActivity",ParentActivity =typeof(UserNotificationActivity))]
    public class SelectionInAlertActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectionInAlertLayout);

            Button showSingle = FindViewById<Button>(Resource.Id.showSingle);
            Button showRadios = FindViewById<Button>(Resource.Id.showRadios);
            Button showChecks = FindViewById<Button>(Resource.Id.showChecks);
            Button showAdapter = FindViewById<Button>(Resource.Id.showAdapter);

            var items = new[]{
                "First",
                "Second",
                "Third",
                "Fourth",
                "Fifth"
            };

            var singleChecked = -1;
            var multiChecked = new bool[items.Length];

            showSingle.Click += delegate {
                using (var dialog = new AlertDialog.Builder(this))
                {
                    dialog.SetTitle("Alert Title");
                    dialog.SetPositiveButton("Close", delegate {
                    });
                    dialog.SetItems(items, (s, e) => {
                        var index = e.Which;
                    });
                    dialog.Show();
                }
            };

            showRadios.Click += delegate {
                using (var dialog = new AlertDialog.Builder(this))
                {
                    dialog.SetTitle("Alert Title");
                    dialog.SetPositiveButton("Close", delegate {
                    });
                    dialog.SetSingleChoiceItems(items, singleChecked, (s, e) => {
                        var index = e.Which;
                        singleChecked = index;
                    });
                    dialog.Show();
                }
            };

            showChecks.Click += delegate {
                using (var dialog = new AlertDialog.Builder(this))
                {
                    dialog.SetTitle("Alert Title");
                    dialog.SetPositiveButton("Close", delegate {
                    });
                    dialog.SetMultiChoiceItems(items, multiChecked, (s, e) => {
                        var index = e.Which;
                        var chekd = e.IsChecked;
                        multiChecked[index] = chekd;
                    });
                    dialog.Show();
                }
            };

            // this could be any adapter that we have created
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SelectDialogItem, items);
            showAdapter.Click += delegate {
                using (var dialog = new AlertDialog.Builder(this))
                {
                    dialog.SetTitle("Alert Title");
                    dialog.SetPositiveButton("Close", delegate {
                    });
                    dialog.SetAdapter(adapter, (s, e) => {
                        var index = e.Which;
                    });
                    dialog.Show();
                }
            };
        }
    }
}