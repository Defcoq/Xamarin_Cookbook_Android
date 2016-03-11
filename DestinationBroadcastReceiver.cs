using System;
using Android.Content;

namespace XamarinCookbook
{
	[BroadcastReceiver]
	public class DestinationBroadcastReceiver : BroadcastReceiver
	{
		public const string ExtraKey = "MyMessage";

		public override void OnReceive (Context context, Intent intent)
		{
			if (intent.HasExtra (ExtraKey)) {
				var message = intent.GetStringExtra (ExtraKey);
				Console.WriteLine ("Intent extra data for broadcast receiver: '{0}'.", message);
			} else {
				Console.WriteLine ("No extra data found for broadcast receiver.");
			}
		}
	}
}