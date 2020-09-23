using System.Net.Sockets;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK.Helpers
{
    public class SenderHelper
    {
        private Socket _socket;
        private string _token;

        private static SenderHelper _helper;
        public static SenderHelper GetHelper()=> _helper;

        public static void Init(Socket socket, string token)
        {
            _helper = new SenderHelper(socket, token);
        }
        
        private SenderHelper(Socket socket, string token)
        {
            _socket = socket;
            _token = token;
        }

        public void SetNewToken(string token)
        {
            _token = token;
        }
        
        public void Send(string typedata, object data, string command)
        {
            var request = new SocketRequest();
            request.Command = command;
            request.Data = data;
            request.Token = _token;
            request.TypeData = typedata;

            var bytes = request.Serialize();

            _socket.Send(bytes);
        }
    }
}