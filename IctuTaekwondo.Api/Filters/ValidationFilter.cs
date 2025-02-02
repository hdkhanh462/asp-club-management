using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace IctuTaekwondo.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var apiResponse = new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Dữ liệu không hợp lệ",
                Errors = errors,
            };

            context.Result = new BadRequestObjectResult(apiResponse);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Không cần xử lý gì sau action.
    }
}
