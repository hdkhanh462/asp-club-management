using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Event
{
    public class EventCreateSchema
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Location { get; set; }

        public string? Description { get; set; }

        public int? Fee { get; set; }

        public int? MaxParticipants { get; set; }
    }
}
