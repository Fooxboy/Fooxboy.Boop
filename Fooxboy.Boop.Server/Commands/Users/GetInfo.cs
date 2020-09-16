using System.Linq;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Models;

namespace Fooxboy.Boop.Server.Commands.Users
{
    public class GetInfo:ICommand
    {
        public string Command => "usr.info";
        public int Id => 5;
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            var response = new ResponseCommand();

            if (!(data is Shared.Models.Users.GetInfo))
            {
                response.Data = 5;
                response.TypeData = "error";
                return response;
            }

            var info = (Shared.Models.Users.GetInfo) data;

            using (var db = new ServerData())
            {
                var usr = db.Users.SingleOrDefault(u => u.UserId == info.UserId);
                if (usr is null)
                {
                    response.Data = 10;
                    response.TypeData = "error";
                    return response;
                }
                
                
            }
        }
    }
}