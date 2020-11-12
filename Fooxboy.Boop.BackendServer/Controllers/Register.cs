using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Helpers;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.BackendServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private Logger _logger;
        public Register(Logger logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public Result Get(string login, string password, string name, string lastName, string number)
        {
            _logger.Info("Запрос на регистрацию.");
            var result = new Result();
            using (var db = new DatabaseContext())
            {
                if (db.Users.Any(u => u.Nickname.ToLower() == login.ToLower()))
                {
                    //Такой пользователь уже зарегестрирован
                    var error = new Error();
                    error.Code = 3;
                    error.Message = "Этот никнейм уже занят.";
                    result.Data = error;
                    result.Status = false;
                }


                if (db.Users.Any(u => u.Number == number))
                {
                    var error = new Error();
                    error.Code = 3;
                    error.Message = "Такой номер телефона уже используется.";
                    result.Data = error;
                    result.Status = false;
                }
                
                //Регистрация

                try
                {
                    var user = new Database.User();
                    user.Nickname = login;
                    user.FirstName = name;
                    user.LastName = lastName;
                    user.Number = number;
                    user.PasswordHash = password.GetSHA256();
                    user.UserId = db.Users.Count() + 1;
                    user.Token = user.PasswordHash.GetSHA256() + new Random().Next(1, 999999999) ;

                    db.Users.Add(user);
                    db.SaveChanges();


                    var response = new Shared.Models.Register();
                    response.Token = user.Token;
                    response.UserId = user.UserId;
                    result.Data = response;
                    result.Status = true;
                    
                    _logger.Info($"Зарегистрирован новый пользователь {user.Nickname}");
                }
                catch (Exception e)
                {
                    _logger.Error("Register", e);
                    var error = new Error();
                    error.Code = 1;
                    error.Message = "Неизвестная ошибка";
                }
            }
            return result;
        }
    }
}
