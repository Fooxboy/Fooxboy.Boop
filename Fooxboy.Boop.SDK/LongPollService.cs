using System.Collections.Generic;
using System.Threading.Tasks;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Newtonsoft.Json.Linq;

namespace Fooxboy.Boop.SDK
{
    public class LongPollService
    {
        private bool _isRun;
        private HttpHelper _httpHelper;
        public event Delegates.NewMessagesDelegate NewMessageEvent;
        
        public LongPollService(string address, string token)
        {
            _httpHelper = new HttpHelper(address, token);
        }
        
        public void StartService()
        {
            if (_isRun) return;
            Task.Run(() =>
            {
                _isRun = true;
                while (_isRun)
                {
                    var parameters = new Dictionary<string, string>();
                    var result = _httpHelper.GetRequest("msg.getUpdates", parameters);

                    var data = (JObject) result.Data;

                    GetUpdates updates;
                    
                    if (result.Status) updates = data.ToObject<GetUpdates>();
                    else throw new BoopRootException(data?.ToObject<Error>()?.Message, (data?.ToObject<Error>()).Code);

                    if (updates.Messages.Count > 0)
                    {
                        NewMessageEvent?.Invoke(updates.Messages);
                    }
                }
            });
        }

        public void StopService()
        {
            _isRun = false;
        }
    }
}