using System.Drawing;
using System.Linq;
using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Finance;
using IctuTaekwondo.Shared.Schemas.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IctuTaekwondo.Api.Services
{
    public interface IFinanceService
    {
        Task<FinanceResponse?> CreateAsync(FinanceCreateSchema schema);
        Task<FinanceResponse?> UpdateAsync(int id, FinanceUpdateSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<Paginator<FinanceResponse>> GetAllAsync(int page, int size);
        Task<FinanceResponse?> GetByIdAsync(int id);
        public Task<long> GetTotalAmountAsync();
        public Task<List<FinanceReportResponse>> GetReportAsync(
            DateTime? startDate,
            DateTime? endDate,
            TransactionType? type = null,
            string[]? categories = null);
        Task<Paginator<FinanceResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            TransactionType? type = null,
            string[]? categories = null,
            DateTime? startDate = null,
            DateTime? endDate = null);
        Task<object> GetBarChart(int year);
    }

    public class FinanceService : IFinanceService
    {
        private readonly ILogger<FinanceService> _logger;
        private readonly ApiDbContext _context;

        public FinanceService(ILogger<FinanceService> logger, ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private List<FinanceReportResponse> CreateReport(List<Finance> finances)
        {
            var groupedByType = finances.GroupBy(e => e.Type);
            var report = new List<FinanceReportResponse>();

            foreach (var group in groupedByType)
            {
                var totalAmount = group.Sum(e => e.Amount);
                var transactionCount = group.Count();
                var averageTransaction = transactionCount > 0 ? totalAmount / transactionCount : 0;

                report.Add(new FinanceReportResponse
                {
                    Type = group.Key,
                    TransactionCount = transactionCount,
                    TotalAmount = totalAmount,
                    AverageAmount = averageTransaction
                });
            }

            return report;
        }

        private async Task<List<Finance>> FilterAsync(
            TransactionType? type,
            string[]? categories,
            DateTime? startDate,
            DateTime? endDate)
        {
            var query = _context.Finances.AsQueryable();

            if (type.HasValue) query = query.Where(f => f.Type == type.Value);

            if (categories != null && categories.Any()) query = query.Where(f => categories.Contains(f.Category));

            if (startDate.HasValue) query = query.Where(f => f.TransactionDate >= startDate.Value);

            if (endDate.HasValue) query = query.Where(f => f.TransactionDate <= endDate.Value);

            return await query.ToListAsync();
        }

        public async Task<FinanceResponse?> CreateAsync(FinanceCreateSchema schema)
        {
            var newFinance = new Finance
            {
                Type = schema.Type,
                Category = schema.Category,
                Amount = schema.Amount,
                TransactionDate = schema.TransactionDate,
                Description = schema.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Finances.Add(newFinance);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return newFinance.ToFinanceResponse();

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var finance = _context.Finances.FirstOrDefault(e => e.Id == id);
            if (finance == null)
            {
                _logger.LogError("Finance not found: {0}", id);
                throw new Exception("Giao dịch không tồn tại");
            }

            _context.Finances.Remove(finance);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Paginator<FinanceResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            TransactionType? type = null,
            string[]? categories = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var finances = await FilterAsync(type, categories, startDate, endDate);
            return new Paginator<FinanceResponse>(
                page, size,
                finances.Count,
                finances.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToFinanceResponse()).ToList());
        }

        public async Task<Paginator<FinanceResponse>> GetAllAsync(int page, int size)
        {
            var finances = await _context.Finances.ToListAsync();

            return new Paginator<FinanceResponse>(
                page, size,
                finances.Count,
                finances.Skip((page - 1) * size).Take(size).Select(e => e.ToFinanceResponse()).ToList());
        }

        public async Task<FinanceResponse?> GetByIdAsync(int id)
        {
            var finance = await _context.Finances
                .FirstOrDefaultAsync(e => e.Id == id);

            if (finance == null)
            {
                _logger.LogError("Finance not found: {0}", id);
                return null;
            }

            return finance.ToFinanceResponse();
        }

        public async Task<long> GetTotalAmountAsync()
        {
            var finances = await _context.Finances.ToListAsync();
            long totalAmount = 0;

            foreach (var finance in finances)
            {
                if (finance.Type == TransactionType.Income)
                {
                    totalAmount += finance.Amount;
                }
                else if (finance.Type == TransactionType.Expense)
                {
                    totalAmount -= finance.Amount;
                }
            }

            return totalAmount;
        }

        public async Task<FinanceResponse?> UpdateAsync(int id, FinanceUpdateSchema schema)
        {
            var finance = await _context.Finances
                .FirstOrDefaultAsync(e => e.Id == id);
            if (finance == null)
            {
                _logger.LogError("Finance not found: {0}", id);
                throw new Exception("Giao dịch không tồn tại");
            }

            finance.Type = schema.Type;
            finance.Category = schema.Category;
            finance.Amount = schema.Amount;
            finance.TransactionDate = schema.TransactionDate;
            finance.Description = schema.Description;

            _context.Entry(finance).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return await GetByIdAsync(id);

            return null;
        }

        public async Task<List<FinanceReportResponse>> GetReportAsync(
            DateTime? startDate,
            DateTime? endDate,
            TransactionType? type,
            string[]? categories)
        {
            var filteredFinances = await FilterAsync(type, categories, startDate, endDate);
            return CreateReport(filteredFinances);
        }

        public async Task<object> GetBarChart(int year)
        {
            var reportData = await _context.Finances
                .Where(f => f.TransactionDate.Date.Year == year)
                .GroupBy(f => new { f.TransactionDate.Date.Year, f.TransactionDate.Date.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Income = g.Where(f => f.Type == TransactionType.Income).Sum(f => f.Amount),
                    Expenses = g.Where(f => f.Type == TransactionType.Expense).Sum(f => f.Amount),
                })
                .OrderBy(g => g.Month)
                .ToListAsync();

            var response = new
            {
                series = new[]
            {
                new
                {
                    name = "Thu nhập",
                    color = "#31C48D",
                    data = reportData.Select(r => r.Income).ToArray()
                },
                new
                {
                    name = "Chi tiêu",
                    color = "#F05252",
                    data = reportData.Select(r => r.Expenses).ToArray()
                }
            },
                categories = reportData.Select(r => $"Thg{r.Month:00}").ToArray()
            };

            return response;
        }
    }
}
