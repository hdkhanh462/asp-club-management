using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string? Description { get; set; }
        public long? Fee { get; set; }
        public short? MaxParticipants { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<EventStatus> Status { get; set; } = [];
    }
}
