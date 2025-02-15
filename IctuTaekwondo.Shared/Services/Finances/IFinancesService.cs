using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Http.Extensions;
using IctuTaekwondo.Shared.Responses.Finance;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Services.Finances
{
    public interface IFinancesService
    {
        public Task<Paginator<FinanceResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null);
        public Task<FinanceResponse?> FindByIdAsync(string token, int id);
        public Task<Dictionary<string, string[]>> CreateAsync(string token, FinanceCreateSchema schema);
        public Task<FinanceResponse?> UpdateAsync(string token, int id, FinanceUpdateSchema schema);
        public Task<bool> DeleteAsync(string token, int id);
        public Task<List<FinanceReportResponse>> GetReportAsync(string token, DateTime? startDate, DateTime? endDate);
        Task<object?> GetBarChart();
    }
}
