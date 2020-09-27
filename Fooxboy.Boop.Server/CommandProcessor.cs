﻿
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
            var request = data.Deserialize<SocketRequest>();
            var command = FindCommand(request.Command);
            ExcecuteCommand(command ?? new ErrorCommand(), request);
        }

        private Models.ICommand FindCommand(string commandText)
        {
            var commands = Startup.Commands;
            var command = commands.Where(c => c.Command == commandText);
            return command.FirstOrDefault();
        }

        private void ExcecuteCommand(Models.ICommand command, SocketRequest request)
        {
            _logger.Debug($"Выполнение команды {request.Command}...");
            Task.Run(() =>
            {
                var response = new SocketRequest();
                response.Command = request.Command;
                response.TypeData = request.TypeData;
                object data = null;
                
                if (request.Token is null && (request.Command != "reg" || request.Command != "log"))
                {
                    data = 1;
                    response.TypeData = "error";
                    _logger.Debug("Пользователь не указал токен.");
                }
                else
                {
                    User user = null;
                    using (var db = new ServerData())
                    {
                        user = db.Users.FirstOrDefault(u => u.Token == request.Token);
                    }

                    if (user is null & (request.Command != "reg" || request.Command != "log")) 
                    {
                        data = 2;
                        response.TypeData = "error";
                        _logger.Debug("Пользователь указал неверный токен.");
                    }
                    else
                    {
                        //Проверка на онлайн
                        if (Startup.ConnectedUsers.Any(c => c.User.UserId != user?.UserId))
                        {
                            //Пользователя нет в сети.
                            Startup.ConnectedUsers.Add(new ConnectUser()
                            {
                                LastCheck =  DateTime.Now,
                                Client =  _client,
                                User = user,
                                Session =  this
                            });
                        }
                        
                        
                        var  resp = command.Execute(request.Data, user, _logger);

                        if (resp.TypeData is null) resp.TypeData = request.TypeData;
                        response.TypeData = resp.TypeData;
                        data = resp.Data;
                    }
                }
                response.Data = data;
                var bytes = response.Serialize();
                try
                {
                    _stream.Write(bytes, 0, bytes.Length);
                    _logger.Debug("Команда выполнена.");
                }
                catch (Exception e)
                {
                    _logger.Error("Произошла ошибка при отправке ответа.", e);
                }
               
            });
        }
    }
}