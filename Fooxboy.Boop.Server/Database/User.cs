using System.ComponentModel.DataAnnotations;

namespace Fooxboy.Boop.Server.Database
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Number { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
    }
}