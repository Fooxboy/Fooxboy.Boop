using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models.Messages
{
    [ProtoContract]
    public class Send
    {
        [ProtoMember(1)]
        public string Text { get; set; }
        [ProtoMember(2)]
        public long ChatId { get; set; }
    }

    [ProtoContract]
    public class SendResponse
    {
        [ProtoMember(1)]
        public long MsgId { get; set; }
    }
}