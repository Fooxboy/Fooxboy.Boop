using System.Net.Sockets;
using System.Text;

namespace Fooxboy.Boop.Server.Helpers
{
    public static class DecoderHelper
    {
        public static string DecodeReceiveToStringUTF8(this Socket socket, int bufferCount = 1024)
        {
            string data = null;
            var buffer = new byte[bufferCount];
            var receive = socket.Receive(buffer);
            data += Encoding.UTF8.GetString(buffer, 0, receive);
            return data;
        }
        
        public static byte[] DecodeReceiveToBytes(this Socket socket, int bufferCount = 1024)
        {
            var data = new byte[bufferCount];
            var receive = socket.Receive(data);
            return data;
        }
    }
}