using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên sự kiện")]
        [MaxLength(100)]
        public string Name { get; set; } 

        [Display(Name = "Bắt đầu vào")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Kết thúc vào")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? EndDate { get; set; } 

        [Display(Name = "Địa điểm")]
        public string Location { get; set; } = null!;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; } 

        [Display(Name = "Phí tham dự")]
        public int? Fee { get; set; } 

        [Display(Name = "Giới hạn tham gia")]
        public int? MaxParticipants { get; set; } 

        [Display(Name = "Ngày tạo")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

        [InverseProperty("Event")]
        public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    }
}
