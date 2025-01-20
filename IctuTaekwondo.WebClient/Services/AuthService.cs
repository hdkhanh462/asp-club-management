using System.Net;
using System.Security.Claims;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WebClient.Models;

namespace IctuTaekwondo.WebClient.Services
{
    public class AuthService
    {
        private readonly APIHelper _api;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
            _api = new APIHelper(_config["ApiUrl"]!);
        }

        public async Task<LoginResponse?> LoginAsync(LoginViewModel schema)
        {
            try
            {
                var response = await _api.PostAsync<LoginResponse>("auth/login", schema);
                return response;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                // Handle 400 Bad Request  
                throw new Exception("Thông tin đăng nhập không hợp lệ.", ex);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Handle 401 Unauthorized  
                throw new Exception("Truy cập không được phép.", ex);
            }
            catch (HttpRequestException ex)
            {
                // Handle other HTTP request errors  
                throw new Exception("Đã xảy ra lỗi khi đăng nhập.", ex);
            }
            catch (Exception ex)
            {
                // Handle other possible errors  
                throw new Exception("Đã xảy ra lỗi không mong muốn.", ex);
            }
        }

        public async Task<ApiResponse?> RegisterAsync(RegisterViewModel schema)
        {
            try
            {
                var result = await _api.PostAsync<ApiResponse>("auth/register", schema);
                return result;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                // Handle 400 Bad Request  
                throw new Exception("Thông tin đăng ký không hợp lệ.", ex);
            }
            catch (HttpRequestException ex)
            {
                // Handle other HTTP request errors  
                throw new Exception("Đã xảy ra lỗi khi đăng ký.", ex);
            }
            catch (Exception ex)
            {
                // Handle other possible errors  
                throw new Exception("Đã xảy ra lỗi không mong muốn.", ex);
            }
        }

        public bool IsAuthenticated(ClaimsPrincipal principal)
        {
            return principal.Identity?.IsAuthenticated ?? false;
        }
    }
}
