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

    public class LoginPresenter : IPresenter
    {
        private readonly ILoginView _view;
        private readonly IAuthService _authService;
        private readonly ILogger _logger;
        private readonly IDependencyResolver _resolver;

        public LoginPresenter(ILoginView view, IAuthService authService, ILogger logger, IDependencyResolver resolver)
        {
            _view = view;
            _authService = authService;
            _logger = logger;
            _resolver = resolver;
            _view.SetPresenter(this);
        }

        public void Run()
        {
            _logger.Log("MainPresenter is running.");
            _view.LoginEvent += _LoginEvent;
            _GetSavedCred();
        }

        public T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        private void _GetSavedCred()
        {
            try
            {
                var password = CredentialManager.RetrieveCredentials(CredentialManager.ApplicationName, out var userName);
                _view.EmailValue = userName;
                _view.PasswordValue = password;
                _view.RememberMeValue = true;
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
                Email = _view.EmailValue,
                Password = _view.PasswordValue,
                RememberMe = _view.RememberMeValue
            });

            if (jwt != null && !string.IsNullOrEmpty(jwt.Token))
            {
                if (_view.RememberMeValue)
                    CredentialManager.SaveCredentials(CredentialManager.ApplicationName, _view.EmailValue, _view.PasswordValue);
                else
                    CredentialManager.SaveCredentials(CredentialManager.ApplicationName, _view.EmailValue, string.Empty);

                IMainView mainView = MainView.GetInstance();
            }
            else
            {
                // Handle login failure (e.g., show error message)
            }
        }
    }
}
