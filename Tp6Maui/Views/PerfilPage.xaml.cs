using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class PerfilPage : ContentPage
{
	public PerfilPage()
	{
		InitializeComponent();
		BindingContext = new PerfilViewModel();
	}
}