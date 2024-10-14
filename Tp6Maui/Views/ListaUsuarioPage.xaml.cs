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
    
}