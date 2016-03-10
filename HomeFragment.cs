using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace XamarinCookbook
{
	public class HomeFragment : Fragment
	{
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.HomeLayout, container, false);

			Button showPopupAlert = view.FindViewById<Button> (Resource.Id.showPopupAlert);
			Button showEmbeddedAlert = view.FindViewById<Button> (Resource.Id.showEmbeddedAlert);

			showPopupAlert.Click += delegate {
				// show the dialog fragment
				AlertFragment2 frag = new AlertFragment2 ();
				frag.Show (FragmentManager, "AlertFragment");
			};

			showEmbeddedAlert.Click += delegate {
				// show the dialog as part of a fragment transaction
				AlertFragment2 frag = new AlertFragment2 ();
				FragmentManager
					.BeginTransaction ()
					.SetTransition (FragmentTransaction.TransitFragmentOpen)
					.Replace (Resource.Id.content, frag)
					.AddToBackStack (null)
					.Commit ();
			};

			return view;
		}
	}
}
	