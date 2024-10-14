using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class PutProductoPage : ContentPage
{
	public PutProductoPage()
	{
		InitializeComponent();
		BindingContext = new PutProductoViewModel();
	}
}