using System.Configuration;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WindowsClient.Presenters;
using IctuTaekwondo.WindowsClient.Services;
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

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ApiUrl);

            ApiService apiService = new ApiService(client);

            IAuthService authService = new AuthService(apiService);
            IAccountService accountService = new AccountService(apiService);
            ILoginView loginView = LoginView.GetInstance();

            new LoginPresenter(loginView, authService, accountService, apiService);

            Application.Run(new MainForm());
        }
    }
}