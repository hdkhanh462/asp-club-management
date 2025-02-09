using Htmx;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Services.Finances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class FinancesController : Controller
    {
        private readonly IFinancesService _service;

        public FinancesController(IFinancesService service)
        {
            _service = service;
        }

        private readonly List<string> validUrls = ["/Finances"];

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            ViewData["QueryParameters"] = Request.Query;

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];
            var list = await _service.GetAllAsync(token, page, size);

            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(int id, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            ViewData["QueryParameters"] = Request.Query;

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var detail = await _service.FindByIdAsync(token, id);
            if (detail == null) return NotFound();
            ViewData["CurrentFinance"] = detail;

            return View(new FinanceUpdateSchema
            {
                Id = detail.Id,
                Type = detail.Type,
                Category = detail.Category,
                Amount = detail.Amount,
                TransactionDate = detail.TransactionDate,
                Description = detail.Description,
            });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, FinanceUpdateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var @new = await _service.UpdateAsync(token, id, schema);
                if (@new != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Cập nhật giao dịch thành công",
                    }));

                    return PartialView("_UpdateFinanceFormPartial", new FinanceUpdateSchema
                    {
                        Id = @new.Id,
                        Type = @new.Type,
                        Category = @new.Category,
                        Amount = @new.Amount,
                        TransactionDate = @new.TransactionDate,
                        Description = @new.Description
                    });
                }
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Cập nhật giao dịch không thành công",
            }));

            return PartialView("_UpdateFinanceFormPartial", schema);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string? next)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var isSuccess = await _service.DeleteAsync(token, id);
                if (isSuccess)
                {
                    object? toast = toast = new
                    {
                        icon = "success",
                        title = "Xoá giao dịch thành công",
                    };

                    if (!string.IsNullOrEmpty(next) && validUrls.Contains(next))
                        toast = new
                        {
                            icon = "success",
                            title = "Xoá giao dịch thành công",
                            redirectUrl = next
                        };

                    Response
                        .Htmx(h => h
                        .WithTrigger("add-sweetalert2-toast", toast)
                        .WithTrigger("delete-elm", $"Row-{id}"));

                    return NoContent();
                }
            }

            Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
            {
                icon = "error",
                title = "Xoá giao dịch không thành công",
            }));

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] FinanceCreateSchema schema)
        {
            if (!Request.IsHtmx()) return BadRequest();

            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            if (ModelState.IsValid)
            {
                var newUser = await _service.CreateAsync(token, schema);
                if (newUser != null)
                {
                    Response.Htmx(h => h.WithTrigger("add-sweetalert2-toast", new
                    {
                        icon = "success",
                        title = "Tạo giao dịch thành công",
                    }));
                    return PartialView("_CreateFinanceFormPartial", new FinanceCreateSchema());
                }
            }
            return PartialView("_CreateFinanceFormPartial", schema);
        }

    }
}
