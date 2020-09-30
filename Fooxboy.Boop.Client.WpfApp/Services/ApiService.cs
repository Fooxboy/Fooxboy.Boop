using System;
using System.Collections.Generic;
using System.Text;
using Fooxboy.Boop.SDK;

namespace Fooxboy.Boop.Client.WpfApp.Services
{
    public class ApiService
    {
        private static Api _api;
        public static Api GetApi()
        {
            _api ??= new Api("127.0.0.1", 2020, null);

            _api.Connect();
            return _api;
        }

        public static void ResetApi(Api api)
        {
            _api = api;
        }
    }
}
