using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public string Location { get; set; }

        public string? Description { get; set; }

        public long? Fee { get; set; }

        public int? MaxParticipants { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}
