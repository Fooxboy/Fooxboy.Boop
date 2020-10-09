using ProtoBuf;

namespace Fooxboy.Boop.Server.Models
{
    [ProtoContract]
    public class SocketRequest<T>
    {
        [ProtoMember(1)]
        public string Command { get; set; }

        [ProtoMember(2)] 
        public string Token { get; set; }

        [ProtoMember(3)]
        public T Data { get; set; }
        
        [ProtoMember(4)]
        public string TypeData { get; set; }
    }
}