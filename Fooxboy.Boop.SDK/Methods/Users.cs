using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Users
    {
        public event EventDelegate InfoEvent;
        public void Info(long userId)
        {
            var model = new Shared.Models.Users.GetInfo();
            model.UserId = userId;
            
            SenderHelper.GetHelper().Send("usr.info", model, "usr.info");
        }
    }
}