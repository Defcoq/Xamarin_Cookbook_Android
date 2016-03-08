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
using Android.Provider;
using Android.Database;

namespace XamarinCookbook
{
    [Activity(Label = "ListviewCursorAdapterActivity", ParentActivity =typeof(ListViewActivity))]
    public class ListviewCursorAdapterActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            // load the view from the layout
            SetContentView(Resource.Layout.ListviewCursorAdapterlayout);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            // access the content provider
            var uri = ContactsContract.Contacts.ContentUri;
            string[] projection = {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName,
                ContactsContract.Contacts.InterfaceConsts.ContactStatusLabel,
            };
            ICursor cursor = ContentResolver.Query(uri, projection, null, null, null);

            // create the adapter
            SimpleCursorAdapter adapter = new SimpleCursorAdapter(
                this, Android.Resource.Layout.SimpleListItem2, cursor,
                new[] { ContactsContract.Contacts.InterfaceConsts.DisplayName, ContactsContract.Contacts.InterfaceConsts.ContactStatusLabel },
                new[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });

            // assign the adapter to the list
            listView.Adapter = adapter;
        }
    }
}