using System;

namespace IctuTaekwondo.Winforms.Utils
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
