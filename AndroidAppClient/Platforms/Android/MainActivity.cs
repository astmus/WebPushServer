using Android.App;
using Android.Content.PM;
using Android.OS;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Widget;
using Android.Gms.Common;

namespace AndroidAppClient
{
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
	public class MainActivity : MauiAppCompatActivity
	{
		static readonly string TAG = "MainActivity";

		internal static readonly string CHANNEL_ID = "my_notification_channel";
		internal static readonly int NOTIFICATION_ID = 100;

		TextView msgText;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Log.Debug(TAG, "google app id: " + GetString(Resource.String.google_app_id));
			//SetContentView(Resource.Layout.Main);
			//msgText = FindViewById<TextView>(Resource.Id.msgText);
			if (Intent.Extras != null)
			{
				foreach (var key in Intent.Extras.KeySet())
				{
					var value = Intent.Extras.GetString(key);
					Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
				}
			}

			IsPlayServicesAvailable();

			CreateNotificationChannel();
		}
		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
					Log.Debug(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
				else
				{
					Log.Debug(TAG, "This device is not supported");
					Finish();
				}
				return false;
			}
			else
			{

				//	Log.Debug(TAG, "Google Play Services is available.");

				return true;
			}
		}
		void CreateNotificationChannel()
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				return;
			}

			//var channel = new NotificationChannel(CHANNEL_ID,
			//									  "FCM Notifications",
			//									  NotificationImportance.Default)
			//{

			//	Description = "Firebase Cloud Messages appear in this channel"
			//};

			//var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
			//notificationManager.CreateNotificationChannel(channel);
		}
	}

}
