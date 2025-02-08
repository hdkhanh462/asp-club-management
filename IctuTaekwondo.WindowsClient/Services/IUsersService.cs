using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Schemas.Account;
using IctuTaekwondo.Shared.Schemas.Auth;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.WindowsClient.Services
{
    public interface IUsersService
    {
        Task<bool> RegisterAsync(string token, AdminRegisterSchema schema);
        Task<bool> DeleteAsync(string token, string id);
        Task<bool> SetPasswordAsync(string token, string id, AdminSetPasswordSchema schema);
        Task<bool> UpdateRolesAsync(string token, string id, UpdateRolesSchema schema);
        Task<UserFullDetailResponse?> UpdateAsync(string token, AdminUserUpdateSchema schema);
        Task<UserFullDetailResponse?> FindByIdAsync(string token, string id);
        Task<PaginationResponse<UserResponse>> GetAllSync(string token, QueryBuilder? query = null);
    }
}
