using System;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class CreateChat : ControllerBase
    {
        [HttpGet]
        public Result Create(string token, string name)
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
                var chat = new GroupChat();
                chat.Admin = usr.UserId;
                chat.Members = $"{usr.UserId},";
                
                //создание тестового сообщения..
                var msg = new Message();
                msg.Text = $"{usr.FirstName} {usr.LastName} создал(а) групповой чат {name}";
                msg.ServiceMessage = true;
                msg.Time = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                msg.MsgId = db.Messages.Count() + 1;
                db.Messages.Add(msg);
                
                chat.Messanges = $"{msg.MsgId},";
                chat.Title = name;
                chat.ChatId = db.GroupChats.Count() + 1;
                db.GroupChats.Add(chat);
                
                var userchat = new UsersChat();
                userchat.Messages = chat.Messanges;
                userchat.Owner = usr.UserId;
                userchat.LastMessage = msg.MsgId;
                userchat.ChatId = chat.ChatId * -1;
                db.UsersChats.Add(userchat);

                

                db.SaveChanges();

                result.Data = chat;

                result.Status = true;
                return result;
            }
        }
    }
}