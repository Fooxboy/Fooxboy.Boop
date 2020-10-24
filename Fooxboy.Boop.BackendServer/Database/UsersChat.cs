using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fooxboy.Boop.BackendServer.Database
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
