using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Users;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class UsersForm : Form
    {
        private readonly IUsersService usersService;
        private readonly UsersDetailForm detailForm;
        private readonly RegisterForm registerForm;

        private JwtResponse Jwt;

        public UsersForm(IUsersService usersService, UsersDetailForm detailForm, RegisterForm registerForm)
        {
            this.usersService = usersService;
            this.detailForm = detailForm;
            this.registerForm = registerForm;

            InitializeComponent();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var query = new QueryBuilder();
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
                query.Add("search", tbSearch.Text);

            var response = await usersService.GetAllAsync(Jwt.Token, 1, 999, query);

            dataGridView1.DataSource = response.Items;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var userId = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                detailForm.SetJwt(Jwt, userId);
                detailForm.ShowDialog();
            }
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            registerForm.SetJwt(Jwt);
            registerForm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
