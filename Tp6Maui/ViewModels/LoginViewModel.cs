using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Services;
using Tp6Maui.Utils;
using Tp6Maui.Views;

namespace Tp6Maui.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _Password;
        [ObservableProperty]
        string _Email;
        IUsuarioService _UsuarioService;
        public LoginViewModel() 
        {
            Title = "Login";
            _UsuarioService = new UsuarioService();
        }
        
        [RelayCommand]
        public async Task LoginAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var response = await _UsuarioService.ValidarLogin(Email, Password);

                    if(response!=null)
                    {
                        Transports.IdUsuario =response.id;
                        Transports.NombreUsuario = response.username;
                        Transports.EmailUsuario = response.email;
                        Transports.Pone = response.phone;
                        Transports.Name = response.name;
                        IsBusy = false;

                        await Application.Current.MainPage.Navigation.PushAsync(new BotoneraPage());
                    }
                    else
                    {
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Error", "Nombre de usuario o contraseña incorrectos", "Ok");
                    }
                }
                catch (Exception)
                {

                    IsBusy=false;
                        await App.Current.MainPage.DisplayAlert("Error", "Algo falló", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        public async Task GoToRegistrarseAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());

        }

        [RelayCommand]
        public async Task Cerrar()
        {
            bool Confirmacion = await Application.Current.MainPage.DisplayAlert(
                       "Confirmar",
                       "¿Estás seguro de que deseas cerrar la aplicación?",
                       "Sí",
                       "No");

            if (Confirmacion)
            {
                // Lógica para cerrar la aplicación
                Application.Current.Quit();
            }
        }

    }
}
