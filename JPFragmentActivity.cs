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
using Fragment = Android.Support.V4.App.Fragment;

namespace XamarinCookbook
{
    [Activity(Label = "JPFragmentActivity", Theme = "@style/Theme.AppCompat")]
    public class JPFragmentActivity : AppCompatActivity, MenuFragment.IMenuSelected
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // the the main layout
            SetContentView(Resource.Layout.JPMainFragment);

            // create the fragments manually if we are protrait
            FrameLayout fragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
            if (fragmentContainer != null)
            {
                // if there is a bundle, we are restoring and we don't 
                // have to do anything, Android takes care of it for us
                Fragment menu = SupportFragmentManager.FindFragmentById(Resource.Id.menu);
                if (bundle == null || menu == null)
                {
                    FragmentManager
                        .BeginTransaction()
                        .Add(Resource.Id.fragmentContainer, new MenuFragment())
                        .Commit();
                }
            }

            // display the up arrow when the fragment manager has a back stack
            SupportFragmentManager.BackStackChanged += delegate {
                OnBackStackChanged();
            };
            // update the up arrow now
            OnBackStackChanged();
        }

        // handle the MenuFragment's selections
        public void OnMenuSelected(string text)
        {
            // update the content based on the selection
           // ContentFragment content = FragmentManager.FindFragmentById(Resource.Id.content) as ContentFragment;
            //content.Update(text);

            // update the content based on the selection
            FrameLayout fragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
            if (fragmentContainer != null)
            {
                // the content fragment needs to be created
                ContentFragment content = new ContentFragment();
                Bundle args = new Bundle();
                args.PutString(ContentFragment.ArgumentsKey, text);
                content.Arguments = args;
                // add it to the activity
                SupportFragmentManager
                    .BeginTransaction()
                    .Replace(Resource.Id.fragmentContainer, content)
                    .AddToBackStack(null)
                    .Commit();
            }
            else {
                // we are in landscape, so we are always visible
                ContentFragment content = SupportFragmentManager.FindFragmentById(Resource.Id.content) as ContentFragment;
                content.Update(text);
            }

        }

        // inflate th options for this fragment
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main_Options, menu);
            return true;
        }

        // update the action bar to use the up arrow
        private void OnBackStackChanged()
        {
            bool hasBack = SupportFragmentManager.BackStackEntryCount > 0;
            SupportActionBar.SetDisplayHomeAsUpEnabled(hasBack);
        }

        // go backwards when we press the up arrow
        public override bool OnSupportNavigateUp()
        {
            SupportFragmentManager.PopBackStack();
            return true;
        }
    }
}