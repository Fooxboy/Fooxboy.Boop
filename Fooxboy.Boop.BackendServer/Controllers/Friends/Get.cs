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
    public class Get : Controller
    {
        private Logger _logger;
        public Get(Logger logger)
        {
            _logger = logger;
        }
        
        
        [HttpGet]
        public Result Index(string token)
        {
            _logger.Debug($"friends.get?&token={token}");
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
                var friendsIds = new List<long>();

                foreach (var idString in user.Friends.Split(","))
                {
                    try
                    {
                        friendsIds.Add(long.Parse(idString));
                    }catch(InvalidCastException) {}
                }
                
                var data = new Shared.Models.Friends.Get();
                data.Friends = new List<Shared.Models.Users.User>();
                foreach (var friendId in friendsIds)
                {
                    var friend = new Shared.Models.Users.User();
                    var f = db.Users.Single(u => u.UserId == friendId);
                    friend.Nickname = f.Nickname;
                    friend.Number = f.Number;
                    friend.FirstName = f.FirstName;
                    friend.LastName = f.LastName;
                    friend.UserId = f.UserId;
                    friend.PathProfilePic = f.PathProfilePic;
                    data.Friends.Add(friend);
                }

                result.Status = true;
                result.Data = data;
                return result;
            }
        }
    }
}