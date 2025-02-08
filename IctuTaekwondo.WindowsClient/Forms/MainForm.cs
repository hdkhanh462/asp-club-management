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
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Services.Account;

namespace IctuTaekwondo.WindowsClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly IAccountService accountService;
        private readonly IContainer appContainer;
        private readonly JwtResponse jwt;
        private UserFullDetailResponse userProfile;

        public MainForm(IContainer appContainer, JwtResponse jwt)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.jwt = jwt;
            this.accountService = appContainer.Resolve<IAccountService>();
        }

        private async void GetProfile()
        {
            var profile = await accountService.GetProfileAsync(jwt.Token);
            if (profile == null)
            {
                MessageBox.Show("Lỗi xác thực người dùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            userProfile = profile;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetProfile();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            var usersForm = new UsersForm(appContainer,jwt);
            usersForm.ShowDialog();
        }
    }
}
