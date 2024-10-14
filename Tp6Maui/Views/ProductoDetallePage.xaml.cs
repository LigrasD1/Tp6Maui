using Tp6Maui.Models;
using Tp6Maui.ViewModels;

namespace Tp6Maui.Views;

public partial class ProductoDetallePage : ContentPage
{
	public ProductoDetallePage(Producto param)
	{
		InitializeComponent();
		ProductoDetalleViewModel vm = new ProductoDetalleViewModel();
		this.BindingContext = vm;
		vm.Producto=param;
	}
}