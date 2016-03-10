using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace XamarinCookbook
{
	public class AlertFragment2 : AppCompatDialogFragment
	{
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// we can't use the alert builder, but we can use a layout
			var view = inflater.Inflate (Resource.Layout.DialogLayout, container, false);

			// we don't want the button when we are embedded, but we do when in the popup
			var dialogButton = view.FindViewById<Button> (Resource.Id.dialogButton);
			if (ShowsDialog) {
				dialogButton.Visibility = ViewStates.Visible;
				dialogButton.Click += delegate {
					Dismiss ();
				};
			} else {
				dialogButton.Visibility = ViewStates.Gone;
			}

			return view;
		}

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			// add the dialog bits, for when shown as a dialog
			var dialog = base.OnCreateDialog (savedInstanceState);
			dialog.SetTitle ("Cool Dialog");

			return dialog;
		}
	}
}
