using System.Data;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.WindowsClient.Schemas;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class RegisterForm : Form
    {
        private readonly IUsersService service;

        private JwtResponse Jwt;
        private bool IsInitialized = false;

        public RegisterForm(IUsersService service)
        {
            this.service = service;

            InitializeComponent();
            LoadGenderComboBox();

            IsInitialized = true;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

            var list = new AdminRegisterSchema
            {
                FullName = "abc"
            };

            adminRegisterSchemaBindingSource.DataSource = list;
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
            if (IsInitialized && cbRole.SelectedValue is not null)
            {
                Enum.TryParse<Role>(cbRole.SelectedValue.ToString(), out var result);
                MessageBox.Show($"Selected Gender: {result.GetDisplayName()}");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            adminRegisterSchemaBindingSource.EndEdit();
            ValidateChildren();
        }

        private void tbFullName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Validated");
        }
    }
}
