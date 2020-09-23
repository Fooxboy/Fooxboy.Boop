using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Login
    {
        public event EventDelegate LognEvent;
        
        public void Logn(string login, string password, bool reset)
        {
            var model = new Shared.Models.Login();
            model.Nickname = login;
            model.Password = password;
            model.ResetAuth = reset;
            
            SenderHelper.GetHelper().Send("log", model, "log");
        }
    }
}