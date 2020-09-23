using System.IO;
using ProtoBuf;

namespace Fooxboy.Boop.SDK.Helpers
{
    public static class ByteHelper
    {
        public static T Deserialize<T>(this byte[] data)
        {
            T obj = default(T);

            using (var str = new MemoryStream(data))
            {
                obj = Serializer.Deserialize<T>(str);
            }

            return obj;
        }
        
        public static byte[] Serialize(this object obj)
        {
            byte[] data;

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, obj);
                data = stream.GetBuffer();
            }

            return data;
        }
    }
}