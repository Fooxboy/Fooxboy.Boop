using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.Boop.SDK;

namespace Fooxboy.Boop.Client.WpfApp.Services
{
    public class ApiService
    {
        private static Api _api;

        public static void Init(string address, string token)
        {
            _api = new Api(address, token);
        }

        public static void ChangeToken(string token)
        {
            AppGlobalConfig.Token = token;
            _api.ChangeToken(token);
        }

        public static void ChangeAddress(string address)
        {
            AppGlobalConfig.Address = address;
            _api.ChangeToken(address);
        }
        public static Api Get() => _api;
    }
}
