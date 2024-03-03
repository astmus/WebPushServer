using System;

using AppNetDroid.Services;
using AppNetDroid.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNetDroid
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
