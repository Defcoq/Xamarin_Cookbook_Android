using System;
using Android.App;
using Android.OS;

namespace XamarinCookbook
{
	[Activity]
	public class DestinationActivity : Activity
	{
		public const string ExtraKey = "MyMessage";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			if (Intent.HasExtra (ExtraKey)) {
				var message = Intent.GetStringExtra (ExtraKey);
				Console.WriteLine ("Intent extra data for activity: '{0}'.", message);
			} else {
				Console.WriteLine ("No extra data found for activity.");
			}
		}
	}
}