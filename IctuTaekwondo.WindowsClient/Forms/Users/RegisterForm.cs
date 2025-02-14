using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Users;

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class RegisterForm : Form
    {
        private readonly IUsersService service;

        private JwtResponse Jwt;

        public RegisterForm(IUsersService service)
        {
            this.service = service;

            InitializeComponent();
            LoadComboBoxs();
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

            var list = new AdminRegisterSchema();

            adminRegisterSchemaBindingSource.DataSource = list;
        }

        private void LoadComboBoxs()
        {
            var values = Enum
                .GetValues(typeof(Role))
                .Cast<Role>()
                .Select(g => new { Value = g, DisplayName = g.GetDisplayName() })
                .ToList();

            cbRole.DataSource = values;
            cbRole.DisplayMember = "DisplayName";
            cbRole.ValueMember = "Value";
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.SelectedValue is not null)
            {
                var isRole = Enum.TryParse<Role>(cbRole.SelectedValue.ToString(), out var result);

                if (isRole && adminRegisterSchemaBindingSource.Current is AdminRegisterSchema schema)
                    schema.Roles = [result];
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            adminRegisterSchemaBindingSource.EndEdit();

            if (adminRegisterSchemaBindingSource.Current is AdminRegisterSchema schema)
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(schema);

                if (!Validator.TryValidateObject(schema, context, results, true))
                {
                    MessageBox.Show(string.Join("\n", results.Select(x => x.ErrorMessage)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var response = await service.RegisterAsync(Jwt.Token, schema);
                if (response.Any())
                {
                    MessageBox.Show(string.Join("\n", response.SelectMany(x => x.Value)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
