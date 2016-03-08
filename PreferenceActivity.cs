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
    [Activity(Label = "PreferenceActivity", ParentActivity =typeof(MainActivity))]
    public class PreferenceActivity : Activity
    {
        private const string PreferencesName = "XamarinPreferences";
        private const string CountKey = "ClickCount";

        private int clickCount = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // load the layout
            SetContentView(Resource.Layout.PreferenceLayout);

            // get the last value from the saved preferences
            ISharedPreferences prefs = GetSharedPreferences(PreferencesName, FileCreationMode.Private);
            if (prefs.Contains(CountKey))
            {
                // load the value
                clickCount = prefs.GetInt(CountKey, 0);
            }

            // get the formattable text from the resources
            string buttonText = Resources.GetString(Resource.String.button_text_clicked);

            // get the button
            Button clickMe = FindViewById<Button>(Resource.Id.clickMe);
            // if there is a value, display it on the button
            if (clickCount > 0)
            {
                // update the button
                clickMe.Text = string.Format(buttonText, clickCount);
            }
            // attach the click event
            clickMe.Click += delegate {
                clickCount++;
                clickMe.Text = string.Format(buttonText, clickCount);
            };
        }

        protected override void OnStop()
        {
            // get the preferences
            ISharedPreferences prefs = GetSharedPreferences(PreferencesName, FileCreationMode.Private);
            // get the editor
            ISharedPreferencesEditor editor = prefs.Edit();
            // set the value
            editor.PutInt(CountKey, clickCount);
            // save the changes
            editor.Commit();

            base.OnStop();
        }
    }
}