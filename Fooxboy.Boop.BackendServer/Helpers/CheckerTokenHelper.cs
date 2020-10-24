using System.Linq;
using Fooxboy.Boop.BackendServer.Database;

namespace Fooxboy.Boop.BackendServer.Helpers
{
    public class CheckerTokenHelper
    {
        public static User GetUser(string token)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Token == token);
                return user;
            }
        }
    }
}