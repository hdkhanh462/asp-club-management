using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Users;

namespace IctuTaekwondo.WindowsClient.Forms.Users
{
    public partial class UsersDetailForm : Form
    {
        private bool isEdit;
        private string? userId;
        private readonly IUsersService usersService;
        private readonly IContainer appContainer;
        private JwtResponse jwt;


        public UsersDetailForm(IContainer appContainer, JwtResponse jwt, bool isEdit, string? userId = null)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.jwt = jwt;
            this.usersService = appContainer.Resolve<IUsersService>();
            this.userId = userId;
            this.isEdit = isEdit;
        }

        private async void UsersDetailForm_Load(object sender, EventArgs e)
        {
            if (isEdit && !string.IsNullOrEmpty(userId))
            {
                var userDetail = await usersService.GetProfileByIdAsync(jwt.Token, userId);

                if (userDetail != null)
                {
                    tbId.Text = userDetail.Id;
                    tbFullName.Text = userDetail.FullName;
                    tbGender.Text = userDetail.Profile.Gender.GetDisplayName();
                    tbPhoneNumber.Text = userDetail.PhoneNumber;
                    tbJoinDate.Text = userDetail.Profile.JoinDate.ToString();
                    tbAddress.Text = userDetail.Profile.Address;
                    tbCurrentRank.Text = userDetail.Profile.CurrentRank.GetDisplayName();
                    tbDateOfBirth.Text = userDetail.Profile.DateOfBirth.ToString();


                    LoadAvatarImage(userDetail.AvatarUrl);
                }

                btnSave.Enabled = userDetail != null;
                btnDelete.Enabled = userDetail != null;
                btnRegister.Enabled = false;
            }

            btnRegister.Enabled = true;
        }

        private void LoadAvatarImage(string? avatarUrl)
        {
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                // Load the avatar image from the URL
                pictureBox1.ImageLocation = avatarUrl;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userId)) return;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                var response = await usersService.DeleteAsync(jwt.Token, userId);

                if (string.IsNullOrEmpty(response))
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show(response);
                }
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            //var userCreateSchema = new RegisterSchema
            //{
            //    PhoneNumber = tbPhoneNumber.Text,
            //    FullName = tbFullName.Text,

            //};

            //var response = await usersService.RegisterAsync(jwt.Token, userCreateSchema);

            //if (response != null)
            //{
            MessageBox.Show("Đăng ký thành công");
            //}
            //else
            //{
            //    MessageBox.Show("Đăng ký không thành công");
            //}
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userId)) return;

            var userUpdateSchema = new UserUpdateSchema
            {
                PhoneNumber = tbPhoneNumber.Text,
                FullName = tbFullName.Text,
                //AvatarUrl = pictureBox1.ImageLocation,
                Gender = (Gender)Enum.Parse(typeof(Gender), tbGender.Text),
                JoinDate = DateOnly.Parse(tbJoinDate.Text),
                Address = tbAddress.Text,
                CurrentRank = (RankName)Enum.Parse(typeof(RankName), tbCurrentRank.Text),
                DateOfBirth = DateOnly.Parse(tbDateOfBirth.Text)

            };

            var response = await usersService.UpdateAsync(jwt.Token, userId, userUpdateSchema);

            if (response != null)
            {
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công");
            }
        }
    }
}
