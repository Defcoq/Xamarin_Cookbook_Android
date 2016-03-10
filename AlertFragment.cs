using Android.App;
using Android.OS;
using DialogFragment = Android.Support.V4.App.DialogFragment;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace XamarinCookbook
{
	public class AlertFragment : DialogFragment
	{
		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			using (var dialog = new AlertDialog.Builder (Activity)) {
				dialog.SetTitle ("Alert Title");
				dialog.SetMessage ("Alert message text here...");
				dialog.SetPositiveButton ("Close", delegate { 
					// do something cool here
				});
				return dialog.Create ();
			}
		}
	}
}
