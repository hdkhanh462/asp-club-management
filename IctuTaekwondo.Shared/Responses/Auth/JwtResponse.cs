namespace IctuTaekwondo.Shared.Responses.Auth
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
