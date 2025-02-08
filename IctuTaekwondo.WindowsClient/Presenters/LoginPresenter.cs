using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WindowsClient.Services;
using IctuTaekwondo.WindowsClient.Utils;
using IctuTaekwondo.WindowsClient.Views;

namespace IctuTaekwondo.WindowsClient.Presenters
{

    public class LoginPresenter
    {
        private ILoginView _loginView;
        private IAuthService _authService;
        private IAccountService _accountService;
        private ApiService _apiService;

        public LoginPresenter(ILoginView loginView, IAuthService authService, IAccountService accountService, ApiService apiService)
        {
            _loginView = loginView;
            _authService = authService;
            _accountService = accountService;
            _apiService = apiService;

            _loginView.LoginEvent += _LoginEvent;

            try
            {
                var password = CredentialManager.RetrieveCredentials(CredentialManager.ApplicationName, out var userName);
                _loginView.EmailValue = userName;
                _loginView.PasswordValue = password;
                _loginView.RememberMeValue = true;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
            }
        }

        private async void _LoginEvent(object? sender, EventArgs e)
        {
            var jwt = await _authService.LoginAsync(new LoginSchema
            {
                Email = _loginView.EmailValue,
                Password = _loginView.PasswordValue,
                RememberMe = _loginView.RememberMeValue
            });

            if (jwt != null && !string.IsNullOrEmpty(jwt.Token))
            {
                if (_loginView.RememberMeValue)
                    CredentialManager.SaveCredentials(CredentialManager.ApplicationName, _loginView.EmailValue, _loginView.PasswordValue);
                else
                    CredentialManager.SaveCredentials(CredentialManager.ApplicationName, _loginView.EmailValue, string.Empty);

                IMainView mainView = MainView.GetInstance();

                _loginView.Hide();

                new MainPresenter(jwt.Token, mainView, _accountService, _apiService);
            }
            else
            {
                // Handle login failure (e.g., show error message)
            }
        }
    }
}
