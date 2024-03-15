
#if IOS
using Plugin.Firebase.Auth;
using Plugin.Firebase.Core.Platforms.iOS;
#elif ANDROID
//using Plugin.Firebase.Core.Platforms.Android;
using Firebase;
#endif

using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AndroidAppClient
{
	public static class MauiProgram
	{
#if ANDROID
		public static FirebaseApp FireApp => _fireApp;
		static FirebaseApp _fireApp;
#endif
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.RegisterFirebase()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}

		private static MauiAppBuilder RegisterFirebase(this MauiAppBuilder builder)
		{
			builder.ConfigureLifecycleEvents(events =>
			{
#if IOS
				events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) =>
				{
					CrossFirebase.Initialize();
					//Firebase.Core.App.Configure();
					Firebase.Crashlytics.Crashlytics.SharedInstance.Init();
					Firebase.Crashlytics.Crashlytics.SharedInstance.SetCrashlyticsCollectionEnabled(true);
					Firebase.Crashlytics.Crashlytics.SharedInstance.SendUnsentReports();
					return false;
				}));
#else
				events.AddAndroid(android => android.OnCreate((activity, bundle) =>
				{
					_fireApp = FirebaseApp.InitializeApp(activity);
				}));
#endif
			});

#if IOS
			CrossFirebaseAuth.Current.UseEmulator("0.0.0.0", 9099);
			builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
#endif
			return builder;
		}

		//private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
		//{
		//	builder.ConfigureLifecycleEvents(events =>
		//	{

		//		events.AddAndroid(android => android.OnCreate((activity, state) =>
		//			CrossFirebase.Initialize(activity, CreateCrossFirebaseSettings())));
		//	});

		//	builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
		//	return builder;
		//}

		//private static CrossFirebaseSettings CreateCrossFirebaseSettings()
		//{
		//	return new CrossFirebaseSettings(isAuthEnabled: true, isCloudMessagingEnabled: true);
		//}

	}
}
