using System;
using System.Collections.Generic;
using System.Text;

namespace Fooxboy.Boop.Server.Models
{
    public class Config
    {
        public ConnectModel Connect { get; set; }


        public class ConnectModel
        {
            public int Port { get; set; }
            public string Ip { get; set; }
        }
    }
}
