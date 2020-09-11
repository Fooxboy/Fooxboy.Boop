using Fooxboy.Boop.Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
                var config = JsonConvert.DeserializeObject<Config>(configString);
                return config;
            } catch (Exception e)
            {
                _logger.Error("Возникла ошибка при чтении конфигурационного файла", e);
            }

            return null;

        }
    }
}
