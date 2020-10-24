using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Users
    {
        private HttpHelper _httpHelper;

        public Users(HttpHelper helper)
        {
            _httpHelper = helper;
        }

        public User GetInfo(long userId)
        {
             var parameters = new Dictionary<string,string>();
             parameters.Add("userId", userId.ToString());

             var result = _httpHelper.GetRequest("users.GetInfo", parameters);
             
             if (result.Status) return (User) result.Data;
             else throw new BoopRootException(((Error)result.Data).Message,((Error)result.Data).Code);
        }
        
        public async Task<User> GetInfoAsync(long userId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("userId", userId.ToString());

            var result = await _httpHelper.GetRequestAsync("users.GetInfo", parameters);
             
            if (result.Status) return (User) result.Data;
            else throw new BoopRootException(((Error)result.Data).Message,((Error)result.Data).Code);
        }
        
    }
}