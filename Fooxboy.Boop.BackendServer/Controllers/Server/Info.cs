using System;
using System.Text.Json.Serialization;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fooxboy.Boop.BackendServer.Controllers.Server
{
    [Produces("application/json")]
    [Route("api/server.[controller]")]
    [ApiController]
    public class Info : Controller
    {
        private Logger _logger;
        public Info(Logger logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public Result Index()
        {
            var result = new Result();

            try
            {
                var text = System.IO.File.ReadAllText("ServerInformation.json");
                var data = JsonConvert.DeserializeObject<ServerInfo>(text);
                result.Status = true;
                result.Data = data;
            }
            catch (Exception e)
            {
                _logger.Error("server.info", e);
                result.Status = false;
                var error = new Error() { Code = 100, Message =  e.Message};
                result.Data = error;
            }

            return result;
        }
    }
}