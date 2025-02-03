using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IctuTaekwondo.Api.Models
{
    public class EventRegistration
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
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

