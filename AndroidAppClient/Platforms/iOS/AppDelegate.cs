using Foundation;

using Microsoft.Maui;
using Microsoft.Maui.Hosting;

using UIKit;

namespace AndroidAppClient
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Firebase.Core.App.Configure();
			return base.FinishedLaunching(application, launchOptions);
		}
	}
}
