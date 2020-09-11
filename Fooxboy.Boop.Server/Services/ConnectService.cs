using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Fooxboy.Boop.Server.Services
{
    public class ConnectService
    {
        private LoggerService _logger;
        public ConnectService(LoggerService logger)
        {
            _logger = logger;
        }

        public void StartCheckConnect(Socket listener, IPEndPoint ipPoint)
        {
            listener.Bind(ipPoint);
            listener.Listen(10);
            while (Startup.IsRun)
            {
                _logger.Debug("Ожидание подключения...");
                var handler = listener.Accept();
                var userIp = ((IPEndPoint)handler.RemoteEndPoint).Address;
                _logger.Debug($"Подключен клиент с IP: {userIp}");
                Task.Run(() => NewConnect(handler));

            }
        }
        
        public void NewConnect(Socket socket)
        {
            _logger.Debug("ауф");
        }
    }
}