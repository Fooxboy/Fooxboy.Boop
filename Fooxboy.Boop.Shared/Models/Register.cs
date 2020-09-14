using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models
{
    [ProtoContract]
    public class Register
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
        [ProtoMember(3)]
        public string Nickname { get; set; }
        [ProtoMember(4)]
        public string Password { get; set; }
        [ProtoMember(5)]
        public string Number { get; set; }
    }

    [ProtoContract]
    public class RegisterResponse
    {
        [ProtoMember(1)]
        public string Token { get; set; }
        [ProtoMember(2)]
        public long UserId { get; set; }
    }
}