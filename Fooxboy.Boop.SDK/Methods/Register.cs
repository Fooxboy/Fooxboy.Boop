using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Register
    {
        public event EventDelegate RegEvent;
        
        public void Reg(string nickname, string number, string password, string firstName, string lastName)
        {
            var model = new Shared.Models.Register();
            model.Nickname = nickname;
            model.Number = number;
            model.Password = password;
            model.FirstName = firstName;
            model.LastName = lastName;
            
            SenderHelper.GetHelper().Send("reg", model, "reg");
        }
    }
}