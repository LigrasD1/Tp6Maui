using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.ModelsDTO;
using Tp6Maui.Services;

namespace Tp6Maui.ViewModels
{
    public partial class PutProductoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Producto _ProductoRecibido;

        [ObservableProperty]
        int id;

        [ObservableProperty]
        ProductoDTO _ModificarProducto= new ProductoDTO();

        [ObservableProperty]
        ImageSource _ImageSource;

        IProductoServices _servicio;
        public PutProductoViewModel()
        {
            Title = "Midifcación del producto";
            _servicio = new ProductoServices();
        }

        [RelayCommand]
        public async Task ModificarProductoAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    ModificarProducto.title=ProductoRecibido.title;
                    ModificarProducto.price = ProductoRecibido.price;
                    ModificarProducto.description = ProductoRecibido.description;
                    ModificarProducto.category = ProductoRecibido.category;
                    ModificarProducto.stock = ProductoRecibido.stock;



                    var response = _servicio.PutProductosAsync(id, ModificarProducto);
                    if (response!=null) 
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta", "Producto modificado correctamente", "Ok");

                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error catastrofico!", "Hubo un error al intentar cargar el producto", "Ok");

                    }
                }
                catch (Exception)
                {

                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error catastrofico!", "Hubo un error al intentar cargar el producto", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }

        }

        [RelayCommand]
        public async Task BuscarPorIdAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    _ProductoRecibido = await _servicio.SearchByIdAsync(id);
                    if (_ProductoRecibido != null) 
                    {
                        IsBusy = false;
                        //recargar las propiedades para que se vean
                        OnPropertyChanged(nameof(ProductoRecibido));

                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error!", "Producto no encontrado", "Ok");

                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    IsBusy = false;

                    await App.Current.MainPage.DisplayAlert("Error!", $"{ex.Message}", "Ok");
                }finally { IsBusy = false; }
            }
        }
        [RelayCommand]
        public async Task BuscarImagenAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    FileResult? result = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Selecciona una imagen"
                    });

                    if (result != null)
                    {
                        // Asignas el resultado al modelo
                        ModificarProducto.Imagen = result;

                        var stream = await result.OpenReadAsync();

                        _ImageSource = ImageSource.FromStream(() => stream); // Asegura que esto está actualizando la UI
                        Application.Current.MainPage.BindingContext = this;
                        // Aquí se fuerza la actualización del BindingContext si no se refleja automáticamente
                        OnPropertyChanged(nameof(ImageSource));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", "Imagen no cargada", "Ok");
                    }
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
        public async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
