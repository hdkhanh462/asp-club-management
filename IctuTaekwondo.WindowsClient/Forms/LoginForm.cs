

using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Auth;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Forms
{

    public partial class LoginForm : Form
    {
        private readonly IAuthService service;
        private readonly MainForm mainForm;

        public LoginForm(IAuthService service, MainForm mainForm)
        {
            this.service = service;
            this.mainForm = mainForm;

            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                var password = CredentialManager.RetrieveCredentials(out var userName);
                tbEmail.Text = userName;

                if (!string.IsNullOrEmpty(password))
                {
                    tbPassword.Text = password;
                    cbRememberMe.Checked = true;
                }
            }
            catch { }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var response = await this.service.LoginAsync(new LoginSchema
            {
                Email = tbEmail.Text,
                Password = tbPassword.Text,
                RememberMe = cbRememberMe.Checked,
            });

            if (response == null)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Hide();
            mainForm.SetJwt(response);
            mainForm.Show();
        }
    }
}
