using System;
using System.Net.Sockets;
using Fooxboy.Boop.Server.Database;

namespace Fooxboy.Boop.Server.Models
{
    public class ConnectUser
    {
        public Socket Socket { get; set; }
        public User User { get; set; }
        public DateTime LastCheck { get; set; }
    }
}