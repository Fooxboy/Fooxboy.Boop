﻿using System;
using System.Threading;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Server.Helpers;

namespace Fooxboy.Boop.Server.Services
{
    public class ConnectedCheckerService
    {
        private ILoggerService _logger;
        public ConnectedCheckerService(LoggerService logger)
        {
            this._logger = logger;
        }


        public void StartService()
        {
            //запуск каждые 2 минуты.
            Thread.Sleep(90000);
            foreach (var connectUser in Startup.ConnectedUsers)
            {
                try
                {
                    var request = new SocketRequest<int>();
                    request.Command = "check";
                    request.Data = 1;
                    request.TypeData = "chk";
                    var bytes = request.Serialize();
                    connectUser.Client.GetStream().Write(bytes, 0 , bytes.Length);
                }
                catch (Exception e)
                {
                    //Пользователь был отключен от сервера.

                    Startup.ConnectedUsers.Remove(connectUser); //отключаем
                }
                
            }
        }
    }
}