using System;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class GetUploadAttachUrl : ControllerBase
    {
        [HttpGet]
        public Result Index(string token, long chatId, long typeAttach)
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
                var upa = new UploadAttachment();
                upa.User = usr.UserId;
                upa.ChatId = chatId;
                upa.TypeAttach = typeAttach;
                upa.AttachmentKey = new Random().Next(0, 999999999);

                db.UploadAttachments.Add(upa);

                db.SaveChanges();
                
                result.Status = true;

                var rsp = new GetUploadServer();
                rsp.Id = upa.AttachmentId;
                rsp.Key = upa.AttachmentKey;
                result.Data = rsp;


            }

            return result;
        }
    }
}