using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Services.Account;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.WindowsClient.Utils;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.WindowsClient.Forms
{
    public partial class AccountForm : Form
    {
        private readonly IAccountService service;
        private readonly ChangePasswordForm changePasswordForm;

        private JwtResponse Jwt;
        private UserFullDetailResponse CurrentUser;

        public AccountForm(IAccountService service, ChangePasswordForm changePasswordForm)
        {
            InitializeComponent();

            Helpers.LoadComboBoxs<RankName>(cbCurrentRank);
            Helpers.LoadComboBoxs<Gender>(cbGender);
            this.service = service;
            this.changePasswordForm = changePasswordForm;
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private async void AccountForm_Load(object sender, EventArgs e)
        {
            var response = await service.GetProfileAsync(Jwt.Token);

            if (response == null)
            {
                MessageBox.Show("Lỗi xác thực tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CurrentUser = response;
            tbId.Text = response.Id;

            userUpdateSchemaBindingSource.DataSource = new UserUpdateSchema
            {
                FullName = response.FullName,
                PhoneNumber = response.PhoneNumber,
                JoinDate = response.Profile.JoinDate,
                Address = response.Profile.Address,
                DateOfBirth = response.Profile.DateOfBirth,
                AvatarUrl = response.AvatarUrl,
                CurrentRank = response.Profile.CurrentRank,
                Gender = response.Profile.Gender,
            };

            if (response.Profile.CurrentRank is not null)
                cbCurrentRank.SelectedValue = response.Profile.CurrentRank;
            if (response.Profile.Gender is not null)
                cbGender.SelectedValue = response.Profile.Gender;
        }

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            userUpdateSchemaBindingSource.Clear();
        }

        private void pbAvatar_Click(object sender, EventArgs e)
        {
            if (userUpdateSchemaBindingSource.Current is UserUpdateSchema schema)
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

        private async void btnSave_Click(object sender, EventArgs e)
        {
            userUpdateSchemaBindingSource.EndEdit();

            if (userUpdateSchemaBindingSource.Current is UserUpdateSchema schema)
            {
                if (!Helpers.IsValidSchema(schema)) return;

                var response = await service.UpdateProfileAsync(Jwt.Token, schema);

                if (response == null)
                {
                    MessageBox.Show("Cập nhật không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                userUpdateSchemaBindingSource.DataSource = new UserUpdateSchema
                {
                    FullName = response.FullName,
                    PhoneNumber = response.PhoneNumber,
                    JoinDate = response.Profile.JoinDate,
                    Address = response.Profile.Address,
                    DateOfBirth = response.Profile.DateOfBirth,
                    AvatarUrl = response.AvatarUrl,
                    CurrentRank = response.Profile.CurrentRank,
                    Gender = response.Profile.Gender,
                };
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            changePasswordForm.SetJwt(Jwt);
            changePasswordForm.ShowDialog();
        }
    }
}
