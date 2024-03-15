using System;

using Microsoft.Maui.Controls;

#if ANDROID
using Android.Gms.Extensions;

using Firebase.Iid;
using Firebase.Messaging;
#else

using Plugin.Firebase.CloudMessaging;
#endif


namespace AndroidAppClient
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		public MainPage()
		{
			InitializeComponent();
		}

		private async void OnCounterClicked(object sender, EventArgs e)
		{
			count++;
#if ANDROID
			var app = MauiProgram.FireApp;
			var token = await FirebaseMessaging.Instance.GetToken();
#else
			var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
			var Token = token;
			await DisplayAlert("FCM token", token, "OK");
#endif
			//var refreshedToken = FirebaseInstanceId.Instance.Token;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";
#if ANDROID
			SemanticScreenReader.Announce(CounterBtn.Text);
#endif
		}
		public string isSubscribed { get; set; } = "Не понятно";
		private async void SubBtn_Clicked(object sender, EventArgs e)
		{
			Button _this = sender as Button;
			if (isSubscribed != "да")
			{
#if ANDROID
				var tickDate = DateTime.Now.Date.Ticks.ToString();
				var task = FirebaseMessaging.Instance.SubscribeToTopic(tickDate);
				task.GetAwaiter().OnCompleted(() =>
				{
					var result = task.Result;
					isSubscribed = "да";
				});
#else
#endif
			}
			else
			{
#if ANDROID
				var tickDate = DateTime.Now.Date.Ticks.ToString();
				var task = FirebaseMessaging.Instance.UnsubscribeFromTopic(tickDate);
				task.GetAwaiter().OnCompleted(() =>
				{
					var result = task.Result;
					isSubscribed = "нет";
				});
#else
#endif
			}
		}

		private async void SubBtnNews_Clicked(object sender, EventArgs e)
		{
#if ANDROID
			var task = FirebaseMessaging.Instance.SubscribeToTopic(inp.Text);
			task.GetAwaiter().OnCompleted(() =>
			{
				var result = task.Result;
				DisplayAlert("Подписка", "Мы подписались", "Ok");
				subsNameLabel.Text = inp.Text;
			});
#else
			await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
#endif
		}
	}

}
