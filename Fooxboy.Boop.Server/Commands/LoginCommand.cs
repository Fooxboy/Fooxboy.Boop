using System;
using System.Linq;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Helpers;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.Server.Commands
{
    public class LoginCommand:ICommand
    {
        public string Command => "log";
        public int Id => 2;
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            var response = new ResponseCommand();

            if (!(data is Login))
            {
                response.Data = 5;
                response.TypeData = "error";
            }

            var model = (Login) data;

            using (var db = new ServerData())
            {
                //проверка на наличие нужного логина.

                if (db.Users.Any(u => u.Nickname.ToLower() == model.Nickname.ToLower()))
                {
                    var userAuth = db.Users.Single(u => u.Nickname == model.Nickname);
                    
                    //Проверка хэша пароля
                    var hashPassword = model.Password.GetSHA256();

                    if (hashPassword.VerifySHA256(userAuth.PasswordHash))
                    {
                        //Пользователь авторизован.
                        var resp = new LoginResponse();

                        resp.Token = model.ResetAuth ? (hashPassword + new Random().Next(1, 10000)).GetSHA256() : userAuth.Token;
                        resp.UserId = userAuth.UserId;

                        response.Data = resp;
                        response.TypeData = "log";

                        return response;
                    }
                    else
                    {
                        response.Data = 7;
                        response.TypeData = "error";

                        return response;
                    }
                }
                else
                {
                    response.Data = 6;
                    response.TypeData = "error";

                    return response;
                }
            }
        }
    }
}