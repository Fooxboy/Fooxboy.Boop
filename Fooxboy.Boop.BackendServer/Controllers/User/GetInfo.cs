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
            
            if (Helpers.CheckerTokenHelper.GetUser(token) is null)
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
                usr.FirstName = user.Nickname;
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