using System.Net;
using System.Net.Sockets;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Methods;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK
{
    public class Api
    {
        private string _ipString;
        private int _port;
        private string _token;

        public Api(string ipString, int port, string token)
        {
            _ipString = ipString;
            _port = port;

            _token = token;
            
            //Создание объектов класса
            
            Register = new Register();
            Login = new Login();
            Messages = new Messages();
            Users = new Users();
        }
        
        public Register Register { get; }
        public Login Login { get; }
        public Messages Messages { get;  }
        public Users Users { get; }


        public void Connect()
        {
            var ipAddress = IPAddress.Parse(_ipString);
            var endPoint = new IPEndPoint(ipAddress, 2020);
            
            var socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
            
            SenderHelper.Init(socket, _token);
            
        }

        public void SetNewToken(string token)
        {
            _token = token;
            SenderHelper.GetHelper().SetNewToken(token);
        }

        private void CommandManager(Socket socket)
        {
            while (true)
            {
                var bytes = new byte[1024];

                var res = socket.Receive(bytes);

                var response = bytes.Deserialize<SocketRequest>();
                
                //todo: обработка комманд

            }
        }
        
        
    }
}