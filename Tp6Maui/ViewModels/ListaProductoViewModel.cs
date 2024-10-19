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
    public partial class ListaProductoViewModel : BaseViewModel
    {
        public ObservableCollection<Producto> productos { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        int Paginas;

        [ObservableProperty]
        Producto productoseleccionado;

        IProductoServices _ProductoServices;
        public ListaProductoViewModel()
        {
            Title = "Lista de productos";
            _ProductoServices = new ProductoServices();
        }

        [RelayCommand]

        public async Task ObtenerProductosAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    Paginas++;

                    IsBusy = true;
                    var LProductos = await _ProductoServices.GetProductosAsync(Paginas);
                    if (LProductos != null) {
                        
                        foreach (Producto producto in LProductos) productos.Add(producto);

                    }
                    else
                    
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                }
                finally
                {
                    IsBusy=false;
                }
            }
        }
        [RelayCommand]
        public async Task RefrescarProductos()
        {
            if (!IsBusy)
            {
                try
                {
                    Paginas = 1;
                    IsBusy = true;
                    var LProductos = await _ProductoServices.GetProductosAsync(Paginas);
                    if (LProductos != null)
                    {
                        if (productos.Count != 0)
                        {
                            productos.Clear();

                        }
                        foreach (Producto producto in LProductos) productos.Add(producto);

                    }
                    else

                        IsBusy = false;
                }
                catch (Exception ex)
                {

                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        [RelayCommand]
        public async Task GoToUsuarioPerfil()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PerfilPage());
        }

        [RelayCommand]
        public async Task GoToDetail()
        {
            bool Decision = await Application.Current.MainPage.DisplayAlert(
                           "Confirmar",
                           "¿Que desea hacer?",
                           "comprar producto",
                           "Detalle del producto");
            if (!Decision)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(productoseleccionado), true);
            }
            else
            {
                if (!IsBusy)
                {
                    try
                    {
                        IsBusy=true;
                        var result = await _ProductoServices.AgregarAlCarritoAsync(Transports.IdUsuario, productoseleccionado.id, 1);
                        if (result != null)
                        {
                            IsBusy = false;

                            await App.Current.MainPage.DisplayAlert("Respuesta!", $"{result}", "Ok");
                        }
                        else
                        {
                            IsBusy = false;

                            await App.Current.MainPage.DisplayAlert("Error!", "No se pudo agregar el producto", "Ok");
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
            Productoseleccionado = null;
        }

        
        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
