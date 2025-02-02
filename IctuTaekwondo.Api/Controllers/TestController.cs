using System.Net;
using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Test endpoint /api/test"
            });
        }
    }
}
