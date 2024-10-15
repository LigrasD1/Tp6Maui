using CommunityToolkit.Mvvm.Input;
using Tp6Maui.ViewModels;

namespace Tp6Maui.Views;

public partial class ListaUsuarioPage : ContentPage
{

	public ListaUsuarioPage()
	{
		InitializeComponent();
		BindingContext = new ListaUsuarioViewModel();
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ListaUsuarioViewModel;

        if (vm != null)
        {
            await vm.GetUserCommand.ExecuteAsync(null);
        }
    }

}