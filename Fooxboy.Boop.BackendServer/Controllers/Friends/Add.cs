using System;
using System.Collections.Generic;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Friends
{
    [Produces("application/json")]
    [Route("api/friends.[controller]")]
    [ApiController]
    public class Add : Controller
    {
        private Logger _logger;
        public Add(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Index(string token, long id)
        {
            _logger.Debug($"friends.add?id={id}&token={token}");
            var result = new Result();
            
            if (Helpers.CheckerTokenHelper.GetUser(token) is null)
            {
                _logger.Debug("Пользователь указал не верный токен");
                var error = new Error();
                error.Code = 2;
                error.Message = "Неверный токен.";

                result.Data = error;
                result.Status = false;

                return result;
            }


            using (var db = new DatabaseContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Token == token);
                var userFriendRequest = db.Users.SingleOrDefault(u => u.UserId == id);

                if (userFriendRequest is null)
                {
                    var error = new Error();
                    error.Code = 34;
                    error.Message = "Пользователя с таким ID не существует.";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }
                
                var requests = new List<long>();
                foreach (var idString in user.FriendsRequested.Split(","))
                {
                    try
                    {
                        requests.Add(long.Parse(idString));
                    }catch(InvalidCastException) {}
                }

                if (requests.Any(idU => idU == id))
                {
                    var error = new Error();
                    error.Code = 35;
                    error.Message = "Вы уже отправили заявку пользователю.";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }

                user.FriendsRequested += $"{id},";
                userFriendRequest.FirendsRequests += $"{user.UserId},";

                db.SaveChanges();

                result.Status = true;
                result.Data = true;
                return result;
            }

        }
    }
}