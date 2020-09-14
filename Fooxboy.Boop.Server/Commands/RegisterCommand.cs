using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Helpers;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Server.Services;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.Server.Commands
{
    public class RegisterCommand:ICommand
    {
        public string Command => "reg";
        public int Id => 1;
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            var response = new ResponseCommand();

            if (!(data is Register))
            {
                response.TypeData = "error";
                response.Data = 5;
            } 
            var model = (Shared.Models.Register) data;

            using (var db = new ServerData())
            {
                //Проверка nickname
                if (db.Users.Any(u => u.Nickname == model.Nickname))
                {
                    response.Data = 3;
                    response.TypeData = "error";

                    return response;
                }

                //проверка number 
                if (db.Users.Any(u => u.Number == model.Number))
                {
                    response.Data = 4;
                    response.TypeData = "error";

                    return response;
                }

                //Регистрация
                try
                {
                    var userNew = new User();

                    userNew.Nickname = model.Nickname;
                    userNew.Number = model.Number;
                    userNew.FirstName = model.FirstName;
                    userNew.LastName = model.LastName;
                    userNew.PasswordHash = model.Password.GetSHA256();
                    userNew.UserId = db.Users.Count() + 1;
                    userNew.Token = userNew.PasswordHash.GetSHA256();

                    db.Users.Add(userNew);
                    db.SaveChanges();

                    var respOk = new RegisterResponse();
                    respOk.Token = userNew.Token;
                    respOk.UserId = userNew.UserId;
                    response.Data = respOk;
                    response.TypeData = "reg";
                    
                    logger.Warning($"Зарегистрирован новый пользователь: ID = {userNew.UserId}");
                }
                catch (Exception e)
                {
                    //Неизвестаня ошибка
                    response.Data = e.Message;
                    response.TypeData = "exception";
                    return response;
                }
            }

            return response;
        }
    }
}