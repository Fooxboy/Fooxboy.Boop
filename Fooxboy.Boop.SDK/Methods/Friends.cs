using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Newtonsoft.Json.Linq;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Friends
    {
        private HttpHelper _httpHelper;

        public Friends(HttpHelper helper)
        {
            _httpHelper = helper;
        }

        public void Add(long id)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", id.ToString());
            
            var result = _httpHelper.GetRequest("friends.add", parameters);
            var data = result.Data as JObject;
            if (!result.Status) throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task AddAsync(long id)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", id.ToString());
            
            var result = await _httpHelper.GetRequestAsync("friends.add", parameters);
            var data = result.Data as JObject;
            if (!result.Status) throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public void Confirm(long id)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", id.ToString());
            
            var result = _httpHelper.GetRequest("friends.confirm", parameters);
            var data = result.Data as JObject;
            if (!result.Status) throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task ConfirmAsync(long id)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", id.ToString());
            
            var result = await _httpHelper.GetRequestAsync("friends.confirm", parameters);
            var data = result.Data as JObject;
            if (!result.Status) throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public Shared.Models.Friends.Get Get()
        {
            var result =  _httpHelper.GetRequest("friends.get", new Dictionary<string,string>());

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Shared.Models.Friends.Get>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Shared.Models.Friends.Get> GetAsync()
        {
            var result =  await _httpHelper.GetRequestAsync("friends.get", new Dictionary<string,string>());

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Shared.Models.Friends.Get>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public void Remove(long id)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", id.ToString());
            
            var result = _httpHelper.GetRequest("friends.remove", parameters);
            var data = result.Data as JObject;
            if (!result.Status) throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task RemoveAsync(long id)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", id.ToString());
            
            var result = await _httpHelper.GetRequestAsync("friends.remove", parameters);
            var data = result.Data as JObject;
            if (!result.Status) throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public long GetCountRequests()
        {
            var result =  _httpHelper.GetRequest("friends.getCountRequests", new Dictionary<string,string>());

            if (result.Status)
            {
                return (long) result.Data;
            }
            else
            {
                var data = (JObject) result.Data;
                throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            }
        }
        
        public async Task<long> GetCountRequestsAsync()
        {
            var result = await _httpHelper.GetRequestAsync("friends.getCountRequests", new Dictionary<string,string>());

            if (result.Status)
            {
                return (long) result.Data;
            }
            else
            {
                var data = (JObject) result.Data;
                throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            }
        }

        public Shared.Models.Friends.Get GetRequests()
        {
            var result =  _httpHelper.GetRequest("friends.getRequests", new Dictionary<string,string>());

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Shared.Models.Friends.Get>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Shared.Models.Friends.Get> GetRequestsAsync()
        {
            var result =  await _httpHelper.GetRequestAsync("friends.getRequests", new Dictionary<string,string>());

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Shared.Models.Friends.Get>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
       
    }
}