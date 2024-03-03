using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;


namespace MauiApp2
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				//.RegisterFirebaseServices()
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
		//	private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
		//	{
		//		builder.ConfigureLifecycleEvents(events =>
		//		{
		//			events.AddAndroid(android => android.OnCreate((activity, state) =>
		//				CrossFirebase.Initialize(activity, state, CreateCrossFirebaseSettings())));
		//		});

		//		builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
		//		return builder;
		//	}

		//	private static CrossFirebaseSettings CreateCrossFirebaseSettings()
		//	{
		//		return new CrossFirebaseSettings(isAuthEnabled: true, isCloudMessagingEnabled: true);
		//	}
		//}
	}
}
