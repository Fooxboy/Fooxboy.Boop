using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Helpers;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private Logger _logger;

        public Login(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Get(string login, string password, bool resetAuth = false)
        {
            _logger.Info("Запрос на авторизацию.");
            var result = new Result();
            using (var db = new DatabaseContext())
            {
                if (db.Users.Any(u => u.Nickname.ToLower() == login.ToLower()))
                {
                    var user = db.Users.Single(u => u.Nickname.ToLower() == login.ToLower());

                    var hashPassword = password.GetSHA256();

                    if (hashPassword.VerifySHA256(user.PasswordHash))
                    {
                        //пользователь авторизлован

                        var loginObject = new Shared.Models.Login();
                        loginObject.Token =
                            resetAuth ? (hashPassword + new Random().Next(1, 100000)).GetSHA256() : user.Token;

                        loginObject.UserId = user.UserId;

                        result.Status = true;
                        result.Data = loginObject;

                        _logger.Info($"Авторизован пользователь {user.Nickname}");

                    }
                    else
                    {
                        //Пароль не верный
                        var error = new Error();
                        error.Code = 7;
                        error.Message = "Неверный пароль";
                        result.Status = false;
                        result.Data = error;
                    }
                }
                else
                {
                    var error =new Error();
                    error.Code = 6;
                    error.Message = "Нет пользователя с таким никнеймом";
                    result.Status = false;
                    result.Data = error;
                }
            }

            return result;
        }
    }
}
