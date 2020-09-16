using System.Collections.Generic;
using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models.Messages
{
    [ProtoContract]
    public class GetChat
    {
        [ProtoMember(1)]
        public long ChatId { get; set; }
        [ProtoMember(2)]
        public long Count { get; set; }
        [ProtoMember(3)]
        public long Offset { get; set; }
    }

    [ProtoContract]
    public class GetChatResponse
    {
        [ProtoMember(1)]
        public List<Message> Messages { get; set; }
    }
}