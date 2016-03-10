using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;

namespace XamarinCookbook
{
	[Activity (Label = "XamarinCookbook Notification", Icon = "@drawable/icon")]
	public class NotificationActivity2 : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			var content = new TextView (this);
			content.Text = "From Notification";
			SetContentView (content);
		}
	}
}
