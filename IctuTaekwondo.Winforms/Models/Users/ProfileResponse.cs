using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Winforms.Enums;

namespace IctuTaekwondo.Winforms.Models.Users
{
    public class ProfileResponse
    {
        public int Id { get; set; }

        [DisplayName("Giới tính")]
        public Gender? Gender { get; set; }

        [DisplayName("Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Cấp đai")]
        public RankName? CurrentRank { get; set; }

        [DisplayName("Ngày tham gia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JoinDate { get; set; }
    }
}
