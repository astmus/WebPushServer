using System;
using System.Collections.Generic;
using System.ComponentModel;

using PushMobile.Models;
using PushMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PushMobile.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}