using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp6Maui.ViewModels
{
    public partial class AcercaViewModel : BaseViewModel
    {
        public AcercaViewModel()
        {
            Title = "Acerda del programador";
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
