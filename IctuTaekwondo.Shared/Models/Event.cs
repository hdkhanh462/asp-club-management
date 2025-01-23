using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IctuTaekwondo.Shared.Schemas.Event;

namespace IctuTaekwondo.Shared.Models
{
    public class Event : TEventWithStartEndDate
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string? Description { get; set; }

        public int Fee { get; set; }

        public int? MaxParticipants { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

        [InverseProperty("Event")]
        public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    }

    
}
