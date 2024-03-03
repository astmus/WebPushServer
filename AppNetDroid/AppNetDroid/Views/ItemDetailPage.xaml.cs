using System.ComponentModel;

using AppNetDroid.ViewModels;

using Xamarin.Forms;

namespace AppNetDroid.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}