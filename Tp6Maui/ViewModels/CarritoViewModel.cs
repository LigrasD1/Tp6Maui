using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.Services;
using Tp6Maui.Utils;
using Tp6Maui.Views;

namespace Tp6Maui.ViewModels
{
    public partial class CarritoViewModel : BaseViewModel
    {
        public ObservableCollection<ProductoCarrito> productoscarito { get; } = new();
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        ProductoCarrito productoseleccionado;

        IProductoServices _ProductoServices;
        public CarritoViewModel()
        {
            Title = "Mi carrito";
            _ProductoServices = new ProductoServices();
        }
        [RelayCommand]
        public async Task TraerProductosAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var response = await _ProductoServices.GetProductoCarrito(Transports.IdUsuario);
                    if (response != null)
                    {
                        if (productoscarito.Count != 0) productoscarito.Clear();
                        foreach (ProductoCarrito producto in response) productoscarito.Add(producto);
                    }
                    IsBusy = false;

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");

                }
                finally { IsBusy = false; }
            }
        }
        [RelayCommand]
        public async Task ComprarAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var result = await _ProductoServices.ComprarCarroAsync(Transports.IdUsuario);
                    if (result != null)
                    {
                            IsBusy=false;
                            await App.Current.MainPage.DisplayAlert("Respuesta!", "Compra realizada con exito", "Ok");

                    }
                    else
                    {
                        IsBusy = false;

                        await App.Current.MainPage.DisplayAlert("Error!", "No se pudo realizar la compra", "Ok");

                    }
                    IsBusy=false;
                }
                catch (Exception ex)
                {

                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                }finally { IsBusy = false; }
            }
        }
        [RelayCommand]
        public async Task RemoveAsync()
        {
            if(!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    bool Confirmacion = await Application.Current.MainPage.DisplayAlert(
                       "Confirmar",
                       "¿Estás seguro de que deseas borrar el producto del carrito?",
                       "Sí",
                       "No");
                    if (Confirmacion)
                    {
                        var result = await _ProductoServices.RemoverProductoAsync(Transports.IdUsuario, productoseleccionado.ProductoId);
                        if (result != null)
                        {

                            IsBusy = false;

                            await App.Current.MainPage.DisplayAlert("Respuesta!", "producto eliminado del carrito", "Ok");

                        }
                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");

                }finally { IsBusy = false; }
            }
        }
        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }

}
