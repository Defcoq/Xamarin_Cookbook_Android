using Android.App;
using Android.Content;
using Android.Widget;

namespace XamarinCookbook
{
	[BroadcastReceiver]
	[IntentFilter(new[] { AlarmReceiver.AlarmIntent })]
	public class AlarmReceiver : BroadcastReceiver
	{
		public const string AlarmIntent = "AlarmTriggered";

		public override void OnReceive (Context context, Intent intent)
		{
			Toast.MakeText (context, "Alarm triggered!", ToastLength.Short).Show ();
		}
	}
}
