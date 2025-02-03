using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class UserResgiteredEventResponse : UserResponse
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? RegisteredAt { get; set; }
    }
}
