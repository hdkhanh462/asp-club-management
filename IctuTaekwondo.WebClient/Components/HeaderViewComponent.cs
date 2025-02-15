using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IAccountService accountService;

        public HeaderViewComponent(IAccountService authService)
        {
            accountService = authService;
        }

        public IViewComponentResult Invoke()
        {
            if (User?.Identity?.IsAuthenticated == false) return View("LoggedOutNavButton");
            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
            var user = accountService.GetUser(token).Result;
            if (user == null) return View("LoggedOutNavButton");

            return View("LoggedInNavButton", user);
        }
    }
}
