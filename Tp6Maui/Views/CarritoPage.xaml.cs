using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class CarritoPage : ContentPage
{
	public CarritoPage()
	{
		InitializeComponent();
		BindingContext = new CarritoViewModel();
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as CarritoViewModel;

        if (vm != null)
        {
            await vm.TraerProductosCommand  .ExecuteAsync(null);
        }
    }
}