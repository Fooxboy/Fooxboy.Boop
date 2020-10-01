using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Methods;
using Fooxboy.Boop.SDK.Models;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Fooxboy.Boop.Shared.Models.Users;
using Login = Fooxboy.Boop.SDK.Methods.Login;
using Register = Fooxboy.Boop.SDK.Methods.Register;

namespace Fooxboy.Boop.SDK
{
    public class Api
    {
        private string _ipString;
        private int _port;
        private string _token;
        private TcpClient _client;
        private NetworkStream _stream;

        public EventDelegate<int> ErrorEvent;

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

            var client = new TcpClient();
            client.Connect(ipAddress, _port);
            
            _client = client;
            _stream = client.GetStream();
            
            SenderHelper.Init(client, _token);

            Task.Run(StartCommandProccessor);
        }

        public void SetNewToken(string token)
        {
            _token = token;
            SenderHelper.GetHelper().SetNewToken(token);
        }

        private void StartCommandProccessor()
        {
            while (true)
            {
                var data = new byte[1024];
                int bytes = 0;
                do
                {    
                    bytes = _stream.Read(data, 0, data.Length);
                } while (_stream.DataAvailable);

                var model = data.Deserialize<SocketRequest>();
                
                
                //Много плохого кода простите, но мне лень, потом перепишу когда нибудь.

                switch (model.TypeData)
                {
                    case "reg":
                        Register.Invoke((RegisterResponse)model.Data);
                        break;
                    case "log":
                        Login.Invoke((LoginResponse)model.Data);
                        break;
                    case "msg.snd":
                        Messages.InvokeSend((SendResponse)model.Data);
                        break;
                    case "msg.getChat":
                         Messages.InvokeGetChat((GetChatResponse) model.Data);
                        break;
                    case "msg.get":
                        Messages.InvokeGet((GetResponseProxy)model.Data);
                        break;
                    case "usr.info":
                        Users.InvokeInfo((User)model.Data);
                        break;
                    case "error":
                        this.ErrorEvent?.Invoke((int)model.Data);
                        break;

                    default: new Exception("default");
                        break;
                }
                
            }
        }
        
        
    }
}