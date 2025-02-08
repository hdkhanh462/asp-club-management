using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.WindowsClient.Utils;

namespace IctuTaekwondo.WindowsClient.Views
{

    public partial class LoginView : Form, ILoginView
    {
        private IPresenter _presenter;
        private static LoginView? _instance = null;

        public LoginView()
        {
            InitializeComponent();
            InitEvents();
        }

        public void SetPresenter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _presenter?.Run();
        }

        public static LoginView GetInstance()
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new LoginView();
            else
            {
                if (_instance.WindowState == FormWindowState.Minimized)
                    _instance.WindowState = FormWindowState.Normal;
                _instance.BringToFront();
            }

            return _instance;
        }

        private void InitEvents()
        {
            btnLogin.Click += delegate { LoginEvent?.Invoke(this, EventArgs.Empty); };
            tbPassword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    LoginEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        public string EmailValue { get => tbUsername.Text; set => tbUsername.Text = value; }
        public string PasswordValue { get => tbPassword.Text; set => tbPassword.Text = value; }
        public bool RememberMeValue { get => cbRememberMe.Checked; set => cbRememberMe.Checked = value; }

        public event EventHandler LoginEvent;

        
    }
}
