using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Models;
using Tp6Maui.ModelsDTO;
using Tp6Maui.Services;

namespace Tp6Maui.ViewModels
{
    public partial class PostProductoViewModel : BaseViewModel
    {
        [ObservableProperty]
        ProductoDTO producto = new ProductoDTO();

        [ObservableProperty]
        IProductoServices _productoServices;

        [ObservableProperty]
        ImageSource _ModificarImagen; 
        public PostProductoViewModel()
        {
            Title = "Publicar producto";
            _productoServices = new ProductoServices();
        
        }
        [RelayCommand]
        public async Task MandarProductoAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                if(producto.Imagen==null || producto.title==null || producto.description==null || producto.price==null || producto.category==null || producto.stock==null) await App.Current.MainPage.DisplayAlert("Error!", "Completar todos los campos", "Ok");
             

                var response= _productoServices.PostProductosAsync(producto);

                if (response != null) 
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Bien!", $"Producto creado", "Ok");
                }
                else
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error!",$"{response.Exception}", "Ok");

                }

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
                        producto.Imagen = result;

                        var stream = await result.OpenReadAsync();
                        
                        ModificarImagen = ImageSource.FromStream(() => stream); // Asegura que esto está actualizando la UI
                        Application.Current.MainPage.BindingContext = this;
                        // Aquí se fuerza la actualización del BindingContext si no se refleja automáticamente
                        OnPropertyChanged(nameof(ModificarImagen));
                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error!", "Imagen no cargada", "Ok");
                    }
                    IsBusy = false;

                }
                catch (Exception ex)
                {
                    IsBusy = false;
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
