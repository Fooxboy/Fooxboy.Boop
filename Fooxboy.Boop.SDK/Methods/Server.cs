using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Newtonsoft.Json.Linq;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Server
    {
        private HttpHelper _httpHelper;

        public Server(HttpHelper helper)
        {
            _httpHelper = helper;
        }

        public ServerInfo Info()
        {
            var result = _httpHelper.GetRequest("server.info", new Dictionary<string, string>());
            
            var data = (JObject)result.Data;
            
            if (result.Status) return data.ToObject<Shared.Models.ServerInfo>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public async Task<ServerInfo> InfoAsync()
        {
            var result = await _httpHelper.GetRequestAsync("server.info", new Dictionary<string, string>());

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Shared.Models.ServerInfo>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
    }
}