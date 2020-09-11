using Fooxboy.Boop.Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fooxboy.Boop.Server.Services
{
    public class ConfigService
    {
        private LoggerService _logger;
        public ConfigService(LoggerService logger)
        {
            _logger = logger;
        }

        public Config GetConfig()
        {
            try
            {
                var configString = File.ReadAllText("config.json");
            } catch (Exception e)
            {

            }   

        }
    }
}
