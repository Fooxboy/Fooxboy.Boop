
using System;
using System.IO;
using Fooxboy.Boop.Server.Helpers;
using Fooxboy.Boop.Server.Models;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Fooxboy.Boop.Server.Commands;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Services;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Server
{
    public class CommandProcessor
    {
        private readonly ILoggerService _logger;
        private readonly TcpClient _client;
        private NetworkStream _stream;
        public bool IsOnline = false;

        public CommandProcessor(ILoggerService logger, TcpClient clinet)
        {
            _logger = logger;
            _client = clinet;
        }


        public void CreateSession()
        {
            IsOnline = true;
            _stream = _client.GetStream();
            while (IsOnline)
            {
                var data = new byte[1024];
                int bytes = 0;
                do
                {
                    bytes = _stream.Read(data, 0, data.Length);
                } while (_stream.DataAvailable);
                
                Process(data);
            }
        }
        
        public void Process(byte[] data)
        {
            var request = (SocketRequest<object>)data.Deserialize();
            var command = FindCommand(request.Command);
            ExcecuteCommand(command ?? new ErrorCommand(), request);
        }

        private Models.ICommand FindCommand(string commandText)
        {
            var commands = Startup.Commands;
            var command = commands.Where(c => c.Command == commandText);
            return command.FirstOrDefault();
        }



        private void ExcecuteCommand(Models.ICommand command, SocketRequest<object> request)
        {
            _logger.Debug($"Выполнение команды {request.Command}...");

            Task.Run(() =>
            {
                byte[] bytes;

                if (request.Token is null && (request.Command != "reg" || request.Command != "log"))
                {
                    var response = new SocketRequest<long>();
                    response.Command = request.Command;
                    response.TypeData = "error";
                    response.Data = 1;

                    bytes = response.Serialize();

                    _logger.Debug("Пользователь не указал токен.");
                }
                else
                {
                    User user = null;
                    using (var db = new ServerData())
                    {
                        user = db.Users.FirstOrDefault(u => u.Token == request.Token);
                    }

                    
                    if (user is null && (request.Command != "reg" || request.Command != "log"))
                    {
                        //todo: проверка на правильный токен..
                        _logger.Debug("f");
                    }

                    //Проверка на онлайн
                    if (Startup.ConnectedUsers.Any(c => c.User.UserId != user?.UserId))
                    {
                        //Пользователя нет в сети.
                        Startup.ConnectedUsers.Add(new ConnectUser()
                        {
                            LastCheck = DateTime.Now,
                            Client = _client,
                            User = user,
                            Session = this
                        });
                    }

                    var resp = command.Execute(request.Data, user, _logger);

                    switch (resp.TypeData)
                    {
                        case "reg":
                           var response = new SocketRequest<RegisterResponse>();
                           response.TypeData = resp.TypeData;
                           response.Token = request.Token;
                           response.Data = (RegisterResponse) resp.Data;
                           bytes = response.Serialize();
                            break;
                        case "log":
                            var response1 = new SocketRequest<LoginResponse>();
                            response1.TypeData = resp.TypeData;
                            response1.Token = request.Token;
                            response1.Data = (LoginResponse)resp.Data;
                            bytes = response1.Serialize();
                            break;
                        case "msg.snd":
                            var response2 = new SocketRequest<SendResponse>();
                            response2.TypeData = resp.TypeData;
                            response2.Token = request.Token;
                            response2.Data = (SendResponse)resp.Data;
                            bytes = response2.Serialize();
                            break;
                        case "msg.getChat":
                            var response3 = new SocketRequest<GetChatResponse>();
                            response3.TypeData = resp.TypeData;
                            response3.Token = request.Token;
                            response3.Data = (GetChatResponse)resp.Data;
                            bytes = response3.Serialize();
                            break;
                        case "msg.get":
                            var response4 = new SocketRequest<GetResponseProxy>();
                            response4.TypeData = resp.TypeData;
                            response4.Token = request.Token;
                            response4.Data = (GetResponseProxy)resp.Data;
                            bytes = response4.Serialize();
                            break;
                        case "usr.info":
                            var response5 = new SocketRequest<User>();
                            response5.TypeData = resp.TypeData;
                            response5.Token = request.Token;
                            response5.Data = (User)resp.Data;
                            bytes = response5.Serialize();
                            break;
                        case "error":
                            var response6 = new SocketRequest<int>();
                            response6.TypeData = resp.TypeData;
                            response6.Token = request.Token;
                            response6.Data = (int)resp.Data;
                            bytes = response6.Serialize();
                            break;

                        default:
                            new Exception("default");
                            bytes = null;
                            break;
                    }


                }

                _stream.Write(bytes, 0, bytes.Length);
                _logger.Debug("Команда выполнена.");
            });
        }
    }
}