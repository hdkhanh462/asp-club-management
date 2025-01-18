using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses.User;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class UserResgiteredEventResponse : UserResponse
    {
        public RegistrationStatus? Status { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
