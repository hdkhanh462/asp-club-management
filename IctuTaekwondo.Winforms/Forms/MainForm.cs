using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Winforms.Forms.Users;
using IctuTaekwondo.Winforms.Models.Users;
using IctuTaekwondo.Winforms.Utils;

namespace IctuTaekwondo.Winforms.Forms
{
    public partial class MainForm : Form
    {
        private readonly IApiService api;
        private readonly UsersForm usersForm;
        //private readonly EventsForm eventsForm;
        //private readonly AchievementsForm achievementsForm;
        //private readonly FinancesForm financesForm;
        
        private JwtResponse Jwt;

        public MainForm(IApiService api, UsersForm usersForm)
        {
            InitializeComponent();

            this.api = api;
            this.usersForm = usersForm;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            api.SetAuthorizationHeader(Jwt.Token);

            var response = await api.GetAsync<UserWithProfileResponse>("api/account/profile");
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            usersForm.SetJwt(Jwt);
            usersForm.Show();
        }

        private void btnEventsManager_Click(object sender, EventArgs e)
        {
        }

        private void btnAchivements_Click(object sender, EventArgs e)
        {
        }

        private void btnFinances_Click(object sender, EventArgs e)
        {
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }
    }
}
