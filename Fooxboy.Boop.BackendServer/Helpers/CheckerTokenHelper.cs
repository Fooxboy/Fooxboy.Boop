using System;
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
                if (user != null)
                {
                    user.LastSeen = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    user.LastSeenText = "Сейчас в сети";
                    db.SaveChanges();
                }

                return user;
            }
        }
    }
}