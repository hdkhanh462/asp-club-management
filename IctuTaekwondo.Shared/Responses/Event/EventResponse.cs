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
        public int Fee { get; set; }
        public int? MaxParticipants { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
