using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android;
using Android.Gms.Common;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using System.Threading.Tasks;
using System.Net;
using Task = System.Threading.Tasks.Task;

[assembly: UsesPermission("com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission("@PACKAGE_NAME@.permission.C2D_MESSAGE")]
namespace XamarinCookbook
{
    [Activity(Label = "PushNotificationActivity", ParentActivity =typeof(UserNotificationActivity))]
    public class PushNotificationActivity : Activity
    {
        private const string RegistrationIdKey = "RegistrationId";
        private const string RegistrationVersionKey = "RegistrationVersion";

        private const string ProjectNumber = "67557278999 ";
        private const string ApiKey = "AIzaSyDou4Zg9joKfQ5LHsiY24_l0lm6qV1P6V8";
        private const string MessagingServerUri = "https://android.googleapis.com/gcm/send";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PushNotificationLayout);
            TextView registrationTextView = FindViewById<TextView>(Resource.Id.registrationTextView);
            TextView statusTextView = FindViewById<TextView>(Resource.Id.statusTextView);
            Button sendMessage = FindViewById<Button>(Resource.Id.sendMessage);

            if (CheckPlayServices())
            {
                // play services found

                // make sure we have a registration
                statusTextView.Text = "Starting registration...";
                var registrationId = await GetRegistrationAsync();
                statusTextView.Text = "Registration complete.";
                registrationTextView.Text = registrationId;

                // set up for sending messages
                sendMessage.Click += delegate {
                    SendMessage(registrationId);
                };
            }
            else {
                // play services not found
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!CheckPlayServices())
            {
                // play services not found
            }
        }

        // Make sure that Google Play Services is installed and enabled
        private bool CheckPlayServices()
        {
            // check for Google PLay Sevices
            var availability = GoogleApiAvailability.Instance;
            var result = availability.IsGooglePlayServicesAvailable(this);

            // there was an error
            if (result != ConnectionResult.Success)
            {
                if (availability.IsUserResolvableError(result))
                {
                    // show a dialog that allows the user to download or enable Play Services
                    using (var dialog = availability.GetErrorDialog(this, result, 0))
                        dialog.Show();
                }
                else {
                    // Play Services is not supported, so end
                    Finish();
                }
                return false;
            }

            return true;
        }

        // Load the registration ID out of the saved preferences
        private Task<string> GetRegistrationAsync()
        {
            return Task.Run(() => {
                string registrationId = null;
                try
                {
                    var instanceId = InstanceID.GetInstance(this);
                    registrationId = instanceId.GetToken(ProjectNumber, GoogleCloudMessaging.InstanceIdScope);

                    // Send the registration ID to some sever now...
                    // As this app will also be ths server, we just need to keep a record of it
                }
                catch (Exception ex)
                {
                    // handle/log the error and let the user try again
                }
                return registrationId;
            });
        }

        // send a message to Google
        private async void SendMessage(string registrationId)
        {
            // create the message, we could use a JSON serializer here though
            var message = string.Format(
                "{{" +
                "  \"registration_ids\": [ \"{0}\" ]," +
                "  \"data\": {{" +
                "    \"cookbook_message\": \"Hello World! JP From Google play\"" +
                "  }}" +
                "}}", registrationId);
            var bytes = Encoding.UTF8.GetBytes(message);
            // create the request
            var request = WebRequest.CreateHttp(MessagingServerUri);
            request.Headers.Add(HttpRequestHeader.Authorization, "key=" + ApiKey);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;
            // write the data
            var stream = await request.GetRequestStreamAsync();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            // send the data
            var response = (HttpWebResponse)await request.GetResponseAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.StatusDescription);
        }
    }
}