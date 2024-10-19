using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.Utils;
using Tp6Maui.Views;

namespace Tp6Maui.ViewModels
{
    public partial class BotoneraViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool _Permiso;
        public BotoneraViewModel()
        {
            Title = "Mercadito";
            if (Transports.IdRol == 2) Permiso = false;
            else Permiso = true;
        }

        [RelayCommand]
        public async Task GoToProductoLista()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ListaProductoPage());

        }
        [RelayCommand]
        public async Task GoToPutProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PutProductoPage());

        }

        [RelayCommand]
        public async Task GoToUsuarioLista()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ListaUsuarioPage());
        }
        [RelayCommand]
        public async Task GoToUsuarioPerfil()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PerfilPage());
        }

        [RelayCommand]
        public async Task GoToCarrito()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CarritoPage());
        }
        [RelayCommand]
        public async Task GotoPostProducto()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PostProductoPage());
        }

        [RelayCommand]
        public async Task GoToAcercaDe()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AcercaDe());
        }

        [RelayCommand]
        public async Task GoToLogin()
        {
            Transports.EmailUsuario=string.Empty;
            Transports.NombreUsuario=string.Empty;
            Transports.Name=string.Empty;
            Transports.IdUsuario = 0;
            Transports.Pone = string.Empty;
            await Application.Current.MainPage.Navigation.PopToRootAsync();
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
                Transports.EmailUsuario = string.Empty;
                Transports.NombreUsuario = string.Empty;
                Transports.Name = string.Empty;
                Transports.IdUsuario = 0;
                Transports.Pone = string.Empty;
                // Funcion para cerrar la aplicación
                Application.Current.Quit();
            }

        }
    }
}
