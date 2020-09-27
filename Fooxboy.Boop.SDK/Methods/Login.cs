using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Models;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Login
    {
        public event EventDelegate<LoginResponse> LognEvent;
        
        public void Logn(string login, string password, bool reset)
        {
            var model = new Shared.Models.Login();
            model.Nickname = login;
            model.Password = password;
            model.ResetAuth = reset;
            
            SenderHelper.GetHelper().Send("log", model, "log");
        }

        public void Invoke(LoginResponse data) => LognEvent?.Invoke(data);
    }
}