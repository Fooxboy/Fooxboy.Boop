using System.ComponentModel.DataAnnotations;

namespace Fooxboy.Boop.BackendServer.Database
{
    public class GroupChat
    {
        [Key]
        public long ChatId { get; set; }
        public string Members { get; set; }
        public string Title { get; set; }
        public long Admin { get; set; }
        public string Messanges { get; set; }
        
    }
}