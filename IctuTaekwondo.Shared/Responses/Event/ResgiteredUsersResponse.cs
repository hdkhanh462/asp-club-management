using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class ResgiteredUsersResponse : UserResponse
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Đăng ký vào")]
        public DateTime? RegisteredAt { get; set; }
    }
}
