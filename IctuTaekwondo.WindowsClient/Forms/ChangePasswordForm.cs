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
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Services.Account;

namespace IctuTaekwondo.WindowsClient.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly IAccountService service;

        private JwtResponse Jwt;

        public ChangePasswordForm(IAccountService service)
        {
            InitializeComponent();
            this.service = service;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }


        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            changePasswordSchemaBindingSource.DataSource = new ChangePasswordSchema();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (changePasswordSchemaBindingSource.Current is ChangePasswordSchema schema)
            {
                var response = await service.ChangePasswordAsync(Jwt.Token, schema);

                if (response)
                {
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                MessageBox.Show("Đổi mật khẩu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
