using IctuTaekwondo.WebClient.Services;
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
            var user = _accountService.GetProfileAsync(Request).Result;
            return View(user);
        }
    }
}
