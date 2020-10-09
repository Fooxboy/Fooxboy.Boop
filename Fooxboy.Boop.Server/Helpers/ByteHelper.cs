using System;
using System.IO;
using System.Text;
using Fooxboy.Boop.Server.Commands.Users;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using ProtoBuf;
using GetInfo = Fooxboy.Boop.Shared.Models.Users.GetInfo;

namespace Fooxboy.Boop.Server.Helpers
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
                        return Serializer.Deserialize<SocketRequest<Register>>(str);
                    case "log":
                        return Serializer.Deserialize<SocketRequest<Login>>(str);
                    case "msg.snd":
                        return Serializer.Deserialize<SocketRequest<Send>>(str);
                    case "msg.getChat":
                        return Serializer.Deserialize<SocketRequest<GetChat>>(str);
                    case "msg.get":
                        return Serializer.Deserialize<SocketRequest<Get>>(str);
                    case "usr.info":
                        return Serializer.Deserialize<SocketRequest<GetInfo>>(str);
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
                var typedata = System.Text.ASCIIEncoding.ASCII.GetBytes(obj.TypeData);
                stream.Write(typedata, 0, typedata.Length);


                Serializer.Serialize(stream, obj);
                data = stream.ToArray();
            }

            return data;
        }
    }
}