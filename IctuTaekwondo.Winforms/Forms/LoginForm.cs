using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Winforms.Models;
using IctuTaekwondo.Winforms.Utils;

namespace IctuTaekwondo.Winforms.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IApiService api;
        private readonly MainForm mainForm;

        public LoginForm(IApiService api, MainForm mainForm)
        {
            InitializeComponent();
            loginBindingSource.DataSource = new LoginModel();
            
            this.api = api;
            this.mainForm = mainForm;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                var password = CredentialManager.RetrieveCredentials(out var userName);

                loginBindingSource.DataSource = new LoginModel
                {
                    Email = userName,
                    Password = password,
                    RememberMe = !string.IsNullOrEmpty(password)
                };
            }
            catch { }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            loginBindingSource.EndEdit();

            var model = loginBindingSource.Current as LoginModel;
            var response = await api.PostAsync<JwtResponse>("api/auth/login", model.ToStringContent());

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            mainForm.SetJwt(response.Data);
            mainForm.Show();
        }
    }
}
