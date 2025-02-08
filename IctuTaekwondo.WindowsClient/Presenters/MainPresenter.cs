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

    public class MainPresenter : IPresenter
    {
        private string _token;
        private IMainView _view;
        private IAccountService _accountService;

        public MainPresenter(IMainView mainView, IAccountService accountService)
        {
            _view = mainView;
            _accountService = accountService;
            _view.SetPresenter(this);

        }

        private async void ShowView(string token)
        {
            var user = await _accountService.GetProfileAsync(token);
            if (user != null)
            {
                _view.User = user;
                _view.Show();
            }
        }

        
        public void Run()
        {
            _view.LogoutEvent += _LogoutEvent;
            _view.ShowLoginViewEvent += _ShowLoginViewEvent;
            _view.ShowUsersViewEvent += _ShowUsersViewEvent;
        }

        private void _LogoutEvent(object? sender, EventArgs e)
        {
            CredentialManager.SaveCredentials(CredentialManager.ApplicationName, _view.User.Email!, string.Empty);
            _view.Hide();
            _ShowLoginViewEvent(sender, e);
        }

        private void _ShowLoginViewEvent(object? sender, EventArgs e)
        {
            //IAuthService authService = new AuthService(_apiService);
            //ILoginView loginView = LoginView.GetInstance();
            //new LoginPresenter(loginView, authService, authService);
            //_mainView.Hide();
            //loginView.Show();
        }

        private void _ShowUsersViewEvent(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
