using System.Collections.Generic;
using ProtoBuf;

namespace Fooxboy.Boop.Shared.Models.Messages
{
    [ProtoContract]
    public class Get
    {
        [ProtoMember(1)]
        public long Count { get; set; }
        [ProtoMember(2)]
        public bool OnlyUnread { get; set; }
        [ProtoMember(3)]
        public long Offset { get; set; }
    }

    [ProtoContract]
    public class GetResponseProxy
    {
        [ProtoMember(1)]
        public List<GetResponse> Chats { get; set; }
    }

    [ProtoContract]
    public class GetResponse
    {
        [ProtoMember(1)]
        public long ChatId { get; set; }
        [ProtoMember(2)]
        public string ChatTitle { get; set; }
        [ProtoMember(3)]
        public long CountUnreadMessages { get; set; }
        [ProtoMember(4)]
        public string LastMessageText { get; set; }
        [ProtoMember(5)]
        public long Time { get; set; }
    }
}