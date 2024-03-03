namespace MauiApp2
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
			//await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
			//var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
			//var Token = token;
			//await DisplayAlert("FCM token", token, "OK");
			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
