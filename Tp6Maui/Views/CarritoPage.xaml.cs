using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class CarritoPage : ContentPage
{
	public CarritoPage()
	{
		InitializeComponent();
		BindingContext = new CarritoViewModel();
	}
}