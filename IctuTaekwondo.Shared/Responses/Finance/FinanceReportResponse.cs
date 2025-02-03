using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Responses.Finance
{
    public class FinanceReportResponse
    {
        public TransactionType Type { get; set; }
        public int TransactionCount { get; set; }
        public long TotalAmount { get; set; }
        public long AverageAmount { get; set; }
    }
}
