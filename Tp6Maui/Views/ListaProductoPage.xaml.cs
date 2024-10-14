using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class ListaProductoPage : ContentPage
{
	public ListaProductoPage()
	{
		InitializeComponent();
		BindingContext = new ListaProductoViewModel();
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ListaProductoViewModel;

        if (vm != null)
        {
            await vm.ObtenerProductosCommand.ExecuteAsync(null);
        }
    }
}