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
    public class Remove : Controller
    {
        public Logger _logger;

        public Remove(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Index(string token, long id)
        {
            _logger.Debug($"friends.remove?id={id}&token={token}");
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
                var user = db.Users.Single(u => u.Token == token);
                var removeUser = db.Users.SingleOrDefault(u => u.UserId == id);
                if (removeUser is null)
                {
                    var error = new Error();
                    error.Code = 34;
                    error.Message = "Пользователя с таким ID не существует.";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }

                var friendsUser = new List<long>();
                foreach (var friendIdString in user.Friends.Split(","))
                {
                    try
                    {
                        friendsUser.Add(long.Parse(friendIdString));
                    }catch(InvalidCastException) {}
                }

                if (friendsUser.Any(u => u != id))
                {
                    var error = new Error();
                    error.Code = 60;
                    error.Message = "Пользователя нет  в списке ваших друзей";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }

                var friendsRemoveUser = new List<long>();
                foreach (var friendIdString in removeUser.Friends.Split(","))
                {
                    try
                    {
                        friendsRemoveUser.Add(long.Parse(friendIdString));
                    }catch(FormatException) {}
                }


                friendsUser.Remove(id);
                friendsRemoveUser.Remove(user.UserId);

                var stringFriendsUser = string.Empty;
                foreach (var friend in friendsUser)
                {
                    stringFriendsUser += $"{friend},";
                }

                var stringRemoveFriendsUser = string.Empty;
                foreach (var friend in friendsRemoveUser)
                {
                    stringRemoveFriendsUser += $"{friend},";
                }

                user.Friends = stringFriendsUser;
                removeUser.Friends = stringRemoveFriendsUser;
                
                db.SaveChanges();

                result.Status = true;
                result.Data = true;

                return result;

            }
        }
    }
}