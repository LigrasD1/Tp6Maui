using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = new LoginViewModel();
	}
}