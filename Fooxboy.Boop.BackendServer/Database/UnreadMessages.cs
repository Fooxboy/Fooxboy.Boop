using System.ComponentModel.DataAnnotations;

namespace Fooxboy.Boop.BackendServer.Database
{
    public class UnreadMessages
    {
        [Key]
        public long UserId { get; set; }
        public string Messages { get; set; }
    }
}