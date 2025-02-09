using System.Net;
using IctuTaekwondo.Api.Services;
using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IctuTaekwondo.Shared.Responses.Finance;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Controllers
{
    [Route("api/finances")]
    [ApiController]
    public class FinancesController : ControllerBase
    {
        private readonly IFinanceService _financeService;

        public FinancesController(IFinanceService financeService)
        {
            _financeService = financeService;
        }

        // GET: api/finances?page=1&size=10
        // Lấy danh sách tài chính với phân trang
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFinances(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var paginator = await _financeService.GetAllAsync(page, size);

            return Ok(new ApiResponse<PaginationResponse<FinanceResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = paginator
            });
        }

        // GET: api/finances/filter?page=1&size=10&type=1&category=danhmuc&start=2025-01-01&end=2025-01-31
        // Lọc danh sách tài chính với phân trang
        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> GetFinancesWithFilter(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] TransactionType? type = null,
            [FromQuery] string[]? category = null,
            [FromQuery] DateTime? start = null,
            [FromQuery] DateTime? end = null)
        {
            var paginator = await _financeService.GetAllWithFilterAsync(page, size, type, category, start, end);

            return Ok(new ApiResponse<PaginationResponse<FinanceResponse>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = paginator
            });
        }

        // GET: api/finances/5
        // Lấy thông tin chi tiết tài chính theo id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFinance(int id)
        {
            var finance = await _financeService.GetByIdAsync(id);
            if (finance == null) return NotFound(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Tài chính không tồn tại"
            });

            return Ok(new ApiResponse<FinanceResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = finance
            });
        }

        // PUT: api/finances/5
        // Cập nhật thông tin tài chính
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFinance(int id, [FromBody] FinanceUpdateSchema schema)
        {
            if (id != schema.Id) return BadRequest(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.BadRequest
            });

            try
            {
                var result = await _financeService.UpdateAsync(id, schema);
                if (result == null) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cập nhật giao dịch thất bại"
                });

                return Ok(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Cập nhật giao dịch thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = ex.Message
                });
            }
        }

        // POST: api/finances
        // Tạo mới tài chính
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostFinance([FromBody] FinanceCreateSchema schema)
        {
            try
            {
                var result = await _financeService.CreateAsync(schema);
                if (result == null) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Tạo giao dịch thất bại"
                });

                return Ok(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Tạo giao dịch thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }
        }

        // DELETE: api/finances/5
        // Xoá tài chính theo id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFinance(int id)
        {
            try
            {
                var result = await _financeService.DeleteAsync(id);
                if (!result) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Xoá tài chính thất bại"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = ex.Message
                });
            }

            return Ok(new ApiResponse<object>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Xoá tài chính thành công"
            });
        }

        //GET: api/finances/total-amount
        // Lấy tổng số tiền giao dịch
        [HttpGet("total-amount")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalAmout()
        {
            var totalAmount = await _financeService.GetTotalAmountAsync();
            return Ok(new ApiResponse<long>
            {
                StatusCode = HttpStatusCode.OK,
                Data = totalAmount
            });
        }

        //GET: api/finances/report?start=2025-01-01&end=2025-01-31&type=1&categories=danhmuc
        // Lấy báo cáo tài chính theo loại
        [HttpGet("report")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReport(
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            [FromQuery] TransactionType? type,
            [FromQuery] string[]? categories)
        {
            try
            {
                if (end <= start) return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Ngày kết thúc phải lớn hơn Ngày bắt đầu"
                });

                var report = await _financeService.GetReportAsync(start, end, type, categories);
                return Ok(new ApiResponse<List<FinanceReportResponse>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = report
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }
    }
}
