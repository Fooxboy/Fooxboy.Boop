using System;
using System.Net.Sockets;
using Fooxboy.Boop.Server.Database;

namespace Fooxboy.Boop.Server.Models
{
    public class ConnectUser
    {
        public TcpClient Client { get; set; }
        public User User { get; set; }
        public DateTime LastCheck { get; set; }
        public CommandProcessor Session { get; set; }
    }
}