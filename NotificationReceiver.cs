using Android.App;
using Android.Content;
using Android.Gms.Gcm;

namespace XamarinCookbook
{
	[BroadcastReceiver (
		Permission = "com.google.android.c2dm.permission.SEND", 
		Exported = true)]
	[IntentFilter (
		new [] {
			"com.google.android.c2dm.intent.RECEIVE", 
			"com.google.android.c2dm.intent.REGISTRATION"
		}, 
		Categories = new[]{ "@PACKAGE_NAME@" })]
	public class NotificationReceiver : GcmReceiver
	{
	}
}
