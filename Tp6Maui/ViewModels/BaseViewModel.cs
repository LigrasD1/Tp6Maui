using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp6Maui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;//esta variable la podemos cambiar siempre que queramos para indicar que la aplicación esta ocupada
        //Por ejemplo para evitar que el usuario realice mas cambios mientra se consulta la base de datos

        [ObservableProperty]
        private string? title;
    }
}
