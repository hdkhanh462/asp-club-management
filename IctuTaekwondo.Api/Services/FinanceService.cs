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

namespace IctuTaekwondo.Api.Services
{
    public interface IFinanceService
    {
        Task<bool> CreateAsync(FinanceCreateSchema schema);
        Task<bool> UpdateAsync(int id, FinanceUpdateSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResponse<FinanceResponse>> GetAllAsync(int page, int size);
        Task<FinanceResponse?> GetByIdAsync(int id);
        public Task<long> GetTotalAmountAsync();
        public Task<List<FinanceReportResponse>> GetReportAsync(
            DateTime startDate,
            DateTime endDate,
            TransactionType? type = null,
            string[]? categories = null);
        Task<PaginationResponse<FinanceResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            TransactionType? type = null,
            string[]? categories = null,
            DateTime? startDate = null,
            DateTime? endDate = null);
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
            DateTime? endDate,
            int? page = null,
            int? size = null)
        {
            var query = _context.Finances.AsQueryable();

            if (type.HasValue) query = query.Where(f => f.Type == type.Value);

            if (categories != null && categories.Any()) query = query.Where(f => categories.Contains(f.Category));

            if (startDate.HasValue) query = query.Where(f => f.TransactionDate >= startDate.Value);

            if (endDate.HasValue) query = query.Where(f => f.TransactionDate <= endDate.Value);

            if (page.HasValue && size.HasValue)
            {
                return await query
                .Skip((page.Value - 1) * size.Value)
                .Take(size.Value)
                .ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<bool> CreateAsync(FinanceCreateSchema schema)
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

            return result > 0;
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

        public async Task<PaginationResponse<FinanceResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            TransactionType? type = null,
            string[]? categories = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var finances = await FilterAsync(type, categories, startDate, endDate, page, size);
            return new PaginationResponse<FinanceResponse>(finances.Count, size)
            {
                CurrentPage = page,
                Items = finances.Select(e => e.ToFinanceResponse()).ToList()
            };
        }

        public async Task<PaginationResponse<FinanceResponse>> GetAllAsync(int page, int size)
        {
            var finances = await _context.Finances
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginationResponse<FinanceResponse>(finances.Count, size)
            {
                CurrentPage = page,
                Items = finances.Select(e => e.ToFinanceResponse()).ToList()
            };
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

        public async Task<bool> UpdateAsync(int id, FinanceUpdateSchema schema)
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

            return result > 0;
        }

        public async Task<List<FinanceReportResponse>> GetReportAsync(
            DateTime startDate,
            DateTime endDate,
            TransactionType? type,
            string[]? categories)
        {
            var filteredFinances = await FilterAsync(type, categories, startDate, endDate);
            return CreateReport(filteredFinances);
        }
    }
}
