using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Finance;

namespace IctuTaekwondo.WebClient.Models
{
    public class DashboardViewModel
    {
        public List<FinanceReportResponse> Current = [];
        public List<FinanceReportResponse> Pass = [];
        public ReportRange Range;

        public long TotalAmount(List<FinanceReportResponse> reports)
        {
            return reports
            .Sum(report => report.Type == TransactionType.Income
                ? report.TotalAmount
                : -report.TotalAmount);
        }

        public double DifferenceTotalAmount()
        {
            if (!Current.Any() || !Pass.Any())
            {
                return 0;
            }

            long currentTotal = TotalAmount(Current);
            long passTotal = TotalAmount(Pass);

            if (passTotal == 0)
            {
                return currentTotal > 0 ? 100 : 0;
            }

            return ((double)(currentTotal - passTotal) / passTotal) * 100;
        }

        public double DifferenceByType(TransactionType type)
        {
            if (!Current.Any() || !Pass.Any())
            {
                return 0;
            }

            long current = Current
                .Where(report => report.Type == type)
                .Sum(report => report.TotalAmount);

            long pass = Pass
                .Where(report => report.Type == type)
                .Sum(report => report.TotalAmount);

            if (pass == 0)
            {
                return current > 0 ? 100 : 0;
            }

            return ((double)(current - pass) / pass) * 100;
        }
    }
}
