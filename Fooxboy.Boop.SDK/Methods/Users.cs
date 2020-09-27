using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Models;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Users
    {
        public event EventDelegate<User> InfoEvent;
        public void Info(long userId)
        {
            var model = new Shared.Models.Users.GetInfo();
            model.UserId = userId;
            
            SenderHelper.GetHelper().Send("usr.info", model, "usr.info");
        }

        public void InvokeInfo(User model) => InfoEvent?.Invoke(model);
    }
}