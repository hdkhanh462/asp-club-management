using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Schemas.Account;
using Microsoft.AspNetCore.Http.Extensions;

namespace IctuTaekwondo.Shared.Services.Users
{
    public interface IUsersService
    {
        Task<PaginationResponse<UserResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null);
        Task<UserFullDetailResponse?> GetProfileByIdAsync(string token, string id);
        Task<UserFullDetailResponse?> UpdateAsync(string token, string id, UserUpdateSchema schema);
        Task<string?> DeleteAsync(string token, string id);
        Task<bool> SetPasswordAsync(string token, string id, AdminSetPasswordSchema schema);
        Task<bool> UpdateRolesAsync(string token, string id, UpdateRolesSchema schema);
    }
}
