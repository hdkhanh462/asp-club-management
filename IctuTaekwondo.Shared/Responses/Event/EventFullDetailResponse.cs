using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventFullDetailResponse : EventResponse
    {
        public virtual ICollection<UserResgiteredEventResponse> RegisteredUsers { get; set; } = new List<UserResgiteredEventResponse>();
    }
}
