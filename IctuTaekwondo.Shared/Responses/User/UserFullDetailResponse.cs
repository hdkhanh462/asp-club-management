namespace IctuTaekwondo.Shared.Responses.User
{
    public class UserFullDetailResponse : UserResponse
    {
        public virtual ICollection<string> Roles { get; set; } = new List<string>();

        public UserProfileResponse Profile { get; set; }
    }
}
