using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Users;
using IctuTaekwondo.WindowsClient.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class UsersDetailForm : Form
    {
        private readonly IUsersService usersService;

        private JwtResponse Jwt;
        private string? Id;

        public UsersDetailForm(IUsersService usersService)
        {
            InitializeComponent();

            Helpers.LoadComboBoxs<RankName>(cbCurrentRank);
            Helpers.LoadComboBoxs<Gender>(cbGender);

            this.usersService = usersService;
        }

        private async void UsersDetailForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var userDetail = await usersService.GetProfileByIdAsync(Jwt.Token, Id);

                if (userDetail != null)
                {
                    tbId.Text = userDetail.Id;

                    userUpdateSchemaBindingSource.DataSource = new UserUpdateSchema
                    {
                        FullName = userDetail.FullName,
                        PhoneNumber = userDetail.PhoneNumber,
                        JoinDate = userDetail.Profile.JoinDate,
                        Address = userDetail.Profile.Address,
                        DateOfBirth = userDetail.Profile.DateOfBirth,
                        AvatarUrl = userDetail.AvatarUrl
                    };

                    if (userDetail.Profile.CurrentRank is not null)
                        cbCurrentRank.SelectedValue = userDetail.Profile.CurrentRank;
                    if (userDetail.Profile.Gender is not null)
                        cbGender.SelectedValue = userDetail.Profile.Gender;
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Id)) return;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var response = await usersService.DeleteAsync(Jwt.Token, Id);
                if (string.IsNullOrEmpty(response))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show(response, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            userUpdateSchemaBindingSource.EndEdit();

            if (userUpdateSchemaBindingSource.Current is UserUpdateSchema schema)
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(schema);

                if (!Validator.TryValidateObject(schema, context, results, true))
                {
                    MessageBox.Show(string.Join("\n", results.Select(x => x.ErrorMessage)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var response = await usersService.UpdateAsync(Jwt.Token, Id, schema);

                if (response != null)
                {
                    tbId.Text = response.Id;

                    userUpdateSchemaBindingSource.Clear();
                    userUpdateSchemaBindingSource.DataSource = new UserUpdateSchema
                    {
                        FullName = response.FullName,
                        PhoneNumber = response.PhoneNumber,
                        JoinDate = response.Profile.JoinDate,
                        Address = response.Profile.Address,
                        DateOfBirth = response.Profile.DateOfBirth,
                        AvatarUrl = response.AvatarUrl
                    };

                    if (response.Profile.CurrentRank is not null)
                        cbCurrentRank.SelectedValue = response.Profile.CurrentRank;
                    if (response.Profile.Gender is not null)
                        cbGender.SelectedValue = response.Profile.Gender;

                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Cập nhật không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void SetJwt(JwtResponse jwt, string? id)
        {
            Jwt = jwt;
            Id = id;
        }

        private void cbCurrentRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCurrentRank.SelectedValue is not null)
            {
                var isRank = Enum.TryParse<RankName>(cbCurrentRank.SelectedValue.ToString(), out var result);

                if (isRank && userUpdateSchemaBindingSource.Current is UserUpdateSchema schema)
                    schema.CurrentRank = result;
            }
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
                        IFormFile formFile = ConvertToIFormFile(openFileDialog.FileName);
                        schema.Avatar = formFile;
                    }
                }
            }
        }

        private IFormFile ConvertToIFormFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "Image", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/" + Path.GetExtension(filePath).TrimStart('.')
            };

            return formFile;
        }

        private void UsersDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            userUpdateSchemaBindingSource.Clear();
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGender.SelectedValue is not null)
            {
                var isGender = Enum.TryParse<Gender>(cbGender.SelectedValue.ToString(), out var result);

                if (isGender && userUpdateSchemaBindingSource.Current is UserUpdateSchema schema)
                    schema.Gender = result;
            }
        }
    }
}
