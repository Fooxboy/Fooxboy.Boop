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
    public class GetId : Controller
    {
        private Logger _logger;
        public GetId(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result Get(string nickname, string token)
        {
            _logger.Debug($"users.getId?nickname={nickname}&token={token}");
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
                var user = db.Users.SingleOrDefault(u => u.Nickname.ToLower() == nickname.ToLower());

                if (user is null)
                {
                    var error = new Error();
                    error.Code = 100;
                    error.Message = "Пользователя с таким никнеймом не найдено.";
                    result.Data = error;
                    result.Status = false;

                    return result;
                }

                result.Data = user.UserId;
                result.Status = true;
                return result;
            }
        }
    }
}