﻿using Microsoft.Maui.Controls;

namespace AndroidAppClient
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
	}
}
