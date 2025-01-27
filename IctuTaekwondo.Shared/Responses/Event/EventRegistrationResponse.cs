using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventRegistrationResponse
    {
        public int Id { get; set; }

        [Display(Name = "Ngày đăng ký")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        public virtual EventResponse Event { get; set; }
        public virtual UserResponse User { get; set; }
    }
}
