using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Fooxboy.Boop.Shared.Models;
using Newtonsoft.Json;

namespace Fooxboy.Boop.SDK.Helpers
{
    public class HttpHelper
    {
        private WebClient _client;
        private string _address;
        private string _token;
        public HttpHelper(string address, string token)
        {
            _client = new WebClient();
            _address = address;
            _token = token;
        }

        public void ChangeToken(string token) => _token = token;
        public void ChangeAddress(string address) => _address = address;

        public Result GetRequest(string method, Dictionary<string,string> parameters, string root= "api")
        {
            string parametersString = "";
            foreach (var parameter in parameters) { parametersString += $"{parameter.Key}={parameter.Value}&";}
            var path = $"{_address}/{root}/{method}?{parametersString}token={_token}";
            string response = string.Empty;
            try
            {
                response = _client.DownloadString(path);

            }
            catch (NotSupportedException e)
            {
                response = new WebClient().DownloadString(path);
            }

            var result = JsonConvert.DeserializeObject<Result>(response);
            return result;
        }
        
        public async Task<Result> GetRequestAsync(string method, Dictionary<string,string> parameters, string root= "api")
        {
            string parametersString = "";
            foreach (var parameter in parameters) { parametersString += $"{parameter.Key}={parameter.Value}&";}
            var path = $"{_address}/{root}/{method}?{parametersString}token={_token}";
            string response = string.Empty;
            try
            {
                response = await _client.DownloadStringTaskAsync(path);

            }
            catch (NotSupportedException e)
            {
                response = await new WebClient().DownloadStringTaskAsync(path);
            }


            var result = JsonConvert.DeserializeObject<Result>(response);
            return result;
        }
    }
}