using System;
using System.Collections.Generic;

using PushMobile.ViewModels;
using PushMobile.Views;

using Xamarin.Forms;

namespace PushMobile
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
		}

	}
}
