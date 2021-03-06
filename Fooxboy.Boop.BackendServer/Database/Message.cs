﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fooxboy.Boop.BackendServer.Database
{
    public class Message
    {
        [Key]
        public long MsgId { get; set; }
        public long SenderId { get; set; }
        public long RecieverId { get; set; }
        public long ChatId { get; set; }
        public string Text { get; set; }
        public string ImageSender { get; set; }
        public string NameSender { get; set; } 
        public long Time { get; set; }
        public string UsersReaded { get; set; }
        public long TestField { get; set; }
        public long Attach { get; set; }
        public bool ServiceMessage { get; set; }
    }
}
