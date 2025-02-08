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
using ReaLTaiizor.Forms;

namespace IctuTaekwondo.WindowsClient.Views
{

    public partial class MainView : MaterialForm, IMainView
    {
        private IPresenter _presenter;
        private static MainView _instance;
        private UserFullDetailResponse _user;

        public MainView()
        {
            InitializeComponent();
            InitEvents();
        }

        private void InitEvents()
        {
            drawerTabControl.SelectedIndexChanged += delegate { ShowUsersViewEvent?.Invoke(this, EventArgs.Empty); };
        }

        public UserFullDetailResponse User { get => _user; set => _user = value; }

        public event EventHandler LogoutEvent;
        public event EventHandler ShowLoginViewEvent;
        public event EventHandler ShowUsersViewEvent;

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

        public void SetPresenter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _presenter?.Run();
        }

        public void OpenAnotherView()
        {
            var anotherView = _presenter.Resolve<IAnotherView>() as Form;
            anotherView?.Show();
        }
    }
}
