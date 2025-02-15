using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.WindowsClient.Utils;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class RegisterForm : Form
    {
        private readonly IUsersService service;

        private JwtResponse Jwt;

        public RegisterForm(IUsersService service)
        {
            InitializeComponent();
            Helpers.LoadComboBoxs<Role>(cbRole);

            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            adminRegisterSchemaBindingSource.DataSource = new AdminRegisterSchema
            {
                Roles = [Role.Member]
            };
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
                    return;
                }

                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                adminRegisterSchemaBindingSource.DataSource = new AdminRegisterSchema();
                if (pbAvatar.Image != null)
                    pbAvatar.Image = null;
            }
        }

        private void pbAvatar_Click(object sender, EventArgs e)
        {
            if (adminRegisterSchemaBindingSource.Current is AdminRegisterSchema schema)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.Title = "Chọn avatar";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pbAvatar.Image = Image.FromFile(openFileDialog.FileName);
                        IFormFile formFile = Helpers.ConvertToIFormFile(openFileDialog.FileName);
                        schema.Avatar = formFile;
                    }
                }
            }
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            adminRegisterSchemaBindingSource.Clear();
        }
    }
}
