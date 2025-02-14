using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace IctuTaekwondo.Shared.Utils
{
    public static class ReportHelper
    {
        public static (DateTime StartDate, DateTime EndDate) GetYearly(int year)
        {
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = new DateTime(year, 12, 31);
            return (startDate, endDate);
        }

        public static (DateTime StartDate, DateTime EndDate) GetQuarterly(int year, int quarter)
        {
            if (quarter < 1 || quarter > 4)
                throw new ArgumentOutOfRangeException(nameof(quarter), "Quarter must be between 1 and 4.");

            int startMonth = (quarter - 1) * 3 + 1;
            DateTime startDate = new DateTime(year, startMonth, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            return (startDate, endDate);
        }

        public static (DateTime StartDate, DateTime EndDate) GetMonthly(int year, int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");

            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            return (startDate, endDate);
        }

        public static (DateTime StartDate, DateTime EndDate) GetPastPeriodDates(ReportRange range)
        {
            DateTime endDate = DateTime.UtcNow;

            switch (range)
            {
                case ReportRange.Month:
                    return GetMonthly(endDate.Year, endDate.Month - 1);
                case ReportRange.Quarter:
                    int currentQuarter = (endDate.Month - 1) / 3 + 1;
                    return GetQuarterly(endDate.Year, currentQuarter - 1);
                case ReportRange.Year:
                    return GetYearly(endDate.Year - 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(range), range, "Invalid report range specified.");
            }
        }

        public static (DateTime StartDate, DateTime EndDate) GetCurrentDates(ReportRange range)
        {
            DateTime endDate = DateTime.UtcNow;

            switch (range)
            {
                case ReportRange.Month:
                    return GetMonthly(endDate.Year, endDate.Month);
                case ReportRange.Quarter:
                    int currentQuarter = (endDate.Month - 1) / 3 + 1;
                    return GetQuarterly(endDate.Year, currentQuarter);
                case ReportRange.Year:
                    return GetYearly(endDate.Year);
                default:
                    throw new ArgumentOutOfRangeException(nameof(range), range, "Invalid report range specified.");
            }
        }
    }
}
