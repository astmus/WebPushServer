using System;
using System.Collections.Generic;
using System.ComponentModel;

using AppNetDroid.Models;
using AppNetDroid.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNetDroid.Views
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