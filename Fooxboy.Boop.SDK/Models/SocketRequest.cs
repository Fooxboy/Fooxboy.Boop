using ProtoBuf;

namespace Fooxboy.Boop.SDK.Models
{
    [ProtoContract]
    public class SocketRequest
    {
        [ProtoMember(1)]
        public string Command { get; set; }

        [ProtoMember(2)] 
        public string Token { get; set; }

        [ProtoMember(3)]
        public object Data { get; set; }
        
        [ProtoMember(4)]
        public string TypeData { get; set; }
    }
}