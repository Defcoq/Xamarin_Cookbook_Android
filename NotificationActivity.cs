using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;

namespace XamarinCookbook
{
	// specify the parent activity so we can build the back stack
	[MetaData ("android.support.PARENT_ACTIVITY", Value = "com.xamarincookbook.LongTaskNotificationActivity")]
	[Activity (Label = "XamarinCookbook Notification", Icon = "@drawable/icon", ParentActivity = typeof(LongTaskNotificationActivity))]
	public class NotificationActivity : Activity
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
