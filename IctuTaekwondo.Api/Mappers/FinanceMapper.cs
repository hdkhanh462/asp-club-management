using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Responses.Finance;

namespace IctuTaekwondo.Api.Mappers
{
    public static class FinanceMapper
    {
        public static FinanceResponse ToFinanceResponse(this Finance finance)
        {
            return new FinanceResponse
            {
                Id = finance.Id,
                Type = finance.Type,
                Category = finance.Category,
                Amount = finance.Amount,
                TransactionDate = finance.TransactionDate,
                Description = finance.Description,
                CreatedAt = finance.CreatedAt,
            };
        }
    }
}
