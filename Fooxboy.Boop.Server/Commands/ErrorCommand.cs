using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Server.Services;

namespace Fooxboy.Boop.Server.Commands
{
    public class ErrorCommand:ICommand
    {
        public string Command => "error";
        public int Id => 0;
        
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            return new ResponseCommand()
            {
                TypeData =  "error",
                Data = 0
            };
        }
    }
}