using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class UserProfileSchema
    {
        [DisplayName("Giới tính")]
        [StringLength(10)]
        public Gender? Gender { get; set; } = null!;

        [DisplayName("Ngày sinh")]
        public DateOnly? DateOfBirth { get; set; }

        [DisplayName("Địa chỉ")]
        public string? Address { get; set; }

        [DisplayName("Cấp đai")]
        [StringLength(50)]
        public RankName? CurrentRank { get; set; } = null!;

        [DisplayName("Ngày tham gia")]
        public DateOnly? JoinDate { get; set; }

        public string UserId { get; set; } = null!;
    }
}
