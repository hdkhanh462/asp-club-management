using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IctuTaekwondo.Api.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Tên thành tích")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [DisplayName("Ngày đạt")]
        public DateOnly DateAchieved { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; } 

        [DisplayName("Ngày tạo")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        public string UserId { get; set; } = null!;

        public int? EventId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("AchievementUsers")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("EventId")]
        [InverseProperty("Achievements")]
        public virtual Event? Event { get; set; }

    }
}
