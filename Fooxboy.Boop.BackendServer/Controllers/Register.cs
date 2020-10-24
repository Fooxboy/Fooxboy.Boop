﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Helpers;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.BackendServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        [HttpGet]
        public Result Get(string login, string password, string name, string lastName, string number)
        {
            var result = new Result();
            using (var db = new DatabaseContext())
            {
                if (db.Users.Any(u => u.Nickname == login))
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
                    user.Token = user.PasswordHash.GetSHA256();

                    db.Users.Add(user);
                    db.SaveChanges();


                    var response = new Shared.Models.Register();
                    response.Token = user.Token;
                    response.UserId = user.UserId;
                    result.Data = response;
                    result.Status = true;
                }
                catch (Exception e)
                {
                    var error = new Error();
                    error.Code = 1;
                    error.Message = "Неизвестная ошибка";

                }


            }

            return result;

        }
    }
}
