using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Schemas.Account
{
    public class UserProfileSchema
    {
        [DisplayName("Giới tính")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Giới tính không hợp lệ")]
        public Gender? Gender { get; set; } = null!;

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date, ErrorMessage = "Đinh dạng ngày tháng không chính xác")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly? DateOfBirth { get; set; }

        [DisplayName("Địa chỉ")]
        public string? Address { get; set; }

        [DisplayName("Cấp đai")]
        [EnumDataType(typeof(RankName), ErrorMessage = "Cấp đai không hợp lệ")]
        public RankName? CurrentRank { get; set; } = null!;

        [DisplayName("Ngày tham gia")]
        [DataType(DataType.Date, ErrorMessage = "Đinh dạng ngày tháng không chính xác")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly? JoinDate { get; set; }
    }
}
