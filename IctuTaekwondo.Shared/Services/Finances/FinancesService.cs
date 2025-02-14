using System.Net;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Responses.Finance;
using IctuTaekwondo.Shared.Schemas.Achievement;
using IctuTaekwondo.Shared.Schemas.Finance;
using IctuTaekwondo.Shared.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Finances
{
    public class FinancesService : IFinancesService
    {
        private readonly IApiService apiService;

        public FinancesService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<FinanceResponse?> CreateAsync(string token, FinanceCreateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PostAsync<FinanceResponse>($"api/finances", schema.ToStringContent());
            return response.Data;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.DeleteAsync<object>($"api/finances/{id}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<FinanceResponse?> FindByIdAsync(string token, int id)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<FinanceResponse>($"api/finances/{id}");
            return response.Data;
        }

        public async Task<PaginationResponse<FinanceResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null)
        {
            apiService.SetAuthorizationHeader(token);

            query = query ?? new QueryBuilder();
            query.Add("page", page.ToString());
            query.Add("size", size.ToString());

            var response = await apiService.GetAsync<PaginationResponse<FinanceResponse>>($"api/finances{query.ToQueryString()}");
            return response.Data ?? PaginationResponse<FinanceResponse>.GetDefaultInstance();
        }

        public async Task<List<FinanceReportResponse>> GetReportAsync(string token, DateTime? startDate, DateTime? endDate)
        {
            var query = new QueryBuilder();
            if (startDate.HasValue && endDate.HasValue)
            {
                query.Add("start",startDate.Value.ToString("O"));
                query.Add("end", endDate.Value.ToString("O"));
            }

            apiService.SetAuthorizationHeader(token);

            var response = await apiService.GetAsync<List<FinanceReportResponse>>($"api/finances/report{query.ToQueryString()}");
            return response.Data!;
        }

        public async Task<FinanceResponse?> UpdateAsync(string token, int id, FinanceUpdateSchema schema)
        {
            apiService.SetAuthorizationHeader(token);

            var response = await apiService.PutAsync<FinanceResponse>($"api/finances/{id}", schema.ToStringContent());
            return response.Data;
        }
    }
}
