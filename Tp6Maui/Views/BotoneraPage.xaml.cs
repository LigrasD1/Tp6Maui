using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class BotoneraPage : ContentPage
{
	public BotoneraPage()
	{
		InitializeComponent();
		BindingContext = new BotoneraViewModel();
	}
}