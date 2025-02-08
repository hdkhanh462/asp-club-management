

using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Auth;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Forms
{

    public partial class LoginForm : Form
    {
        private readonly IAuthService authService;
        private readonly IContainer appContainer;
        private JwtResponse jwt;

        public LoginForm(IContainer appContainer)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.authService = appContainer.Resolve<IAuthService>();
        }

        private void RetrieveCredentials()
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

        private void ShowMainForm()
        {
            var mainForm = new MainForm(appContainer, jwt);
            Hide();
            
            mainForm.Show();
        }

        private void LoginForms_Load(object sender, EventArgs e)
        {
            RetrieveCredentials();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var response = await this.authService.LoginAsync(new LoginSchema
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

            jwt = response;

            ShowMainForm();
        }
    }
}
