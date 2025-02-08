using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.WindowsClient.Views
{
    public partial class MainViewTest : Form, IMainView
    {
        public MainViewTest()
        {
            InitializeComponent();
            InitEvents();
        }

        private void InitEvents()
        {
            button1.Click += delegate { ShowUsersViewEvent?.Invoke(this, EventArgs.Empty); };

        }

        public UserFullDetailResponse User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler LogoutEvent;
        public event EventHandler ShowLoginViewEvent;
        public event EventHandler ShowUsersViewEvent;
    }
}
