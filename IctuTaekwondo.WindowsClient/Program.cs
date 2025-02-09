using System.Configuration;
using Autofac;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.Shared.Services.Auth;
using IctuTaekwondo.Shared.Services.Events;
using IctuTaekwondo.Shared.Services.Finances;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WindowsClient.Forms.Achievements;
using IctuTaekwondo.WindowsClient.Forms.Events;
using IctuTaekwondo.WindowsClient.Forms.Finances;
using IctuTaekwondo.WindowsClient.Forms.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IctuTaekwondo.WindowsClient.Forms
{
    internal static class Program
    {
        public static IContainer AppContainer;
        private static string _apiUrl = ConfigurationManager.AppSettings["ApiUrl"]!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IApiService>(new ApiService(new HttpClient { BaseAddress = new Uri(_apiUrl) }));
                services.AddSingleton<IAuthService, AuthService>();
                services.AddSingleton<IAccountService, AccountService>();
                services.AddSingleton<IUsersService, UsersService>();
                services.AddSingleton<IEventsService, EventsService>();
                services.AddSingleton<IAchievementsService, AchievementsService>();
                services.AddSingleton<IFinancesService, FinancesService>();
                services.AddSingleton<LoginForm>();
                services.AddSingleton<MainForm>();
                services.AddSingleton<UsersForm>();
                services.AddSingleton<UsersDetailForm>();
                services.AddSingleton<EventsForm>();
                services.AddSingleton<EventsDetailForm>();
                services.AddSingleton<AchievementsForm>();
                services.AddSingleton<AchievementsDetailForm>();
                services.AddSingleton<FinancesForm>();
                services.AddSingleton<FinancesDetailForm>();
            });

            IHost host = builder.Build();

            host.Start();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var loginForm = host.Services.GetRequiredService<LoginForm>();

            Application.Run(loginForm);

            host.StopAsync().GetAwaiter().GetResult();
            host.Dispose();
        }
    }
}