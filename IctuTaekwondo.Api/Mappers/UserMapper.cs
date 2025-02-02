using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Api.Models;

namespace IctuTaekwondo.Api.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
            };
        }

        public static UserProfileResponse ToUserProfileResponse(this UserProfile user)
        {
            return new UserProfileResponse
            {
                Id = user.Id,
                Address = user.Address,
                CurrentRank = user.CurrentRank,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                JoinDate = user.JoinDate
            };
        }

        public static UserFullDetailResponse ToUserFullDetailResponse(this User user)
        {
            return new UserFullDetailResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                Profile = user.UserProfile != null ? ToUserProfileResponse(user.UserProfile) : new UserProfileResponse(),
            };
        }
    }
}
