
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
        private readonly Socket _socket;

        public CommandProcessor(ILoggerService logger, Socket socket)
        {
            _logger = logger;
            _socket = socket;
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
                if (request.Token is null)
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
                    if (user is null)
                    {
                        data = 2;
                        response.TypeData = "error";
                        _logger.Debug("Пользователь указал неверный токен.");
                    }
                    else
                    {
                        var  resp = command.Execute(request.Data, user, _logger);

                        if (resp.TypeData is null) resp.TypeData = request.TypeData;
                        response.TypeData = resp.TypeData;
                        data = resp.Data;
                    }
                }
                response.Data = data;
                var bytes = response.Serialize();
                _socket.Send(bytes);
                _logger.Debug("Команда выполнена.");
            });
        }
    }
}