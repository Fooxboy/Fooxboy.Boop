using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Newtonsoft.Json.Linq;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Messages
    {
        private HttpHelper _httpHelper;

        public Messages(HttpHelper helper)
        {
            _httpHelper = helper;
        }

        public Get Get(long count, bool onlyUnread = false, int offset = 0)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("count", count.ToString());
            parameters.Add("offset", offset.ToString());
            parameters.Add("onlyUnread", onlyUnread.ToString());

            var result = _httpHelper.GetRequest("msg.get", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Get>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Get> GetAsync(long count, bool onlyUnread = false, int offset = 0)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("count", count.ToString());
            parameters.Add("offset", offset.ToString());
            parameters.Add("onlyUnread", onlyUnread.ToString());

            var result = await _httpHelper.GetRequestAsync("msg.get", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Get>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public GetChat GetChat(long chatId, long count, long offset = 0)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("count", count.ToString());
            parameters.Add("offset", offset.ToString());
            parameters.Add("chatId", chatId.ToString());
            
            var result =  _httpHelper.GetRequest("msg.getChat", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<GetChat>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<GetChat> GetChatAsync(long chatId, long count, long offset = 0)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("count", count.ToString());
            parameters.Add("offset", offset.ToString());
            parameters.Add("chatId", chatId.ToString());
            
            var result = await _httpHelper.GetRequestAsync("msg.getChat", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<GetChat>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public LongPollService GetLongPollService()
        {
            var service = new LongPollService(_httpHelper);
            return service;
        }

        public Send Send(string text, long chatId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("text", text);
            parameters.Add("chatId", chatId.ToString());
            var result = _httpHelper.GetRequest("msg.send", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Send>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Send> SendAsync(string text, long chatId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("text", text);
            parameters.Add("chatId", chatId.ToString());
            var result = await _httpHelper.GetRequestAsync("msg.send", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Send>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
    }
}