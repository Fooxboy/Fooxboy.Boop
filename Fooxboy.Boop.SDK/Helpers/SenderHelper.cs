using System.Net.Sockets;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK.Helpers
{
    public class SenderHelper
    {
        private TcpClient _client;
        private string _token;
        private readonly NetworkStream _stream;

        private static SenderHelper _helper;
        public static SenderHelper GetHelper()=> _helper;

        public static void Init(TcpClient client, string token)
        {
            _helper = new SenderHelper(client, token);
        }
        
        private SenderHelper(TcpClient client, string token)
        {
            _client = client;
            _token = token;
            _stream = client.GetStream();
        }

        public void SetNewToken(string token)
        {
            _token = token;
        }
        
        public void Send<T>(string typedata, T data, string command)
        {

            var request = new SocketRequest<T>();

            request.Command = command;
            request.Data = data;
            request.Token = _token;
            request.TypeData = typedata;

            var bytes = request.Serialize();
            
            _stream.Write(bytes, 0, bytes.Length);

        }
    }
}