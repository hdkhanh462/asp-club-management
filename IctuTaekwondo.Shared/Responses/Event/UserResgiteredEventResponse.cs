using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class UserResgiteredEventResponse : UserResponse
    {
        public DateTime? RegisteredAt { get; set; }
    }
}
