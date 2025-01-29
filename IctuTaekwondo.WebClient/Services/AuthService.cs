using System.Net;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IctuTaekwondo.WebClient.Services
{
    public interface IAuthService
    {
        public void LoginAsync(
            LoginViewModel model,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies,
            IResponseCookies responseCookies);
    }

    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly ApiHelper _apiHelper;

        public AuthService(ILogger<AuthService> logger, ApiHelper apiHelper)
        {
            _logger = logger;
            _apiHelper = apiHelper;
        }

        public async void LoginAsync(
            LoginViewModel model,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies,
            IResponseCookies responseCookies)
        {
            var response = await _apiHelper.PostAsync<JwtResponse>("api/auth/login", model);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.Message != null) modelState.AddModelError(string.Empty, response.Message);
                if (response.Errors != null)
                {
                    foreach (var (key, value) in response.Errors)
                    {
                        foreach (var error in value)
                        {
                            modelState.AddModelError(key, error);
                        }
                    }
                }

                _logger.LogError("Login failed with status code: {StatusCode}, Message: {Message}", response.StatusCode, response.Message);
                return;
            }

            var jwtResponse = response.Data;
            if (jwtResponse == null)
            {
                modelState.AddModelError(string.Empty, "Không thể đăng nhập vào hệ thống");
                _logger.LogError("JWT response is null.");
                return;
            }

            if (requestCookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
            {
                responseCookies.Delete(GlobalConst.CookieAuthTokenKey);
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = model.RememberMe ? jwtResponse.ExpiredAt : null,
                SameSite = SameSiteMode.Strict
            };

            responseCookies.Append(GlobalConst.CookieAuthTokenKey, jwtResponse.Token, cookieOptions);

            _logger.LogInformation("User logged in successfully, token set in cookies.");
        }
    }
}
