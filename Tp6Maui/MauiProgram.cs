using Microsoft.Extensions.Logging;
using Tp6Maui.Services;
using Tp6Maui.ViewModels;
using Tp6Maui.Views;

namespace Tp6Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fontello.ttf", "MaterialDesignIcons");
                });


            builder.Services.AddSingleton<IProductoServices, ProductoServices>();
            builder.Services.AddSingleton<IUsuarioService, UsuarioService>();


            builder.Services.AddTransient<RegistrarseViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<PutProductoViewModel>();
            builder.Services.AddTransient<PutProductoPage>();
            builder.Services.AddTransient<PostProductoViewModel>();
            builder.Services.AddTransient<PostProductoPage>();
            builder.Services.AddTransient<ListaProductoViewModel>();
            builder.Services.AddTransient<ListaProductoPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
