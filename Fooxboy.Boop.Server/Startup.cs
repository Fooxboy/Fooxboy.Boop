using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DryIoc;
using Fooxboy.Boop.Server.Commands;
using Fooxboy.Boop.Server.Commands.Messages;
using Fooxboy.Boop.Server.Commands.Users;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Server.Services;

namespace Fooxboy.Boop.Server
{
    public class Startup
    {
        public static List<ICommand> Commands { get; private set; }
        private IContainer _container;
        private ILoggerService _logger;
        private ConnectedCheckerService _checker;
        public static bool IsRun = false;
        public static List<ConnectUser> ConnectedUsers { get; private set; } 
        public Startup()
        {
            IContainer container = new Container();
            container.Register<LoggerService>(Reuse.Singleton);
            container.Register<ConfigService>(Reuse.Singleton);
            container.Register<ConnectService>(Reuse.Singleton);
            container.Register<ConnectedCheckerService>(Reuse.Singleton);
            
            
            Commands = new List<ICommand>();
            ConnectedUsers = new List<ConnectUser>();
            
            _container = container;
            _logger = _container.Resolve<LoggerService>();
            _checker = _container.Resolve<ConnectedCheckerService>();
        }
        
        public void Run()
        {
            Commands.Add(new Get());
            Commands.Add(new Send());
            Commands.Add(new GetChat());
            Commands.Add(new GetInfo());
            Commands.Add(new LoginCommand());
            Commands.Add(new RegisterCommand());
            Commands.Add(new ErrorCommand());
            
            IsRun = true;
            var config = _container.Resolve<ConfigService>().GetConfig();
            if (config is null)
            {
                _logger.Warning("Остановка сервера....");
                IsRun = false;
                return;
            }
            _logger.Warning($"Запуск сервера по адресу {config.Connect.Ip}:{config.Connect.Port}...");
            _logger.Debug("Привязка ip и port...");
            try
            {
                var ipAddress = IPAddress.Parse(config.Connect.Ip);
                var listener = new TcpListener(ipAddress, config.Connect.Port);
                var connectService = _container.Resolve<ConnectService>();
                Task.Run(() =>
                {
                    connectService.StartCheckConnect(listener);
                });

                Task.Run((() =>
                {
                    _checker.StartService();
                }));
            }
            catch (Exception e)
            {
                IsRun = false;
                _logger.Error("Произошла ошибка при привязке ip и port", e);
                return;
            }

           
        }
    }
}
