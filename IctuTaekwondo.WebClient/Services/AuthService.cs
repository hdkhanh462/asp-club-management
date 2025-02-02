using System.Net;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IctuTaekwondo.WebClient.Services
{
    public interface ICallApiService
    {
        public void HandleErrors<T>(ApiResponse<T> response, ModelStateDictionary modelState);
    }

    public interface IAuthService : ICallApiService
    {
        public Task<bool> LoginAsync(LoginViewModel model,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies,
            IResponseCookies responseCookies);
        public Task<bool> RegisterAsync(RegisterViewModel model,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies,
            IResponseCookies responseCookies);
        public bool Logout(IRequestCookieCollection requestCookies,
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

        public async Task<bool> LoginAsync(LoginViewModel model,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies,
            IResponseCookies responseCookies)
        {
            var response = await _apiHelper.PostAsync<JwtResponse>("api/auth/login", model);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                HandleErrors<JwtResponse>(response, modelState);
                return false;
            }

            var jwtResponse = response.Data;
            if (jwtResponse == null)
            {
                modelState.AddModelError(string.Empty, "Không thể đăng nhập vào hệ thống");
                _logger.LogError("JWT response is null.");
                return false;
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
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model,
            ModelStateDictionary modelState,
            IRequestCookieCollection requestCookies,
            IResponseCookies responseCookies)
        {
            if (!requestCookies.ContainsKey(GlobalConst.CookieAuthTokenKey)) return false;

            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                {
                    GlobalConst.ApiAuthorizationKey,
                    $"Bearer {requestCookies[GlobalConst.CookieAuthTokenKey]!}" 
                }
            });

            var response = await _apiHelper.PostAsync<object>("api/auth/register", model.ToDictionary(), "multipart/form-data");
            if (response.StatusCode != HttpStatusCode.Created)
            {
                HandleErrors<object>(response, modelState);
                return false;
            }

            return true;
        }

        public bool Logout(IRequestCookieCollection requestCookies, IResponseCookies responseCookies)
        {
            if (!requestCookies.ContainsKey(GlobalConst.CookieAuthTokenKey)) return false;

            responseCookies.Delete(GlobalConst.CookieAuthTokenKey);

            return true;
        }

        public void HandleErrors<T>(ApiResponse<T> response, ModelStateDictionary modelState)
        {
            if (response.Message != null && response.Errors == null) modelState.AddModelError(string.Empty, response.Message);
            if (response.Errors != null)
            {
                foreach (var (key, value) in response.Errors)
                {
                    var keyName = string.Empty;

                    if (key.Contains("Email") || key.Contains("UserName")) keyName = "Email";
                    if (key.Contains("Password")) keyName = "ConfirmPassword";

                    foreach (var error in value)
                    {
                        modelState.AddModelError(keyName, error);
                    }
                }
            }
            _logger.LogError("Register failed with status code: {StatusCode}, Message: {Message}", response.StatusCode, response.Message);
        }

    }
}
