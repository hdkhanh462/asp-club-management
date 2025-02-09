using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.Shared.Services.Finances;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    public partial class FinancesForm : Form
    {
        private readonly IFinancesService service;
        private readonly IContainer appContainer;
        private JwtResponse jwt;

        public FinancesForm(IContainer appContainer, JwtResponse jwt)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.jwt = jwt;
            this.service = appContainer.Resolve<IFinancesService>();
        }

        private async void EventsForm_Load(object sender, EventArgs e)
        {
            var response = await service.GetAllAsync(jwt.Token, 1, 999);

            dataGridView1.DataSource = response.Items;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                int.TryParse(id, out var result);

                var userDetailForm = new FinancesDetailForm(appContainer, jwt, true, result);
                userDetailForm.ShowDialog();
            }
        }

        private void btnAđNew_Click(object sender, EventArgs e)
        {
            var userDetailForm = new FinancesDetailForm(appContainer, jwt, false);
            userDetailForm.ShowDialog();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = tbSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var query = new QueryBuilder
                {
                    { "search", searchTerm }
                };
                var response = await service.GetAllAsync(jwt.Token, 1, 999, query);
                dataGridView1.DataSource = response.Items;
            }
        }
    }
}
