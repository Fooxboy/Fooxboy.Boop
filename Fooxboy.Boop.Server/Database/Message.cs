﻿using System.ComponentModel.DataAnnotations;

namespace Fooxboy.Boop.Server.Database
{
    public class Message
    {
        [Key]
        public long MsgId { get; set; }
        public long SenderId { get; set; }
        public long RecieverId { get; set; }
        public long ChatId { get; set; }
        public string Text { get; set; }
        public long Time { get; set; }
        public string UsersReaded { get; set; }
    }
}