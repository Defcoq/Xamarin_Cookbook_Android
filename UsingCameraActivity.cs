
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Content.PM;
using Java.IO;

[assembly: UsesFeature(Android.Content.PM.PackageManager.FeatureMicrophone, Required = false)]
[assembly: UsesFeature(Android.Content.PM.PackageManager.FeatureCamera, Required = false)]
[assembly: UsesFeature(Android.Content.PM.PackageManager.FeatureCameraFront, Required = false)]
namespace XamarinCookbook
{

    [Activity(Label = "UsingCameraActivity", ParentActivity =typeof(MultimediaActivity))]
    public class UsingCameraActivity : Activity
    {

        private const int TakePhotoCode = 123;
        private const int TakeVideoCode = 456;

        private const int VideoQualityHigh = 1;
        private const int VideoQualityLow = 0;

        private ImageView imageView;
        private VideoView videoView;

        private Java.IO.File imageFile;
        private Java.IO.File videoFile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            SetContentView(Resource.Layout.UsingCameraLayout);

            Button takePhoto = FindViewById<Button>(Resource.Id.takePhoto);
            Button takeVideo = FindViewById<Button>(Resource.Id.takeVideo);

            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            videoView = FindViewById<VideoView>(Resource.Id.videoView);

            // check to see if we have any cameras
            if (!PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureCamera) &&
                   !PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureCameraFront))
            {
                // we should stop here, but we won't in this example
            }

            // check to see if there is a camera app:
            var activities = PackageManager.QueryIntentActivities(
                                 new Intent(MediaStore.ActionImageCapture),
                                 PackageInfoFlags.MatchDefaultOnly);
            if (activities.Count == 0)
            {
                // handle no camera apps
            }

            // get the location of the files
            var root = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
            root = new Java.IO.File(root, "XamarinCookbook");
            if (!root.Exists())
                root.Mkdirs();
            imageFile = new Java.IO.File(root, "image.jpg");
            videoFile = new Java.IO.File(root, "video.mp4");
            var imageUri = Android.Net.Uri.FromFile(imageFile);
            var videoUri = Android.Net.Uri.FromFile(videoFile);

            // take a photo using the camera app
            takePhoto.Click += delegate {
                var intent = new Intent(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, imageUri);
                StartActivityForResult(intent, TakePhotoCode);
            };
            // record a video using the camera app
            takeVideo.Click += delegate {
                var intent = new Intent(MediaStore.ActionVideoCapture);
                intent.PutExtra(MediaStore.ExtraOutput, videoUri);
                StartActivityForResult(intent, TakeVideoCode);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == TakePhotoCode)
            {
                imageView.Visibility = ViewStates.Visible;
                videoView.Visibility = ViewStates.Gone;
                if (resultCode == Result.Ok)
                {
                    // display the picture
                    imageView.SetImageURI(Android.Net.Uri.Parse(imageFile.AbsolutePath));
                }
                else {
                    Toast.MakeText(this, "Canceled photo.", ToastLength.Short).Show();
                }
            }
            else if (requestCode == TakeVideoCode)
            {
                imageView.Visibility = ViewStates.Gone;
                videoView.Visibility = ViewStates.Visible;
                if (resultCode == Result.Ok)
                {
                    // play the video
                    videoView.SetVideoPath(videoFile.AbsolutePath);
                    videoView.SetMediaController(new MediaController(this));
                    videoView.RequestFocus();
                    videoView.Start();
                }
                else {
                    Toast.MakeText(this, "Canceled video.", ToastLength.Short).Show();
                }
            }
            else {
                base.OnActivityResult(requestCode, resultCode, data);
            }
        }
    }
}