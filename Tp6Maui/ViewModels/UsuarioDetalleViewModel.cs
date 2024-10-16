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
    public partial class UsuarioDetalleViewModel : BaseViewModel
    {
        [ObservableProperty]
        Usuario _usuario;
        IUsuarioService _Servicio;

        [ObservableProperty]
        bool _Permiso;
        public UsuarioDetalleViewModel()
        {
            Title = "Detalle del usuario";
            _Servicio = new UsuarioService();
            if (Transports.IdRol == 2) Permiso = false;
        }

        [RelayCommand]
        public async Task DeleteUsuarioAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy= true;
                    bool Confirmacion = await Application.Current.MainPage.DisplayAlert(
                       "Confirmar",
                       "¿Estás seguro de que deseas borrar el usuario?",
                       "Sí",
                       "No");

                    if (Confirmacion)
                    {
                        var response = _Servicio.BorrarUsuario(Usuario.id);
                        if (response != null)
                        {
                            IsBusy = false;
                            await App.Current.MainPage.DisplayAlert("Bien!", "Se borró el usuario correctamente", "Ok");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            IsBusy = false;
                            await App.Current.MainPage.DisplayAlert("Error!", "No se pudo borrar el usuario", "Ok");
                        }
                        IsBusy = false;

                    }

                }
                catch (Exception ex)
                {
                            IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error Fatal!", $"Ha ocurrido un error: {ex.Message}", "Ok");

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        
        [RelayCommand]
        private async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
