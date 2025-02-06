using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Responses.User
{
    public class UserProfileResponse
    {
        public int Id { get; set; }

        [DisplayName("Giới tính")]
        public Gender? Gender { get; set; }

        [DisplayName("Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly? DateOfBirth { get; set; }

        [DisplayName("Địa chỉ")]
        public string? Address { get; set; }

        [DisplayName("Cấp đai")]
        public RankName? CurrentRank { get; set; }

        [DisplayName("Ngày tham gia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly? JoinDate { get; set; }
    }
}
