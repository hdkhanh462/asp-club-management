using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IctuTaekwondo.Shared.Schemas.Event;

namespace IctuTaekwondo.Shared.Models
{
    public class Event : TEventWithStartEndDate
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên sự kiện")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Display(Name = "Địa điểm")]
        public string Location { get; set; } = null!;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Phí tham dự")]
        public int Fee { get; set; }

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
