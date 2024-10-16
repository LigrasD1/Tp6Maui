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
using Tp6Maui.Views;

namespace Tp6Maui.ViewModels
{
    public partial class ListaUsuarioViewModel : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; } = new();
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Usuario _Usuarioseleccionado;

        IUsuarioService _usuarioservices;
        public ListaUsuarioViewModel()
        {
            Title = "Lista de usuarios";
            _usuarioservices = new UsuarioService();
        }
        [RelayCommand]
        private async Task GetUserAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    var LUsuarios = await _usuarioservices.GetUsuariosAsync();

                    if (LUsuarios != null)
                    {
                        if (Usuarios.Count != 0)
                            Usuarios.Clear();

                        foreach (var producto in LUsuarios)
                            Usuarios.Add(producto);

                    }

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
            Usuarioseleccionado = null;
        }
        [RelayCommand]
        public async Task GoToDetail()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UsuarioDetalle(Usuarioseleccionado),true);
        }
        [RelayCommand]
        public async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
