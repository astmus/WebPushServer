
using Android.Gms.Extensions;

using Firebase.Iid;
using Firebase.Messaging;
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
			var app = MauiProgram.FireApp;
			//FirebaseInstanceId.GetInstance(app);
			var token = await FirebaseMessaging.Instance.GetToken();

			//var refreshedToken = FirebaseInstanceId.Instance.Token;
			//var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
			//var Token = token;
			//await DisplayAlert("FCM token", token, "OK");

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
		public string isSubscribed { get; set; } = "Не понятно";
		private async void SubBtn_Clicked(object sender, EventArgs e)
		{
			Button _this = sender as Button;
			if (isSubscribed != "да")
			{
				var tickDate = DateTime.Now.Date.Ticks.ToString();
				var task = FirebaseMessaging.Instance.SubscribeToTopic(tickDate);
				task.GetAwaiter().OnCompleted(() =>
				{
					var result = task.Result;
					isSubscribed = "да";
				});
			}
			else
			{
				var tickDate = DateTime.Now.Date.Ticks.ToString();
				var task = FirebaseMessaging.Instance.SubscribeToTopic(tickDate);
				task.GetAwaiter().OnCompleted(() =>
				{
					var result = task.Result;
					isSubscribed = "нет";
				});
			}
		}
	}

}
