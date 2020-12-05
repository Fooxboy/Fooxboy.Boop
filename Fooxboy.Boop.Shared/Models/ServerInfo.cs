using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.Boop.Shared.Models
{
    public class ServerInfo
    {
        public string Address { get; set; }
        public string Token { get; set; }
        public string NameUser { get; set; }
        public string NameServer { get; set; }
        public bool AllowRegister { get; set; }
    }
}
