using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models.Users
{
    [ProtoContract]
    public class User
    {
        [ProtoMember(1)]
        public long UserId { get; set; }
        [ProtoMember(2)]
        public string Nickname { get; set; }
        [ProtoMember(3)]
        public string Number { get; set; }
        [ProtoMember(4)]
        public string FirstName { get; set; }
        [ProtoMember(5)]
        public string LastName { get; set; }
        [ProtoMember(6)]
        public string PathProfilePic { get; set; }
    }
}