using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models.Users
{
    [ProtoContract]
    public class GetInfo
    {
        [ProtoMember(1)]
        public long UserId { get; set; }
    }
}