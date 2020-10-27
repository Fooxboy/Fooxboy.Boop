using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Newtonsoft.Json.Linq;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Register
    {
        private HttpHelper _httpHelper;

        public Register(HttpHelper helper)
        {
            _httpHelper = helper;
        }
        public Shared.Models.Register Start(string login, string password, string firstName, string lastName, string number)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("login", login);
            parameters.Add("password", password);
            parameters.Add("name", firstName);
            parameters.Add("lastName", lastName);
            parameters.Add("number", number);

            var result = _httpHelper.GetRequest("register", parameters);

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<Shared.Models.Register>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<Shared.Models.Register> StartAsync(string login, string password, string firstName, string lastName, string number)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("login", login);
            parameters.Add("password", password);
            parameters.Add("name", firstName);
            parameters.Add("lastName", lastName);
            parameters.Add("number", number);

            var result = await _httpHelper.GetRequestAsync("register", parameters);

            var data = (JObject) result.Data;

            if (result.Status) return data.ToObject<Shared.Models.Register>();
            else throw new BoopRootException(data?.ToObject<Error>().Message,(data?.ToObject<Error>()).Code);
        }
    }
}