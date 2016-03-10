using System;
using Android.App;
using Android.Content;
using Android.Gms.Gcm;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace XamarinCookbook
{
	[Service]
	[IntentFilter (new []{ "com.google.android.c2dm.intent.RECEIVE" })]
	public class NotificationService : GcmListenerService
	{
		private const int NotificationId = 13;

		public override void OnMessageReceived (string from, Bundle data)
		{
			var message = data.GetString ("cookbook_message");

			// show notification
			var manager = NotificationManager.FromContext (this);
			using (var builder = new NotificationCompat.Builder (this)) {
				builder.SetSmallIcon (Android.Resource.Drawable.StatNotifySync);
				builder.SetContentTitle ("Message From Server");
				builder.SetStyle (new NotificationCompat.BigTextStyle ().BigText (message));
				builder.SetContentText (message);
				builder.SetAutoCancel (true);
				manager.Notify (NotificationId, builder.Build ());
			}
		}
	}
}
