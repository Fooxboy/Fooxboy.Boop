using System.Collections.Generic;

namespace Fooxboy.Boop.Shared.Models.Messages
{
    public class Get
    {
        public List<GetResponse> Chats { get; set; }
    }
    
    public class GetResponse
    {
        public long ChatId { get; set; }
        public string ChatTitle { get; set; }
        public long CountUnreadMessages { get; set; }
        public string LastMessageText { get; set; }
        public long Time { get; set; }
    }
}