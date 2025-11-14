using Microsoft.Extensions.Logging;
using SMITCOMIDAS.Services;
using SMITCOMIDAS.Services.SnackBark;
using SMITCOMIDAS.Shared.Services;
using SMITCOMIDAS.Shared.Services.SnackBar;

namespace SMITCOMIDAS
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
                });

            // Add device-specific services used by the SMITCOMIDAS.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IPlatformService, PlatformService>();
            builder.Services.AddSingleton<IPersonasService, PersonasService>();
            builder.Services.AddSingleton<ICentrosCostoService, CentrosCostoService>();
            builder.Services.AddSingleton<ICompaniasService, CompaniasService>();
            builder.Services.AddSingleton<IProveedoresService, ProveedoresService>();
            builder.Services.AddSingleton<IMenuService, MenuService>();
            builder.Services.AddSingleton<IElementoMenuService, ElementoMenuService>();
            builder.Services.AddSingleton<IDisponibilidadElementoService, DisponibilidadElementoService>();
            builder.Services.AddSingleton<IRolesService, RolesService>();
            builder.Services.AddSingleton<IPedidoService, PedidoService>();

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddScoped(sp =>
            {
                var handler = sp.GetService<AuthenticationHandler>();
                if (handler != null)
                {
                    return new HttpClient(handler)
                    {
                        BaseAddress = new Uri("http://10.0.2.2:5000/")
                    };
                }
                return new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5000/") };
            });


            builder.Services.AddScoped<IToastService, ToastService>();
            builder.Services.AddScoped<IDialogService, DialogService>();
            builder.Services.AddScoped<ISnackbar, SnackbarService>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
