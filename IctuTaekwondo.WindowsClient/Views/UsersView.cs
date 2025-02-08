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

    public partial class UsersView : Form, IUsersView
    {
        public UsersView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        public UserFullDetailResponse User
        {
            get => new UserFullDetailResponse
            {
                UserName = materialTextBoxEdit1.Text,
                Email = materialTextBoxEdit2.Text,
                PhoneNumber = materialTextBoxEdit3.Text,
                FullName = materialTextBoxEdit4.Text,
                // Add other properties as needed
            };
            set
            {
                materialTextBoxEdit1.Text = value.UserName;
                materialTextBoxEdit2.Text = value.Email;
                materialTextBoxEdit3.Text = value.PhoneNumber;
                materialTextBoxEdit4.Text = value.FullName;
                // Set other properties as needed
            }
        }

        public string SearchValue
        {
            get => materialTextBoxEdit1.Text;
            set => materialTextBoxEdit1.Text = value;
        }

        public bool IsEdit { get; set; }

        public event EventHandler SearchEvent;
        public event EventHandler RegisterEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetBindingSource(BindingSource bindingSource)
        {
            dataGridView1.DataSource = bindingSource;
        }

        private static UsersView _instance;
        public static UsersView GetInstance()
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new UsersView();
            else
            {
                if (_instance.WindowState == FormWindowState.Minimized)
                    _instance.WindowState = FormWindowState.Normal;
                _instance.BringToFront();
            }

            return _instance;
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            btnRegister.Click += delegate { RegisterEvent?.Invoke(this, EventArgs.Empty); };
            materialButton1.Click += delegate { SaveEvent?.Invoke(this, EventArgs.Empty); };
            materialButton2.Click += delegate { CancelEvent?.Invoke(this, EventArgs.Empty); };
            dataGridView1.DoubleClick += delegate { EditEvent?.Invoke(this, EventArgs.Empty); };
        }
    }
}
