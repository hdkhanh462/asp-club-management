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
using ReaLTaiizor.Forms;

namespace IctuTaekwondo.WindowsClient.Views
{

    public partial class MainView : MaterialForm, IMainView
    {
        private UserFullDetailResponse _user;

        public MainView()
        {
            InitializeComponent();
            InitEvents();
        }

        private void InitEvents()
        {
            drawerTabControl.SelectedIndexChanged += delegate { ShowLoginViewEvent?.Invoke(this, EventArgs.Empty); };
        }

        public UserFullDetailResponse User { get => _user; set => _user = value; }

        public event EventHandler LogoutEvent;
        public event EventHandler ShowLoginViewEvent;
        public event EventHandler ShowUsersViewEvent;

        private static MainView _instance;
        public static MainView GetInstance()
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new MainView();
            else
            {
                if (_instance.WindowState == FormWindowState.Minimized)
                    _instance.WindowState = FormWindowState.Normal;
                _instance.BringToFront();
            }

            return _instance;
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            LogoutEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
