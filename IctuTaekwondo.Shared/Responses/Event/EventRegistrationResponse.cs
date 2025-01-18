using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventRegistrationResponse
    {
        public int Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        public RegistrationStatus? Status { get; set; }

        public virtual EventResponse Event { get; set; }

        public virtual UserResponse User { get; set; }
    }
}
