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
    public class GetUploadAvatarUrl : Controller
    {
        [HttpGet]
        public Result Index(string token)
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
                var usr = db.Users.Single(u => u.Token == token);
                if (usr.KeyUploadAvatar is null)
                {
                    usr.KeyUploadAvatar = new Random().Next(0, 999999999).ToString();
                    db.SaveChanges();
                    result.Data = usr.KeyUploadAvatar;
                    result.Status = true;
                    return result;
                }
                else
                {
                    result.Data = usr.KeyUploadAvatar;
                    result.Status = true;
                    return result;
                }
            }
        }
    }
}