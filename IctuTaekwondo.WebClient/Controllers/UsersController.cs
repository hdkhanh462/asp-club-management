using IctuTaekwondo.WebClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(
            [FromQuery] List<string> search,
            [FromQuery] List<string> order,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var paginator = await _userService.GetAllAsync(
                page, size,
                search, order,
                ModelState, Request.Cookies);

            ViewData["QueryParameters"] = Request.Query;
            return View(paginator);
        }

        public IActionResult Detail(int id)
        {
            return View();
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsnyc(id, ModelState, Request.Cookies);
            return View(result);
        }
    }
}
