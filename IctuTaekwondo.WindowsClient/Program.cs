using System.Configuration;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WindowsClient.Presenters;
using IctuTaekwondo.WindowsClient.Services;
using IctuTaekwondo.WindowsClient.Utils;
using IctuTaekwondo.WindowsClient.Views;
using YourNamespace;

namespace IctuTaekwondo.WindowsClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"]!;

            HttpClient client = new()
            {
                BaseAddress = new Uri(ApiUrl)
            };

            var resolver = new DependencyResolver();
            resolver.Register<IApiService, ApiService>(resolver => new ApiService(client));
            resolver.RegisterSingleton<ILogger, ConsoleLogger>();
            resolver.Register<ILoginView, LoginView>();
            resolver.Register<IPresenter, LoginPresenter>(resolver => new LoginPresenter(
                resolver.Resolve<ILoginView>(),
                resolver.Resolve<IAuthService>(), 
                resolver.Resolve<ILogger>(),
                resolver
            ));

            var loginView = resolver.Resolve<ILoginView>() as Form;

            Application.Run(loginView!);
        }
    }
}