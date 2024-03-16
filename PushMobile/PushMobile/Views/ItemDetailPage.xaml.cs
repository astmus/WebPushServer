using System.ComponentModel;

using PushMobile.ViewModels;

using Xamarin.Forms;

namespace PushMobile.Views
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