﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Fooxboy.Boop.Shared.Models.Users;
using Newtonsoft.Json.Linq;

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

            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<User>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }
        
        public async Task<User> GetInfoAsync(long userId)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("userId", userId.ToString());

            var result = await _httpHelper.GetRequestAsync("users.GetInfo", parameters);
            var data = (JObject)result.Data;

            if (result.Status) return data.ToObject<User>();
            else throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
        }

        public async Task<long> GetIdAsync(string nickname)
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("nickname", nickname);

            var result = await _httpHelper.GetRequestAsync("users.getId", parameters);
            
            try
            {
                var data = (long)result.Data;
                return data;
            }catch
            {
                var data = (JObject) result.Data;
                throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            }
        }
        
        public bool IsFriend(long userId) 
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", userId.ToString());

            var result = _httpHelper.GetRequest("users.isFriend", parameters);
            try
            {
                var data = (bool)result.Data;
                return data;
            }catch
            {
                var data = (JObject) result.Data;
                throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            }
        }
        
        public async Task<bool> IsFriendAsync(long userId) 
        {
            var parameters = new Dictionary<string,string>();
            parameters.Add("id", userId.ToString());

            var result = await _httpHelper.GetRequestAsync("users.isFriend", parameters);
            try
            {
                var data = (bool)result.Data;
                return data;
            }catch
            {
                var data = (JObject) result.Data;
                throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            }
        }

        public async Task<string> GetUploadAvatarUrl()
        {
            var parameters = new Dictionary<string,string>();

            var result = await _httpHelper.GetRequestAsync("users.getUploadAvatarUrl", parameters);
            try
            {
                var data = (string)result.Data;
                return data;
            }catch
            {
                var data = (JObject) result.Data;
                throw new BoopRootException(data?.ToObject<Error>().Message, (data?.ToObject<Error>()).Code);
            }
        }
        
    }
}