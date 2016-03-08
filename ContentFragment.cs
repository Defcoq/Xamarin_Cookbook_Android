using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
namespace XamarinCookbook
{
    public class ContentFragment : Fragment
    {
        // text that has been passed during creation
        private string currentText = null;

        // key used to transfer data
        public const string ArgumentsKey = "ArgumentsKey";

        // this is needed to add the options menu
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // let the activity know that we are going to have an options menu
            HasOptionsMenu = true;
        }

        // update the the UI with the text
        public override void OnStart()
        {
            base.OnStart();

            // get the text from the fragment manager
            if (Arguments != null)
            {
                currentText = Arguments.GetString(ArgumentsKey);
            }
            // restore state that was captured during inflation
            if (currentText != null)
            {
                Update(currentText);
            }
        }

        // create the UI and restore any data
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // restore the saved state
            if (savedInstanceState != null)
            {
                currentText = savedInstanceState.GetString(ArgumentsKey);
            }

            return inflater.Inflate(Resource.Layout.ContentFragment, container, false);
        }

        // preserve the data, just in case we are re-instantiated
        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            // save the state
            outState.PutString(ArgumentsKey, currentText);
        }

        // update the UI with the text
        public void Update(string text)
        {
            // update state
            currentText = text;

            // update UI
            var textView = Activity.FindViewById<TextView>(Resource.Id.textView);
            textView.Text = string.Format("JP You have selected '{0}'...", text);
        }

        // inflate th options for this fragment
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.ContentFragment, menu);
        }
    }
}