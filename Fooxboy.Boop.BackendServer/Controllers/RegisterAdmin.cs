using System;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Helpers;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fooxboy.Boop.BackendServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAdmin : Controller
    {
        [HttpGet]
        public Result Index(string nickname, string number, string email, string firstName, string lastName, string position, string password, long code)
        {
            var result = new Result();

            if (code != StaticData.Code)
            {
                var error = new Error();
                error.Code = 700;
                error.Message = "Неверный код доступа";
                result.Data = error;
                result.Status = false;
                return result;
            }
            
            try
            {
                using (var db = new DatabaseContext())
                {
                    var user = new Database.User();
                    user.Nickname = nickname;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Number = number;
                    user.PasswordHash = password.GetSHA256();
                    user.UserId = db.Users.Count() + 1;
                    user.Token = user.PasswordHash.GetSHA256() + new Random().Next(1, 999999999) ;
                    user.Friends = "";
                    user.FirendsRequests = "";
                    user.PathProfilePic = "avatars/Stock/usr.jpg";
                    user.FriendsRequested = "";
                    user.Status = "online";
                    user.Group = "";
                    user.Position = position;
                    user.Specialty = "";
                    user.AccessLevel = 10;
                    user.EMail = email;
                    
                    db.Users.Add(user);

                    db.UnreadMessages.Add(new UnreadMessages() {Messages = "", UserId = user.UserId});
                    db.SaveChanges();


                    var response = new Shared.Models.Register();
                    response.Token = user.Token;
                    response.UserId = user.UserId;
                    result.Data = response;
                    result.Status = true;
                }
               
            }
            catch (Exception e)
            {
                var error = new Error();
                error.Code = 1;
                error.Message = "Неизвестная ошибка";

                result.Data = error;
                result.Status = false;
                return result;
            }

            return result;
        }
    }
}