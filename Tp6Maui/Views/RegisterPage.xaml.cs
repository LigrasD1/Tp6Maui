using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		this.BindingContext = new RegistrarseViewModel();
	}
}