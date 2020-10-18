
using System;
using System.IO;
using Fooxboy.Boop.Server.Helpers;
using Fooxboy.Boop.Server.Models;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Fooxboy.Boop.Server.Commands;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Services;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Fooxboy.Boop.Shared.Models.Users;
using User = Fooxboy.Boop.Server.Database.User;

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
            int countBytes = 0;
            while (IsOnline)
            {
                var data = new byte[1024];
                do
                {
                    countBytes = _stream.Read(data, 0, data.Length);
                    
                } while (_stream.DataAvailable);

                var data2 = new byte[countBytes];

                for (int i = 0; i < countBytes; i++)
                {
                    data2[i] = data[i];
                }

                Process(data2);
            }
        }
        
        public void Process(byte[] data)
        {

            //todo: придумать нормальную реализацию.

            var resp = data.Deserialize();

            if (resp is SocketRequest<Register>)
            {
                var request = (SocketRequest<Register>) resp;
                var command = FindCommand(request.Command);
                ExcecuteCommand(command ?? new ErrorCommand(), request);
            }
            else if (resp is SocketRequest<Login>)
            {
                var request = (SocketRequest<Login>)resp;
                var command = FindCommand(request.Command);
                ExcecuteCommand(command ?? new ErrorCommand(), request);
            }
            else if (resp is SocketRequest<Send>)
            {
                var request = (SocketRequest<Send>)resp;
                var command = FindCommand(request.Command);
                ExcecuteCommand(command ?? new ErrorCommand(), request);
            }
            else if (resp is SocketRequest<GetChat>)
            {
                var request = (SocketRequest<GetChat>)resp;
                var command = FindCommand(request.Command);
                ExcecuteCommand(command ?? new ErrorCommand(), request);
            }
            else if (resp is SocketRequest<Get>)
            {
                var request = (SocketRequest<Get>)resp;
                var command = FindCommand(request.Command);
                ExcecuteCommand(command ?? new ErrorCommand(), request);
            }
            else if (resp is SocketRequest<GetInfo>)
            {
                var request = (SocketRequest<GetInfo>)resp;
                var command = FindCommand(request.Command);
                ExcecuteCommand(command ?? new ErrorCommand(), request);
            }
            else
            {
                new Exception("F");
            }

        }

        private Models.ICommand FindCommand(string commandText)
        {
            var commands = Startup.Commands;
            var command = commands.Where(c => c.Command == commandText);
            return command.FirstOrDefault();
        }

        private void ExcecuteCommand<T>(Models.ICommand command, SocketRequest<T> request)
        {
            _logger.Debug($"Выполнение команды {request.Command}...");

            Task.Run(() =>
            {
                byte[] bytes;

                ResponseCommand resp;
                User user = null;

                if (request.Token is null)
                {

                    if (request.Command == "log" || request.Command == "reg")
                    {
                        
                    }
                    else
                    {
                        var response = new SocketRequest<long>();
                        response.Command = request.Command;
                        response.TypeData = "error";
                        response.Data = 1;

                        bytes = response.Serialize();

                        _stream.Write(bytes, 0, bytes.Length);
                        _logger.Debug("Команда выполнена.");
                        return;

                    }

                }
                else
                {
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


                }


                resp = command.Execute(request.Data, user, _logger);

                switch (resp.TypeData)
                {
                    case "reg":
                        var response = new SocketRequest<RegisterResponse>();
                        response.TypeData = resp.TypeData;
                        response.Token = request.Token;
                        response.Data = (RegisterResponse)resp.Data;
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
                _stream.Write(bytes, 0, bytes.Length);
                _logger.Debug("Команда выполнена.");
            });
        }
    }
}