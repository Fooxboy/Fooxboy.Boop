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
    public class Confirm : Controller
    {
        private Logger _logger;

        public Confirm(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Index(string token, long id)
        {
            _logger.Debug($"friends.confirm?id={id}&token={token}");
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
                var userRequest = db.Users.SingleOrDefault(u => u.UserId == id);
                
                if (userRequest is null)
                {
                    var error = new Error();
                    error.Code = 34;
                    error.Message = "Пользователя с таким ID не существует.";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }

                var userRequests = new List<long>();
                foreach (var idString in user.FirendsRequests.Split(","))
                {
                    try { userRequests.Add(long.Parse(idString));} 
                    catch(InvalidCastException) {}
                }

                if (userRequests.All(r => r != id))
                {
                    var error = new Error();
                    error.Code = 40;
                    error.Message = "Пользователь не запрашивал запрос в друзья.";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }

                var userRequested = new List<long>();
                foreach (var idString in userRequest.FriendsRequested.Split(","))
                {
                    try { userRequested.Add(long.Parse(idString));} 
                    catch(InvalidCastException) {}
                }

                try
                {
                    userRequested.Remove(user.UserId);
                }
                catch (Exception e)
                {
                    _logger.Error("Friends.Condirm", e);
                    var error = new Error();
                    error.Code = 55;
                    error.Message = "Невозможно добавить этого пользователя в друзья";
                    result.Status = false;
                    result.Data = error;
                    return result;
                }

                userRequests.Remove(id);

                var userRequestedString = string.Empty;
                foreach (var idUser in userRequested)
                {
                    userRequestedString += $"{idUser},";
                }

                var userRequestsString = string.Empty;

                foreach (var idUser in userRequests)
                {
                    userRequestsString += $"{idUser},";
                }

                user.FirendsRequests = userRequestsString;
                userRequest.FriendsRequested = userRequestedString;

                user.Friends += $"{id},";
                userRequest.Friends += $"{user.UserId},";

                db.SaveChanges();

                result.Data = true;
                result.Status = true;

                return result;
            }
        }
    }
}