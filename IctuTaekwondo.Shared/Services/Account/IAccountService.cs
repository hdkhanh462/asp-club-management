using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;

namespace IctuTaekwondo.Shared.Services.Account
{
    public interface IAccountService
    {
        Task<UserFullDetailResponse?> GetProfileAsync(string token);
        Task<UserFullDetailResponse?> UpdateProfileAsync(string token, UserUpdateSchema schema);
        Task<bool> ChangePasswordAsync(string token, ChangePasswordSchema schema);
        Task<UserResponse?> GetUser(string token);
    }
}
