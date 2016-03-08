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

namespace XamarinCookbook
{
    public class MenuFragment : ListFragment
    {
        // random data
        private static readonly string[] items = new string[] {
            "First",
            "Second",
            "Third",
            "Fourth"
        };

        // handle for the object that will handle menu selections
        private IMenuSelected menuSelected;

        // get hold of the activity that will handle menu selections (parent)
        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);

            menuSelected = activity as IMenuSelected;
        }

        // set up the fragment
        public override void OnStart()
        {
            base.OnStart();

            ListAdapter = new ArrayAdapter(
                Activity,
                Android.Resource.Layout.SimpleListItem1,
                items);

            // when an item is selected, notify the activity
            ListView.ItemClick += (sender, e) => {
                if (menuSelected != null)
                {
                    menuSelected.OnMenuSelected(items[e.Position]);
                }
            };
        }

        // this interface is the contract between an activity and this fragment
        public interface IMenuSelected
        {
            void OnMenuSelected(string text);
        }
    }
}