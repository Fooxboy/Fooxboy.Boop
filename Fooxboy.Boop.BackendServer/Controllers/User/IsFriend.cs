using System;
using System.Collections.Generic;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.User
{
    [Produces("application/json")]
    [Route("api/users.[controller]")]
    [ApiController]
    public class IsFriend : Controller
    {
        private Logger _logger;

        public IsFriend(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Index(string token, long id)
        {
            var result = new Result();

            var usrRequest = Helpers.CheckerTokenHelper.GetUser(token);
            if (usrRequest is null)
            {
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
                var friendsIds = new List<long>();

                foreach (var idString in user.Friends.Split(","))
                {
                    try
                    {
                        friendsIds.Add(long.Parse(idString));
                    }catch(InvalidCastException) {}
                }

                result.Status = true;
                result.Data = friendsIds.Any(idU => idU == id);

                return result;
            }
        }
    }
}