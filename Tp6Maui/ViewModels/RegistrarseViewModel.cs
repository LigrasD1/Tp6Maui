using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp6Maui.ModelsDTO;
using Tp6Maui.Services;
using Tp6Maui.Views;

namespace Tp6Maui.ViewModels
{
    public partial class RegistrarseViewModel : BaseViewModel
    {
        [ObservableProperty]
        UserRegisterDTO _NuevoUsuario=new UserRegisterDTO();
        IUsuarioService _Servicio;
        public RegistrarseViewModel()
        {
            Title = "Registrarse";
            _Servicio = new UsuarioService();
        }

        [RelayCommand]
        public async Task RegistrarseAsync()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var response=_Servicio.RealizarRegistro(_NuevoUsuario);
                    if(response !=null)
                    {
                        IsBusy = false;

                        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

                    }
                    IsBusy = false;

                }
                catch (Exception)
                {
                    IsBusy = false;

                    await App.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error", "Ok");
                }finally
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
