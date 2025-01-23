using IctuTaekwondo.Shared.Mappers;
using IctuTaekwondo.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HeaderViewComponent(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            if (!_signInManager.IsSignedIn(HttpContext.User)) return View("LoggedOutNavButton");
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            return View("LoggedInNavButton", user?.ToUserResponse());
        }
    }
}
