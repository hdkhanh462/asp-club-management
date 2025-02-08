using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.WindowsClient.Services
{

    public interface IAccountService
    {
        Task<UserFullDetailResponse?> GetProfileAsync(string token);
        Task<bool> UpdateProfileAsync(UserFullDetailResponse userProfile, string token);
        Task<bool> ChangePasswordAsync(string currentPassword, string newPassword, string token);
        Task<bool> DeleteAccountAsync(string userId, string token);
    }
}
