using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Hardware;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Media;

using Camera = Android.Hardware.Camera;
using Environment = Android.OS.Environment;
using Path = System.IO.Path;

// permission to use the camera
[assembly: UsesPermission(Manifest.Permission.Camera)]
[assembly: UsesPermission(Manifest.Permission.RecordAudio)]
[assembly: UsesPermission(Manifest.Permission.WriteExternalStorage)]

// let the package manager know we are using the features
[assembly: UsesFeature(Android.Content.PM.PackageManager.FeatureMicrophone, Required = false)]
[assembly: UsesFeature(Android.Content.PM.PackageManager.FeatureCamera, Required = false)]
[assembly: UsesFeature(Android.Content.PM.PackageManager.FeatureCameraFront, Required = false)]

namespace XamarinCookbook
{
    [Activity(Label = "CreateCameraAppActivity",ParentActivity =typeof(MultimediaActivity), ScreenOrientation = ScreenOrientation.Landscape)]
    public class CreateCameraAppActivity : Activity, ISurfaceHolderCallback, Camera.IPictureCallback, Camera.IShutterCallback
    
    {
        private FrameLayout container;
        private SurfaceView surfaceView;

        private ISurfaceHolder surfaceHolder;

        private Camera camera;
        private int currentCamera;
        private Camera.CameraInfo[] cameras;
        private Camera.Size previewSize;

        private MediaRecorder recorder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateCameraAppLayout);

            Spinner availableCameras = FindViewById<Spinner>(Resource.Id.availableCameras);
            Spinner captureMode = FindViewById<Spinner>(Resource.Id.captureMode);
            container = FindViewById<FrameLayout>(Resource.Id.container);
            surfaceView = FindViewById<SurfaceView>(Resource.Id.surfaceView);

            // set up the surface holder
            var holder = surfaceView.Holder;
            holder.AddCallback(this);
            holder.SetType(SurfaceType.PushBuffers);

            // get a list of all the cameras
            var cameraCount = Camera.NumberOfCameras;
            cameras = new Camera.CameraInfo[cameraCount];
            for (int index = 0; index < cameraCount; index++)
            {
                cameras[index] = new Camera.CameraInfo();
                Camera.GetCameraInfo(index, cameras[index]);
                if (cameras[index].Facing == CameraFacing.Back)
                {
                    currentCamera = index;
                }
            }

            // assign the cameras to a drop down
            var camerasAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, cameras.Select(c => c.Facing).ToArray());
            camerasAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            availableCameras.Adapter = camerasAdapter;
            availableCameras.ItemSelected += async (sender, e) => {
                if (currentCamera != e.Position)
                {
                    await SwitchCamera(e.Position);
                }
            };

            // assign the capture modes to a drop down
            var captureAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, new[] { "Photo", "Video" });
            captureAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            captureMode.Adapter = captureAdapter;

            // allow photos/videos to be taken
            // if we were recording, stop, otherwise start a new capture
            surfaceView.Click += delegate {
                if (recorder != null)
                {
                    StopRecording();
                }
                else if (captureMode.SelectedItemPosition == 0)
                {
                    TaskePhoto();
                }
                else if (captureMode.SelectedItemPosition == 1)
                {
                    StartRecording();
                }
            };
        }

        protected override void OnPause()
        {
            base.OnPause();

            // finish any video recording
            StopRecording();

            // free the camera for other apps
            ReleaseCamera();
        }

        protected override async void OnResume()
        {
            base.OnResume();

            // open the last camera
            await OpenCamera(currentCamera);
        }

        // preview surface

        public void SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
        {
            // we don't need to do anything
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            surfaceHolder = holder;

            if (camera != null)
            {
                // update the surface size
                UpdatePreviewSurfaceSize();
                // start the preview on the new surface
                camera.SetPreviewDisplay(surfaceHolder);
                camera.StartPreview();
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            surfaceHolder = null;

            if (camera != null)
            {
                // stop the preview as there is no surface
                camera.StopPreview();
            }
        }

        // manage the camera

        private async Task SwitchCamera(int position)
        {
            ReleaseCamera();
            await OpenCamera(position);
        }

        private void ReleaseCamera()
        {
            if (camera != null)
            {
                // stop the preview
                camera.StopPreview();
                // dispose of camera resources
                camera.Release();
                camera = null;
            }
        }

        private async Task OpenCamera(int cameraId)
        {
            // open the camera
            currentCamera = cameraId;

            await Task.Run(() => {
                try
                {
                    camera = Camera.Open(cameraId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            // get the supported preview sizes
            var parameters = camera.GetParameters();
            // use the biggest
            previewSize = parameters.SupportedPreviewSizes[0];
            // set the preview size
            parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
            camera.SetParameters(parameters);

            if (surfaceHolder != null)
            {
                // update the surface size
                UpdatePreviewSurfaceSize();

                // we want to preview on our surface
                camera.SetPreviewDisplay(surfaceHolder);

                // start the preview
                camera.StartPreview();
            }
        }

        private void UpdatePreviewSurfaceSize()
        {
            if (previewSize != null)
            {
                // find the largest preview size that fits
                var scale = Math.Min(
                    container.Width / (float)previewSize.Width,
                    container.Height / (float)previewSize.Height);
                var w = previewSize.Width * scale;
                var h = previewSize.Height * scale;

                // set the preview view size to that size
                surfaceHolder.SetFixedSize((int)w, (int)h);
            }
        }

        // taking photos

        private void TaskePhoto()
        {
            // a null shutter callback mutes the sound
            camera.TakePicture(this, null, this);
        }

        public void OnShutter()
        {
            // an empty callback plays the default sound
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            // save the file
            File.WriteAllBytes(GetPath("jpg"), data);

            // start the preview again
            camera.StartPreview();
        }

        // recording video

        private void StartRecording()
        {
            // let the camera be used by the recorder
            camera.Unlock();

            // start recording
            recorder = new MediaRecorder();
            recorder.SetCamera(camera);
            recorder.SetAudioSource(AudioSource.Camcorder);
            recorder.SetVideoSource(VideoSource.Camera);
            recorder.SetProfile(CamcorderProfile.Get(CamcorderQuality.High));
            recorder.SetOutputFile(GetPath("mp4"));
            recorder.SetPreviewDisplay(surfaceHolder.Surface);
            recorder.Prepare();
            recorder.Start();
        }

        private void StopRecording()
        {
            if (recorder != null)
            {
                recorder.Stop();
                recorder.Reset();
                recorder.Release();
                recorder = null;

                // we no longer need to share the camera
                camera.Reconnect();
                // start the preview again
                camera.StartPreview();
            }
        }

        // helper methods

        private string GetPath(string extension)
        {
            // the file name 
            var name = string.Format("XAM_{0}.{1}", DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss"), extension);

            // get the location of the files
            var path = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).AbsolutePath;
            path = Path.Combine(path, "XamarinCookbook");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, name);

            return path;
        }
    }
}