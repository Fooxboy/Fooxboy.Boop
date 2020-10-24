using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Login
    {
        
        private HttpHelper _httpHelper;

        public Login(HttpHelper helper)
        {
            _httpHelper = helper;
        }

        public Shared.Models.Login Start(string login, string password, bool resetAuth = false)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("login", login);
            parameters.Add("password", password);
            parameters.Add("resetAuth", resetAuth.ToString());

            var result = _httpHelper.GetRequest("login", parameters);
            
            if (result.Status) return (Shared.Models.Login) result.Data;
            else throw new BoopRootException(((Error)result.Data).Message,((Error)result.Data).Code);
        }
        
        public async Task<Shared.Models.Login> StartAsync(string login, string password, bool resetAuth = false)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("login", login);
            parameters.Add("password", password);
            parameters.Add("resetAuth", resetAuth.ToString());

            var result = await _httpHelper.GetRequestAsync("login", parameters);
            
            if (result.Status) return (Shared.Models.Login) result.Data;
            else throw new BoopRootException(((Error)result.Data).Message,((Error)result.Data).Code);
        }
    }
}