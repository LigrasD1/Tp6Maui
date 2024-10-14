using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class AcercaDe : ContentPage
{
	public AcercaDe()
	{
		InitializeComponent();
		BindingContext = new AcercaViewModel();
	}
}