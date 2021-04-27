namespace Fooxboy.Boop.Shared.Models.Messages
{
    public class Message
    {
        public long MsgId { get; set; }
        public long SenderId { get; set; }
        public string NameSender { get; set; }
        public string ImageSender { get; set; }
        public long RecieverId { get; set; }
        public long ChatId { get; set; }
        public string Text { get; set; }
        public long Time { get; set; }
        public long Attach { get; set; }
        public string UsersReaded { get; set; }
        public bool ServiceMessage { get; set; }
    }
}