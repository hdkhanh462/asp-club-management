using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IctuTaekwondo.Shared.DataAnnotations;

namespace IctuTaekwondo.Api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime StartDate { get; set; }
        
        [GreaterThan("StartDate")] 
        public DateTime? EndDate { get; set; } 
        public string? Description { get; set; }

        [Range(1000, long.MaxValue)]
        public long? Fee { get; set; }

        [Range(1, 999)]
        public short? MaxParticipants { get; set; }
        public DateTime? CreatedAt { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();

        [InverseProperty("Event")]
        public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    }

    
}
