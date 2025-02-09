using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Achievements;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Forms.Achievements
{
    public partial class AchievementsForm : Form
    {
        private readonly IAchievementsService service;
        AchievementsDetailForm detailForm;

        private JwtResponse Jwt;

        public AchievementsForm(IAchievementsService service, AchievementsDetailForm detailForm)
        {
            this.service = service;
            this.detailForm = detailForm;
            
            InitializeComponent();
        }

        private async void EventsForm_Load(object sender, EventArgs e)
        {
            var response = await service.GetAllAsync(Jwt.Token, 1, 999);

            dataGridView1.DataSource = response.Items;
        }

        private  void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                int.TryParse(id, out var result);

                detailForm.SetJwt(Jwt, true, result);
                detailForm.ShowDialog();
            }
        }

        private void btnAđNew_Click(object sender, EventArgs e)
        {
            detailForm.SetJwt(Jwt, false, null);
            detailForm.ShowDialog();
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
                var response = await service.GetAllAsync(Jwt.Token, 1, 999, query);
                dataGridView1.DataSource = response.Items;
            }
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }
    }
}
