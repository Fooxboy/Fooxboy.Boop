using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models.Messages
{
    [ProtoContract]
    public class Message
    {
        [ProtoMember(1)]
        public long MsgId { get; set; }
        [ProtoMember(2)]
        public long SenderId { get; set; }
        [ProtoMember(3)]
        public long RecieverId { get; set; }
        [ProtoMember(4)]
        public long ChatId { get; set; }
        [ProtoMember(5)]
        public string Text { get; set; }
        [ProtoMember(6)]
        public long Time { get; set; }
        [ProtoMember(7)]
        public string UsersReaded { get; set; }
    }
}