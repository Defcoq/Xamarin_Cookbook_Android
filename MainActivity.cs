using Android.App;
using Android.Views;
using Android.OS;

using Android.Support.V7.App;
using System;
using Android.Widget;
using ActionMode = Android.Support.V7.View.ActionMode;

namespace XamarinCookbook
{
	[Activity (Label = "XamarinCookbook", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity, ActionMode.ICallback

    {
        private ActionMode actionMode; // a handle to the current mode

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

			RegisterForContextMenu (FindViewById (Resource.Id.label));
            SupportActionBar.Title = "JP Xamarin Cookbook";
            TextView lb = FindViewById<TextView>(Resource.Id.label);
            lb.LongClick += (sender, e) => {
                if (actionMode == null)
                {
                    // start the action mode
                    actionMode = StartSupportActionMode(this);
                    actionMode.Title = "Random";
                }
            };

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // inflate the Main_Options menu
            MenuInflater.Inflate(Resource.Menu.Main_Options, menu);
            return true;
        }

        public override void OnCreateContextMenu (IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);

			// inflate the Main_Options menu
			MenuInflater.Inflate(Resource.Menu.Main_Options, menu);

			menu.SetHeaderTitle ("Random Options...");
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{
			if (item.ItemId == Resource.Id.action_refresh)
			{
				// do something here...
				return true; // we handled the event
			}
			return base.OnOptionsItemSelected(item);
		}

        public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_refresh)
            {
                // do something here...
                return true;
            }
            return false;

        }

        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            mode.MenuInflater.Inflate(Resource.Menu.Main_Options, menu);
            return true;

        }

        public void OnDestroyActionMode(ActionMode mode)
        {
           
        }

        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            return false;
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_feed:
                   StartActivity(typeof(FeedActivity));
                    return true;

                case Resource.Id.action_fragment:
                    StartActivity(typeof(JPFragmentActivity));
                    return true;

                case Resource.Id.action_preference:
                    StartActivity(typeof(PreferenceActivity));
                    return true;
                case Resource.Id.action_manageFile:
                    StartActivity(typeof(ManageFileActivity));
                    return true;

                case Resource.Id.action_presenteData:
                    StartActivity(typeof(ListViewActivity));
                    return true;

                case Resource.Id.action_comunication:
                    StartActivity(typeof(ComunicationActivity));
                    return true;

                case Resource.Id.action_userNotification:
                    StartActivity(typeof(UserNotificationActivity));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
