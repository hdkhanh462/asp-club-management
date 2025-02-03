using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IctuTaekwondo.Shared.Utils
{
    public static class ReportHelper
    {
        public static object GetYearly(short year)
        {
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = new DateTime(year, 12, 31);

            return new
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }

        public static object GetQuarterly(short year, short quarter)
        {
            int startMonth = (quarter - 1) * 3 + 1;
            DateTime startDate = new DateTime(year, startMonth, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            return new
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }
    
        public static object GetMonthly(short year, short month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            return new
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}
