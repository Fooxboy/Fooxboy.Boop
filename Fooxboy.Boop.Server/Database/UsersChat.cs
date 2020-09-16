using System.ComponentModel.DataAnnotations;

namespace Fooxboy.Boop.Server.Database
{
    public class UsersChat
    {
        [Key]
        public long LocalId { get; set; }
        public long Owner { get; set; }
        public long ChatId { get; set; }
        public string Messages { get; set; }
        public long LastMessage { get; set; }
    }
}