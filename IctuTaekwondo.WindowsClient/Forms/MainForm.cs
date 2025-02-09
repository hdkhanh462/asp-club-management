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
using IctuTaekwondo.WindowsClient.Forms.Achievements;
using IctuTaekwondo.WindowsClient.Forms.Events;
using IctuTaekwondo.WindowsClient.Forms.Finances;
using IctuTaekwondo.WindowsClient.Forms.Users;

namespace IctuTaekwondo.WindowsClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly IAccountService accountService;
        private readonly UsersForm usersForm;
        private readonly EventsForm eventsForm;
        private readonly AchievementsForm achievementsForm;
        private readonly FinancesForm financesForm;

        private JwtResponse Jwt;
        private UserFullDetailResponse UserProfile;

        public MainForm(IAccountService accountService, UsersForm usersForm, EventsForm eventsForm, AchievementsForm achievementsForm, FinancesForm financesForm)
        {
            this.accountService = accountService;
            this.usersForm = usersForm;
            this.eventsForm = eventsForm;
            this.achievementsForm = achievementsForm;
            this.financesForm = financesForm;

            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var profile = await accountService.GetProfileAsync(Jwt.Token);
            if (profile == null)
            {
                MessageBox.Show("Lỗi xác thực người dùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            usersForm.SetJwt(Jwt);
            usersForm.ShowDialog();
        }

        private void btnEventsManager_Click(object sender, EventArgs e)
        {
            eventsForm.SetJwt(Jwt);
            eventsForm.ShowDialog();
        }

        private void btnAchivements_Click(object sender, EventArgs e)
        {
            achievementsForm.SetJwt(Jwt);
            achievementsForm.ShowDialog();
        }

        private void btnFinances_Click(object sender, EventArgs e)
        {
            financesForm.SetJwt(Jwt);
            financesForm.ShowDialog();
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }
    }
}
