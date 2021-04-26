using System.Collections.Generic;
using System.Linq;
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


        private static LongPollService _longPollService;

        public LongPollService GetLongPollService(string address, string token)
        {

            //if (!(address.Any(c => c == ':'))) address += ":2020";

            address = $"http://{address}";

            if (!(_longPollService is null)) return _longPollService;
            _longPollService = new LongPollService(address, token);
            return _longPollService;
        }

        public Send Send(string text, long chatId, long attach)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("text", text);
            parameters.Add("chatId", chatId.ToString());
            var result = _httpHelper.GetRequest("msg.send", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Send>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Send> SendAsync(string text, long chatId, long attach)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("text", text);
            parameters.Add("chatId", chatId.ToString());
            parameters.Add("attach", attach.ToString());
            var result = await _httpHelper.GetRequestAsync("msg.send", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Send>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<GetUploadServer> GetUploadServerAsync(long chatId, long typeAttach)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("chatId", chatId.ToString());
            parameters.Add("typeAttach", typeAttach.ToString());
            var result =  await _httpHelper.GetRequestAsync("msg.GetUploadAttachUrl", parameters);
            
            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<GetUploadServer>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            
        }


        public Attach GetAttachment(long attachId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("attachId", attachId.ToString());
            var result =  _httpHelper.GetRequest("msg.getAttachment", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Attach>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Attach> GetAttachmentAsync(long attachId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("attachId", attachId.ToString());
            var result = await _httpHelper.GetRequestAsync("msg.getAttachment", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Attach>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Chat> CreateChat(string name)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("name", name);
            var result = await _httpHelper.GetRequestAsync("msg.createChat", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Chat>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task AddToChat(long chatId, long userId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("chatId", chatId.ToString());
            parameters.Add("userId", userId.ToString());
            var result = await _httpHelper.GetRequestAsync("msg.addToChat", parameters);
        }
    }
}