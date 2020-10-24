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
            var response =_client.DownloadString(path);

            var result = JsonConvert.DeserializeObject<Result>(response);
            return result;
        }
        
        public async Task<Result> GetRequestAsync(string method, Dictionary<string,string> parameters, string root= "api")
        {
            string parametersString = "";
            foreach (var parameter in parameters) { parametersString += $"{parameter.Key}={parameter.Value}&";}
            var path = $"{_address}/{root}/{method}?{parametersString}token={_token}";
            var response = await _client.DownloadStringTaskAsync(path);

            var result = JsonConvert.DeserializeObject<Result>(response);
            return result;
        }
    }
}