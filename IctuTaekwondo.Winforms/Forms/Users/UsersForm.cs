using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IctuTaekwondo.Winforms.Models.Users;
using IctuTaekwondo.Winforms.Responses;
using IctuTaekwondo.Winforms.Utils;
using Microsoft.AspNetCore.Http.Extensions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IctuTaekwondo.Winforms.Forms.Users
{
    public partial class UsersForm : Form
    {
        private readonly IApiService api;
        private readonly UsersDetailForm detailForm;
        private readonly RegisterForm registerForm;

        private JwtResponse Jwt;

        public UsersForm(IApiService api, UsersDetailForm detailForm, RegisterForm registerForm)
        {
            InitializeComponent();

            this.api = api;
            this.detailForm = detailForm;
            this.registerForm = registerForm;
        }

        private  async void UsersForm_Load(object sender, EventArgs e)
        {
            api.SetAuthorizationHeader(Jwt.Token);

            var query =  new QueryBuilder();
            var response = await api.GetAsync<PaginationResponse<UserResponse>>($"api/users{query.ToQueryString()}");
            
            userResponseBindingSource.DataSource = response.Data.Items ?? new List<UserResponse>();
        }

        internal void SetJwt(JwtResponse jwt)
        {
            Jwt = jwt;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            registerForm.SetJwt(Jwt);
            registerForm.Show();
        }

        private async void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (userResponseBindingSource.Current is UserResponse selectedUser && !string.IsNullOrEmpty(selectedUser.Id))
            {
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    api.SetAuthorizationHeader(Jwt.Token);
                    var response = await api.DeleteAsync<ApiResponse<object>>($"api/users/{selectedUser.Id}");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        userResponseBindingSource.RemoveCurrent();
                        MessageBox.Show("Xóa người dùng thành công.");
                    }
                    else
                    {
                        MessageBox.Show(response.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có người dùng nào được chọn.");
            }
        }
    }
}
