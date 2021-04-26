using System;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class AddToChat:ControllerBase
    {
        [HttpGet]
        public Result Add(string token, long chatId, long userId)
        {
            var result = new Result();
            var usr = Helpers.CheckerTokenHelper.GetUser(token);
            if (usr is null)
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
                var chat = db.GroupChats.SingleOrDefault(c => c.ChatId == chatId);
                var userchat = new UsersChat();
                userchat.Messages = chat.Messanges;
                userchat.Owner = userId;
                userchat.ChatId = chatId * -1;


                var user = db.Users.SingleOrDefault(u => u.UserId == userId);

                chat.Members += $"{user.UserId},";
                //создание сервисного сообщения
                var msg = new Message();
                msg.Text = $"{usr.FirstName} {usr.LastName} добавил(а) {user.FirstName} {user.LastName} в чат";
                msg.ServiceMessage = true;
                msg.Time = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                msg.MsgId = db.Messages.Count() + 1;
                msg.ChatId = chatId * -1;
                db.Messages.Add(msg);

                chat.Messanges += $"{msg.MsgId},";
                
                userchat.LastMessage = msg.MsgId;
                db.UsersChats.Add(userchat);

                foreach (var memberStr in chat.Members.Split(","))
                {
                    if (memberStr != "")
                    {
                        var update = db.UnreadMessages.SingleOrDefault(u => u.UserId == long.Parse(memberStr));
                        update.Messages += $",{msg.MsgId}";
                    }
                }
                
                db.SaveChanges();

                result.Status = true;
                result.Data = true;
            }

            return result;
        }
    }
}