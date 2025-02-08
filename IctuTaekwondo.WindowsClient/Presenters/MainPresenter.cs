using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WindowsClient.Services;
using IctuTaekwondo.WindowsClient.Utils;
using IctuTaekwondo.WindowsClient.Views;

namespace IctuTaekwondo.WindowsClient.Presenters
{

    public class MainPresenter
    {
        private string _token;
        private IMainView _mainView;
        private IAccountService _accountService;
        private ApiService _apiService;

        public MainPresenter(string token, IMainView mainView, IAccountService accountService, ApiService apiService)
        {
            _token = token;
            _mainView = mainView;
            _accountService = accountService;
            _apiService = apiService;

            _mainView.LogoutEvent += _LogoutEvent;
            _mainView.ShowLoginViewEvent += _ShowLoginViewEvent;
            _mainView.ShowUsersViewEvent += _ShowUsersViewEvent;

            ShowView(token);
        }

        private void _ShowUsersViewEvent(object? sender, EventArgs e)
        {
            var service = new UsersService(_apiService);
            var view = UsersView.GetInstance();
            new UserPresenter(_token, view, service);
        }

        private async void ShowView(string token)
        {
            var user = await _accountService.GetProfileAsync(token);
            if (user != null)
            {
                _mainView.User = user;
                _mainView.Show();
            }
        }

        private void _LogoutEvent(object? sender, EventArgs e)
        {
            CredentialManager.SaveCredentials(CredentialManager.ApplicationName, _mainView.User.Email!, string.Empty);
            _mainView.Hide();
            _ShowLoginViewEvent(sender, e);
        }

        private void _ShowLoginViewEvent(object? sender, EventArgs e)
        {
            IAuthService authService = new AuthService(_apiService);
            ILoginView loginView = LoginView.GetInstance();
            new LoginPresenter(loginView, authService, _accountService, _apiService);
            _mainView.Hide();
            loginView.Show();
        }
    }
}
