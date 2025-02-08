using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using IctuTaekwondo.Shared.Responses.Auth;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Services.Users;

namespace IctuTaekwondo.WindowsClient.Forms
{
    public partial class UsersForm : Form
    {
        private readonly IUsersService usersService;
        private readonly IContainer appContainer;
        private JwtResponse jwt;

        public UsersForm(IContainer appContainer, JwtResponse jwt)
        {
            InitializeComponent();
            this.appContainer = appContainer;
            this.jwt = jwt;
            this.usersService = appContainer.Resolve<IUsersService>();
        }

        private async void UsersForm_Load(object sender, EventArgs e)
        {
            var response = await usersService.GetAllAsync(jwt.Token, 1, 999);

            dataGridView1.DataSource = response.Items;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var userId = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                var userDetail = await usersService.GetProfileByIdAsync(jwt.Token, userId);
                if (userDetail != null)
                {
                    tbPhoneNumber.Text = userDetail.PhoneNumber;
                    tbFullName.Text = userDetail.FullName;
                    tbCurrentRank.Text = userDetail.Profile.CurrentRank.ToString();
                    tbAddress.Text = userDetail.Profile.Address;
                    tbDateOfBirth.Text = userDetail.Profile.DateOfBirth.ToString();
                    tbGender.Text = userDetail.Profile.Gender.ToString();
                    tbJoinDate.Text = userDetail.Profile.JoinDate.ToString();
                    tbId.Text = userDetail.Id;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tbPhoneNumber.Text = string.Empty;
            tbFullName.Text = string.Empty;
            tbCurrentRank.Text = string.Empty;
            tbAddress.Text = string.Empty;
            tbDateOfBirth.Text = string.Empty;
            tbGender.Text = string.Empty;
            tbJoinDate.Text = string.Empty;
            tbId.Text = string.Empty;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var userUpdateSchema = new UserUpdateSchema
            {
                PhoneNumber = tbPhoneNumber.Text,
                FullName = tbFullName.Text,
                AvatarUrl = null
            };

            if (tbId.Text != string.Empty)
            {
                var response = await usersService.UpdateAsync(jwt.Token, tbId.Text, userUpdateSchema);

                if (response != null)
                {
                    MessageBox.Show("Cập nhật thành công");
                    var usersResponse = await usersService.GetAllAsync(jwt.Token, 1, 999);
                    dataGridView1.DataSource = usersResponse.Items;
                }
                else
                {
                    MessageBox.Show("Cập nhật không thành công.");
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var userId = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var response = await usersService.DeleteAsync(jwt.Token, userId);

                    if (response != null)
                    {
                        MessageBox.Show("Xóa thành công");
                        var usersResponse = await usersService.GetAllAsync(jwt.Token, 1, 999);
                        dataGridView1.DataSource = usersResponse.Items;
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa");
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
            //    var usersResponse = await usersService.GetAllAsync(jwt.Token, 1, 999);
            //    dataGridView1.DataSource = usersResponse.Items;
            //}
            //else
            //{
            //    MessageBox.Show("Đăng ký không thành công");
            //}
        }
    }
}
