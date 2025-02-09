using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Winforms.Forms;
using IctuTaekwondo.Winforms.Forms.Users;
using IctuTaekwondo.Winforms.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IctuTaekwondo.Winforms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IApiService>(new ApiService(new HttpClient
                { BaseAddress = new Uri("http://localhost:9000") }));
                services.AddSingleton<LoginForm>();
                services.AddSingleton<MainForm>();
                services.AddSingleton<UsersForm>();
                services.AddSingleton<UsersDetailForm>();
                services.AddSingleton<RegisterForm>();
                //services.AddSingleton<EventsForm>();
                //services.AddSingleton<EventsDetailForm>();
                //services.AddSingleton<AchievementsForm>();
                //services.AddSingleton<AchievementsDetailForm>();
                //services.AddSingleton<FinancesForm>();
                //services.AddSingleton<FinancesDetailForm>();
            });

            IHost host = builder.Build();
            host.Start();

            var loginForm = host.Services.GetRequiredService<LoginForm>();

            Application.Run(loginForm);

            host.StopAsync().GetAwaiter().GetResult();
            host.Dispose();
        }
    }
}
