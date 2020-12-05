using System.Collections.Generic;

namespace Fooxboy.Boop.Client.WpfApp.Models
{
    public class Config
    {
        public bool ShowWelcomePage { get; set; }
        public List<ServerInfo> Servers { get; set; }
    }

    public class ServerInfo
    {
        public string Address { get; set; }
        public string Token { get; set; }
        public string NameUser { get; set; }
    }
}