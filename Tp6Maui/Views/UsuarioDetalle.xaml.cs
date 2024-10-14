using Tp6Maui.Models;
using Tp6Maui.ViewModels;
namespace Tp6Maui.Views;

public partial class UsuarioDetalle : ContentPage
{
	public UsuarioDetalle(Usuario param)
	{
		InitializeComponent();
		UsuarioDetalleViewModel vm=	new UsuarioDetalleViewModel();
		BindingContext = vm;
		vm.Usuario = param;
	}
}