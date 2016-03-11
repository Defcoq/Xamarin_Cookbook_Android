using System;
using Android.App;
using Android.Content;

namespace XamarinCookbook
{
	[Service]
	public class DestinationService : IntentService
	{
		public const string ExtraKey = "MyMessage";

		protected override void OnHandleIntent (Intent intent)
		{
			if (intent.HasExtra (ExtraKey)) {
				var message = intent.GetStringExtra (ExtraKey);
				Console.WriteLine ("Intent extra data for service: '{0}'.", message);
			} else {
				Console.WriteLine ("No extra data found for service.");
			}
		}
	}
}