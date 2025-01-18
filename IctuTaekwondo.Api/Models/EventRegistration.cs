using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Models
{
    public class EventRegistration
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Trạng thái")]
        [MaxLength(15)]
        public RegistrationStatus? Status { get; set; }

        [DisplayName("Ngày đăng ký")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? RegistrationDate { get; set; }

        public int EventId { get; set; }

        public string UserId { get; set; } = null!;

        [ForeignKey("EventId")]
        [InverseProperty("EventRegistrations")]
        public virtual Event Event { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("EventRegistrations")]
        public virtual User User { get; set; } = null!;
    }
}

