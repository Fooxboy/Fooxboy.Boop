using System;
using System.IO;
using System.Text;
using Fooxboy.Boop.SDK.Models;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Fooxboy.Boop.Shared.Models.Users;
using ProtoBuf;

namespace Fooxboy.Boop.SDK.Helpers
{
    public static class ByteHelper
    {
        public static object Deserialize(this byte[] data)
        {
            var st = new StringBuilder();
            using (var str = new MemoryStream(data))
            {
                while (true)
                {
                    var currentChar = (char)str.ReadByte();
                    if (currentChar == '|')
                    {
                        break;
                    }

                    st.Append(currentChar);
                }

                string typedata = st.ToString();

                switch (typedata)
                {
                    case "reg":
                        return Serializer.Deserialize<SocketRequest<RegisterResponse>>(str);
                    case "log":
                        return Serializer.Deserialize<SocketRequest<LoginResponse>>(str);
                    case "msg.snd":
                        return Serializer.Deserialize<SocketRequest<SendResponse>>(str);
                    case "msg.getChat":
                        return Serializer.Deserialize<SocketRequest<GetChatResponse>>(str);
                    case "msg.get":
                        return Serializer.Deserialize<SocketRequest<GetResponseProxy>>(str);
                    case "usr.info":
                        return Serializer.Deserialize<SocketRequest<User>>(str);
                    default:
                        new Exception("default");
                        return null;
                }
            }

        }

        public static byte[] Serialize<T>(this SocketRequest<T> obj)
        {
            byte[] data;

            using (var stream = new MemoryStream())
            {
                var typedata = ASCIIEncoding.ASCII.GetBytes(obj.TypeData);
                var seporator = ASCIIEncoding.ASCII.GetBytes("|");
                stream.Write(typedata, 0, typedata.Length);
                stream.Write(seporator, 0, seporator.Length);

                Serializer.Serialize(stream, obj);
                data = stream.ToArray();
            }

            return data;
        }
    }
}