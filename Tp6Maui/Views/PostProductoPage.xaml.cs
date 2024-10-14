using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class PostProductoPage : ContentPage
{
	public PostProductoPage()
	{
		InitializeComponent();
		BindingContext = new PostProductoViewModel();
	}
}