using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.Services;
using Tp6Maui.Utils;

namespace Tp6Maui.ViewModels
{
    public partial class ProductoDetalleViewModel : BaseViewModel
    {
        [ObservableProperty]
        Producto producto;
        IProductoServices _servicio;
        [ObservableProperty]
        bool _Permiso;
        public ProductoDetalleViewModel()
        {
            Title = "Detalle del producto";
            _servicio = new ProductoServices();
            if (Transports.IdRol == 2) _Permiso = false;
            else _Permiso=true;
        }
        [RelayCommand]
        public async Task DeleteProducto()
        {
            if (!IsBusy)
            {
                  try
                   {
                        bool Confirmacion = await Application.Current.MainPage.DisplayAlert(
                           "Confirmar",
                           "¿Estás seguro de que deseas borrar el producto?",
                           "Sí",
                           "No");
                        if (Confirmacion)
                        {
                            IsBusy = true;

                            var result = _servicio.BorrarProducto(producto.id);
                            if (result != null)
                            {
                                IsBusy = false;
                                await App.Current.MainPage.DisplayAlert("Bien!", "Se borró el producto correctamente", "Ok");
                                await Application.Current.MainPage.Navigation.PopAsync();
                            }
                            else
                            {
                                IsBusy = false;
                                await App.Current.MainPage.DisplayAlert("Error!", "No se pudo borrar el producto", "Ok");
                            }

                        }

                  }
                  catch (Exception ex)
                  {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                  }
                  finally { IsBusy = false; }
                
                
                
            }
        }
        [RelayCommand]
        public async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
