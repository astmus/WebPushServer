using Foundation;

using UIKit;
using Plugin.FirebasePushNotification;
using Firebase.CloudMessaging;
namespace iOSAppClient
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Initialize(launchOptions, true);

			return base.FinishedLaunching(application, launchOptions);
		}
		public static void Initialize(NSDictionary options, bool autoRegistration = true)
		{
			if (Firebase.Core.App.DefaultInstance == null)
			{
				Firebase.Core.App.Configure();
			}

			CrossFirebasePushNotification.Current.NotificationHandler = CrossFirebasePushNotification.Current.NotificationHandler ?? new DefaultPushNotificationHandler();
			Messaging.SharedInstance.AutoInitEnabled = autoRegistration;

			if (options?.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey) ?? false)
			{
				var pushPayload = options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
				if (pushPayload != null)
				{
					var parameters = GetParameters(pushPayload);

					var notificationResponse = new NotificationResponse(parameters, string.Empty, NotificationCategoryType.Default);


					/*if (_onNotificationOpened == null && enableDelayedResponse)
						delayedNotificationResponse = notificationResponse;
					else*/
					_onNotificationOpened?.Invoke(CrossFirebasePushNotification.Current, new FirebasePushNotificationResponseEventArgs(notificationResponse.Data, notificationResponse.Identifier, notificationResponse.Type));

					CrossFirebasePushNotification.Current.NotificationHandler?.OnOpened(notificationResponse);
				}
			}

			if (autoRegistration)
			{
				CrossFirebasePushNotification.Current.RegisterForPushNotifications();
			}

		}

		private static FirebasePushNotificationTokenEventHandler _onTokenRefresh;
		public event FirebasePushNotificationTokenEventHandler OnTokenRefresh
		{
			add
			{
				_onTokenRefresh += value;
			}
			remove
			{
				_onTokenRefresh -= value;
			}
		}

		private static FirebasePushNotificationDataEventHandler _onNotificationDeleted;
		public event FirebasePushNotificationDataEventHandler OnNotificationDeleted
		{
			add
			{
				_onNotificationDeleted += value;
			}
			remove
			{
				_onNotificationDeleted -= value;
			}
		}

		private static FirebasePushNotificationErrorEventHandler _onNotificationError;
		public event FirebasePushNotificationErrorEventHandler OnNotificationError
		{
			add
			{
				_onNotificationError += value;
			}
			remove
			{
				_onNotificationError -= value;
			}
		}

		private static FirebasePushNotificationResponseEventHandler _onNotificationOpened;
		public event FirebasePushNotificationResponseEventHandler OnNotificationOpened
		{
			add
			{
				_onNotificationOpened += value;
			}
			remove
			{
				_onNotificationOpened -= value;
			}
		}

		private static FirebasePushNotificationResponseEventHandler _onNotificationAction;
		public event FirebasePushNotificationResponseEventHandler OnNotificationAction
		{
			add
			{
				_onNotificationAction += value;
			}
			remove
			{
				_onNotificationAction -= value;
			}
		}

		private static FirebasePushNotificationDataEventHandler _onNotificationReceived;
		public event FirebasePushNotificationDataEventHandler OnNotificationReceived
		{
			add
			{
				_onNotificationReceived += value;
			}
			remove
			{
				_onNotificationReceived -= value;
			}
		}

		private static IDictionary<string, object> GetParameters(NSDictionary data)
		{
			var parameters = new Dictionary<string, object>();

			var keyAps = new NSString("aps");
			var keyAlert = new NSString("alert");

			foreach (var val in data)
			{
				if (val.Key.Equals(keyAps))
				{
					var aps = data.ValueForKey(keyAps) as NSDictionary;

					if (aps != null)
					{
						foreach (var apsVal in aps)
						{
							if (apsVal.Value is NSDictionary)
							{
								if (apsVal.Key.Equals(keyAlert))
								{
									foreach (var alertVal in apsVal.Value as NSDictionary)
									{
										parameters.Add($"aps.alert.{alertVal.Key}", $"{alertVal.Value}");
									}
								}
							}
							else
							{
								parameters.Add($"aps.{apsVal.Key}", $"{apsVal.Value}");
							}

						}
					}
				}
				else
				{
					parameters.Add($"{val.Key}", $"{val.Value}");
				}

			}


			return parameters;
		}
	}
}
