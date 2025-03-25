using System.Configuration;
using IctuTaekwondo.Shared;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Services.Finances;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IFinancesService financesService;
        private readonly IConfiguration configuration;

        public DashboardController(IFinancesService financesService, IConfiguration configuration)
        {
            this.financesService = financesService;
            this.configuration = configuration;
        }

        [Authorize]
        public async Task<IActionResult> Index([FromQuery] ReportRange? range = null)
        {
            var token = Request.Cookies[GlobalConst.CookieAuthTokenKey];

            var model = new DashboardViewModel();
            var apiUrl = configuration["ApiUrl"];

            if (!string.IsNullOrEmpty(apiUrl))
                model.ApiUrl = apiUrl;

            if (range.HasValue)
            {
                (DateTime StartDate, DateTime EndDate) current;
                (DateTime StartDate, DateTime EndDate) pass;
                current = ReportHelper.GetCurrentDates(range.Value);
                pass = ReportHelper.GetPastPeriodDates(range.Value);
                var currentReport = await financesService.GetReportAsync(token, current.StartDate,current.EndDate);
                var passReport = await financesService.GetReportAsync(token, pass.StartDate, pass.EndDate);
                model.Current = currentReport;
                model.Pass = passReport;
                model.Range = range.Value;
                return View(model);
            }

            var report = await financesService.GetReportAsync(token, null, null);
            model.Current = report;

            return View(model);
        }
    }
}
