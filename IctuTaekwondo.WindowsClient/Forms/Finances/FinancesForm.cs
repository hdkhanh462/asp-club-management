using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Achievements;
using IctuTaekwondo.Shared.Services.Finances;
using IctuTaekwondo.WindowsClient.Forms.Achievements;
using IctuTaekwondo.WindowsClient.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Forms.Finances
{
    public partial class FinancesForm : Form
    {
        private readonly IFinancesService service;
        private readonly IAccountService accountService;

        FinancesDetailForm detailForm;
        FinanceAddNewForm addNewForm;

        private JwtResponse Jwt;
        private bool IsAdmin;
        private bool IsManager;
        private bool IsMember;

        public FinancesForm(IFinancesService service, FinancesDetailForm detailForm, FinanceAddNewForm addNewForm, IAccountService accountService)
        {
            InitializeComponent();

            this.service = service;
            this.detailForm = detailForm;
            this.addNewForm = addNewForm;
            this.accountService = accountService;
        }

        private async void EventsForm_Load(object sender, EventArgs e)
        {
            var currentUser = await accountService.GetProfileAsync(Jwt.Token);

            if (currentUser == null)
            {
                MessageBox.Show("Lỗi xác thực tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            IsAdmin = currentUser.Roles.Contains(Role.Admin.ToString());
            IsManager = currentUser.Roles.Contains(Role.Manager.ToString());
            IsMember = currentUser.Roles.Contains(Role.Member.ToString());

            if (IsAdmin)
            {
                btnAddNew.Enabled = true;
            }

            var response = await service.GetAllAsync(Jwt.Token, 1, 999);

            dataGridView1.DataSource = response.Items;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                int.TryParse(id, out var result);

                detailForm.SetJwt(Jwt, result);
                detailForm.ShowDialog();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            addNewForm.SetJwt(Jwt);
            addNewForm.ShowDialog();
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
