using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Models;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ApiHelper _apiHelper;

        public HeaderViewComponent(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public IViewComponentResult Invoke()
        {
            if (User?.Identity?.IsAuthenticated == false || !Request.Cookies.ContainsKey(GlobalConst.CookieAuthTokenKey))
            {
                return View("LoggedOutNavButton");
            }

            _apiHelper.AddHeaders(new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {Request.Cookies[GlobalConst.CookieAuthTokenKey]}"
            });

            var userDetail = _apiHelper.GetAsync<UserFullDetailResponse>("api/auth/profile").Result.Data;
            if (userDetail == null) return View("LoggedOutNavButton");

            return View("LoggedInNavButton", userDetail);
        }
    }
}
