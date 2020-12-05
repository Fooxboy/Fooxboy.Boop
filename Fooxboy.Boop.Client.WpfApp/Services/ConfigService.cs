using System.Collections.Generic;
using System.IO;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Shared.Models;
using Newtonsoft.Json;

namespace Fooxboy.Boop.Client.WpfApp.Services
{
    public class ConfigService
    {
        public Config GetConfig()
        {
            if (File.Exists("config.json"))
            {
                var text = File.ReadAllText("config.json");
                var config = JsonConvert.DeserializeObject<Config>(text);
                return config;
            }
            else
            {
               var stream = File.Create("config.json");
               stream.Close();
               stream.Dispose();

               var config = new Config()
               {
                   ShowWelcomePage = true,
                   Servers =  new List<ServerInfo>()
               };

               var text = JsonConvert.SerializeObject(config);
               File.WriteAllText("config.json", text);

               return config;
            }
        }

        public void EditConfig(Config config)
        {
            var text = JsonConvert.SerializeObject(config);
            File.WriteAllText("config.json", text);
        }
    }
}