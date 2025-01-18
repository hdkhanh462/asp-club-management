using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Responses.User
{
    public class UserProfileResponse
    {
        public int Id { get; set; }
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public RankName? CurrentRank { get; set; }
        public DateOnly? JoinDate { get; set; }
    }
}
