using IctuTaekwondo.WebClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IAccountService _authService;

        public HeaderViewComponent(IAccountService authService)
        {
            _authService = authService;
        }

        public IViewComponentResult Invoke()
        {
            if (User?.Identity?.IsAuthenticated == false) return View("LoggedOutNavButton");

            var user = _authService.GetUserAsync(Request.Cookies);
            if (user == null) return View("LoggedOutNavButton");

            return View("LoggedInNavButton", user);
        }
    }
}
