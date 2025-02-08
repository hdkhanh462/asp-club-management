using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using IctuTaekwondo.Shared.Utils;
using IctuTaekwondo.WindowsClient.Services;
using IctuTaekwondo.WindowsClient.Views;

namespace IctuTaekwondo.WindowsClient.Presenters
{

    public class UserPresenter
    {
        private string _token;
        private IUsersView _usersView;
        private IUsersService _usersService;
        private BindingSource _bindingSource;

        public UserPresenter(string token, IUsersView usersView, IUsersService usersService)
        {
            _token = token;
            _usersView = usersView;
            _usersService = usersService;

            _bindingSource = new BindingSource();
            _usersView.SetBindingSource(_bindingSource);

            SetPaginator();
            _usersView.Show();
        }

        private async void SetPaginator()
        {
            var paginator = await _usersService.GetAllSync(_token);
            _bindingSource.DataSource = paginator.Items;
        }

        public async Task<bool> RegisterUser(AdminRegisterSchema schema)
        {
            return await _usersService.RegisterAsync(_token, schema);
        }

        public async Task<bool> DeleteUser(string id)
        {
            return await _usersService.DeleteAsync(_token, id);
        }

        public async Task<bool> SetUserPassword(string id, AdminSetPasswordSchema schema)
        {
            return await _usersService.SetPasswordAsync(_token, id, schema);
        }

        public async Task<bool> UpdateUserRoles(string id, UpdateRolesSchema schema)
        {
            return await _usersService.UpdateRolesAsync(_token, id, schema);
        }

        public async Task<UserFullDetailResponse?> UpdateUser(AdminUserUpdateSchema schema)
        {
            return await _usersService.UpdateAsync(_token, schema);
        }

        public async Task<UserFullDetailResponse?> FindUserById(string id)
        {
            return await _usersService.FindByIdAsync(_token, id);
        }
    }
}
