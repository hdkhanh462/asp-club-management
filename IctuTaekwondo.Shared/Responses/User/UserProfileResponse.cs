using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Responses.User
{
    public class UserProfileResponse
    {
        public int Id { get; set; }

        [Display(Name = "Giới tính")]
        public Gender? Gender { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateOnly? DateOfBirth { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Cấp đai")]
        public RankName? CurrentRank { get; set; }

        [Display(Name = "Ngày tham gia")]
        public DateOnly? JoinDate { get; set; }
    }
}
