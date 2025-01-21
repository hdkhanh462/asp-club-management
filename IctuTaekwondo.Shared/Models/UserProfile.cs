using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using IctuTaekwondo.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Shared.Models
{
    [Index("UserId", Name = "UserProfiles_UserId_Key", IsUnique = true)]
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Giới tính")]
        [MaxLength(10)]
        public Gender? Gender { get; set; } = null!;

        [DisplayName("Ngày sinh")]
        public DateOnly? DateOfBirth { get; set; } 

        [DisplayName("Địa chỉ")]
        public string? Address { get; set; } 

        [DisplayName("Cấp đai")]
        [MaxLength(50)]
        public RankName? CurrentRank { get; set; } = null!;

        [DisplayName("Ngày tham gia")]
        public DateOnly? JoinDate { get; set; } 

        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("UserProfile")]
        public virtual User User { get; set; } = null!;
    }
}
