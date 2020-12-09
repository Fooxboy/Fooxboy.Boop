using System;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.User
{
    [Produces("application/json")]
    [Route("api/users.[controller]")]
    [ApiController]
    public class GetInfo : Controller
    {
        [HttpGet]
        public Result Get(long userId, string token)
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

            if (userId == 0)
            {
                var usr = new Shared.Models.Users.User();
                usr.Nickname = usrRequest.Nickname;
                usr.Number = usrRequest.Number;
                usr.FirstName = usrRequest.FirstName;
                usr.LastName = usrRequest.LastName;
                usr.UserId = userId;
                usr.PathProfilePic = usrRequest.PathProfilePic;
                usr.LastSeen = usrRequest.LastSeen;
                var time = "Был в сети " + TimeSpan.FromSeconds(usr.LastSeen).ToString(@"hh\:mm");
                usr.LastSeenText = time;
                result.Data = usr;
                result.Status = true;
                return result;
            }


            using (var db = new DatabaseContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserId == userId);

                if (user is null)
                {
                    var error =new Error();
                    error.Code = 10;
                    error.Message = "Пользователя с таким Id нет.";
                    result.Data = error;
                    result.Status = false;

                    return result;
                }
                
                var usr = new Shared.Models.Users.User();
                usr.Nickname = user.Nickname;
                usr.Number = "";
                usr.FirstName = user.FirstName;
                usr.LastName = user.LastName;
                usr.UserId = userId;
                usr.PathProfilePic = user.PathProfilePic;

                result.Data = usr;
                result.Status = true;

                return result;
            }
        }
    }
}