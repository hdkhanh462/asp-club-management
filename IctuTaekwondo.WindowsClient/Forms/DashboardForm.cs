using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Finances;

namespace IctuTaekwondo.WindowsClient.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly IFinancesService service;
        private JwtResponse Jwt;

        public DashboardForm(IFinancesService service)
        {
            InitializeComponent();
            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            var response = await service.GetReportAsync(Jwt.Token, null, null);
            lbTotal.Text = response
                .Sum(report => report.Type == TransactionType.Income
                ? report.TotalAmount
                : -report.TotalAmount).ToString("N0") + "đ";

            lbIncome.Text = response
                .Where(report => report.Type == TransactionType.Income)
                .Sum(report => report.TotalAmount).ToString("N0") + "đ";

            lbExpense.Text = response
                .Where(report => report.Type == TransactionType.Expense)
                .Sum(report => report.TotalAmount).ToString("N0") + "đ";

            //var response = await service.GetBarChart();

            //chart1.Series.Clear();
            //chart1.ChartAreas.Clear();

            //var chartArea = new ChartArea();
            //chart1.ChartAreas.Add(chartArea);

            //foreach (var seriesData in barChartData.series)
            //{
            //    var series = new Series
            //    {
            //        Name = seriesData.name,
            //        Color = ColorTranslator.FromHtml(seriesData.color),
            //        ChartType = SeriesChartType.Bar
            //    };

            //    series.Points.DataBindXY(barChartData.categories, seriesData.data);
            //    chart1.Series.Add(series);
            //}

            //chart1.Invalidate();
        }
    }
}
