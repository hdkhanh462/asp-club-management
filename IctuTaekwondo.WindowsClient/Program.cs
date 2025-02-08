using System.Configuration;
using Autofac;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Auth;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.Shared.Utils;

namespace IctuTaekwondo.WindowsClient.Forms
{
    internal static class Program
    {
        public static IContainer AppContainer;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            AppContainer = Configure();

            Application.Run(new LoginForm(AppContainer));
        }

        static IContainer Configure()
        {
            string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"]!;

            HttpClient client = new()
            {
                BaseAddress = new Uri(ApiUrl)
            };

            var apiService = new ApiService(client);

            var builder = new ContainerBuilder();

            builder.RegisterInstance(apiService).As<IApiService>();
            builder.RegisterInstance(new AuthService(apiService)).As<IAuthService>();
            builder.RegisterInstance(new AccountService(apiService)).As<IAccountService>();
            builder.RegisterInstance(new UsersService(apiService)).As<IUsersService>();
            builder.RegisterType<LoginForm>();

            return builder.Build();
        }
    }
}