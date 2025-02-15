using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Events;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Forms.Events
{
    public partial class EventsForm : Form
    {
        private readonly IEventsService service;
        private readonly EventsDetailForm detailForm;
        private readonly EventAddNewForm addNewForm;
        private readonly IAccountService accountService;


        private JwtResponse Jwt;
        private bool IsAdmin;
        private bool IsManager;
        private bool IsMember;

        public EventsForm(IEventsService service, EventsDetailForm detailForm, EventAddNewForm addNewForm, IAccountService accountService)
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

            if (IsAdmin || IsManager)
            {
                btnAddNew.Enabled = true;
            }

            LoadItems();
        }

        private async void LoadItems()
        {
            var query = new QueryBuilder();
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
                query.Add("name", tbSearch.Text);

            var response = await service.GetAllAsync(Jwt.Token, 1, 999, query);

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

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadItems();
        }
    }
}
