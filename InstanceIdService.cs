using System;
using Android.App;
using Android.Content;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace XamarinCookbook
{
	[Service]
	[IntentFilter (new []{ "com.google.android.gms.iid.InstanceID" })]
	public class InstanceIdService : InstanceIDListenerService
	{
		public override void OnTokenRefresh ()
		{
            var instanceId = InstanceID.GetInstance(this);
            var token = instanceId.GetToken(
            "67557278999",
            GoogleCloudMessaging.InstanceIdScope);

        }
    }
}
