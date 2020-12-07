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
    public class GetCountRequests : Controller
    {
        public Logger _logger;

        public GetCountRequests(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Index(string token)
        {
            _logger.Debug($"friends.getCountRequests?&token={token}");
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

                foreach (var idString in user.FirendsRequests.Split(","))
                {
                    try
                    {
                        friendsIds.Add(long.Parse(idString));
                    }catch(FormatException) {}
                }
                result.Status = true;
                result.Data = friendsIds.Count;
                return result;
            }
        }
    }
}