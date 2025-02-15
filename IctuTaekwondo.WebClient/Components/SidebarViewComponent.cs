using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Components
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;

        public SidebarViewComponent(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IViewComponentResult Invoke()
        {
            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
            var user = _accountService.GetProfileAsync(token).Result;
            return View(user);
        }
    }
}
