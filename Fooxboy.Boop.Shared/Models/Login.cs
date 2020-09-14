using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models
{
    [ProtoContract]
    public class Login
    {
        [ProtoMember(1)]
        public string Nickname { get; set; }
        [ProtoMember(2)]
        public string Password { get; set; }
        public bool ResetAuth { get; set; }
    }

    [ProtoContract]
    public class LoginResponse
    {
        [ProtoMember(1)]
        public string Token { get; set; }
        [ProtoMember(2)]
        public long UserId { get; set; }
    }
}