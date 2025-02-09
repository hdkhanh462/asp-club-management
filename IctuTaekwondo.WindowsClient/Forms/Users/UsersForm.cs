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

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class UsersForm : Form
    {
        private readonly IUsersService usersService;
        private readonly IContainer appContainer;
        private JwtResponse jwt;

        public UsersForm(IContainer appContainer, JwtResponse jwt)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.jwt = jwt;
            this.usersService = appContainer.Resolve<IUsersService>();
        }

        private async void UsersForm_Load(object sender, EventArgs e)
        {
            var response = await usersService.GetAllAsync(jwt.Token, 1, 999);

            dataGridView1.DataSource = response.Items;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var userId = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                var userDetailForm = new UsersDetailForm(appContainer,jwt,true,userId);
                userDetailForm.ShowDialog();
            }
        }
    }
}
