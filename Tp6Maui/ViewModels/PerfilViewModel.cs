using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.ModelsDTO;
using Tp6Maui.Services;
using Tp6Maui.Utils;

namespace Tp6Maui.ViewModels
{
    public partial class PerfilViewModel : BaseViewModel
    {
        [ObservableProperty]
        UserRegisterDTO _UsuarioModificado=new UserRegisterDTO();
        IUsuarioService _Servicio;
        [ObservableProperty]
        string _Username;
        [ObservableProperty]
        string _Email;
        [ObservableProperty]
        string _Telefono;
        [ObservableProperty]
        string _Name;
        public PerfilViewModel()
        {
            Title = "Modificaciones de perfil";
            _Servicio = new UsuarioService();
            Username=Transports.NombreUsuario;
            Email = Transports.EmailUsuario;
            Telefono = Transports.Pone;
            Name = Transports.Name;
        }

        [RelayCommand]
        public async Task ConfirmarCambiosAsync()
        {
            if (!IsBusy)
            {
                try
                {

                    UsuarioModificado.name = Name;
                    UsuarioModificado.phone=Telefono;
                    
                    IsBusy = true;
                    var result= _Servicio.ModificarUsuario(UsuarioModificado);
                    if (result != null)
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta", "Usuario modificado correctamente", "Ok");

                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta", "ocurrio un error", "Ok");

                    }
                }
                catch (Exception)
                {

                    await App.Current.MainPage.DisplayAlert("Respuesta", "ocurrio un gran error", "Ok");
                    IsBusy = false;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        [RelayCommand]
        public async Task DeleteUsuarioAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var result=_Servicio.BorrarUsuario(Transports.IdUsuario);
                    if(result != null)
                    {
                        Transports.EmailUsuario = string.Empty;
                        Transports.NombreUsuario = string.Empty;
                        Transports.Name = string.Empty;
                        Transports.IdUsuario = 0;
                        Transports.Pone = string.Empty;
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta", "Usuario borrado con exito", "Ok");
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Respuesta", "No se ha podido borrar tu usuario", "Ok");

                    }
                }
                catch (Exception ex)
                {

                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");

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
