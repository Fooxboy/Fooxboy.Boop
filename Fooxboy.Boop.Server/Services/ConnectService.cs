using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Fooxboy.Boop.Server.Helpers;
using Fooxboy.Boop.Server.Models;

namespace Fooxboy.Boop.Server.Services
{
    public class ConnectService
    {
        private LoggerService _logger;
        public ConnectService(LoggerService logger)
        {
            _logger = logger;
        }

        public void StartCheckConnect(TcpListener listener)
        {
            listener.Start();
            
            while (Startup.IsRun)
            {
                _logger.Debug("Ожидание подключения...");
                var client = listener.AcceptTcpClient();
                var userIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                _logger.Debug($"Подключен клиент с IP: {userIp}");
                Task.Run(() => NewConnect(client));
            }
        }
        
        public void NewConnect(TcpClient client)
        {
            _logger.Debug("Creating proccessor...");
            var processor = new CommandProcessor(_logger, client);
            Task.Run(() => processor.CreateSession());
        }
        
        
    }
}