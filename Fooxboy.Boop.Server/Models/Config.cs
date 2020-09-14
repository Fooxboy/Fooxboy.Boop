using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Fooxboy.Boop.Server.Models
{
    public class Config
    {
        [JsonProperty("connect")]
        public ConnectModel Connect { get; set; }


        public class ConnectModel
        {
            [JsonProperty("port")]
            public int Port { get; set; }
            [JsonProperty("ip")]
            public string Ip { get; set; }
        }
    }
}
