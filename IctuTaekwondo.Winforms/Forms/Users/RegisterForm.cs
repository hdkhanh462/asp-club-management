using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using IctuTaekwondo.Winforms.Enums;
using IctuTaekwondo.Winforms.Utils;

namespace IctuTaekwondo.Winforms.Forms.Users
{
    public partial class RegisterForm : Form
    {
        private readonly IApiService api;

        private JwtResponse Jwt;
        private bool IsInitialized = false;

        public RegisterForm(IApiService api)
        {
            InitializeComponent();

            this.api = api;

            LoadGenderComboBox();
            IsInitialized = true;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadGenderComboBox()
        {
            var values = Enum.GetValues(typeof(Role))
                                   .Cast<Role>()
                                   .Select(g => new { Value = g, DisplayName = g.GetDisplayName() })
                                   .ToList();

            cbRole.DataSource = values;
            cbRole.DisplayMember = "DisplayName";
            cbRole.ValueMember = "Value";
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInitialized && cbRole.SelectedValue != null)
            {
                Enum.TryParse<Role>(cbRole.SelectedValue.ToString(), out var result);
                MessageBox.Show($"Selected Gender: {result.GetDisplayName()}");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
        }
    }
}
