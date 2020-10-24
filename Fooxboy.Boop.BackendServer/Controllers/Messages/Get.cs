using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class Get : ControllerBase
    {
        [HttpGet]
        public Result GetMethod(int count, bool onlyUnread, int offset, string token)
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
                var user = db.Users.Single(u => u.Token == token);

                var chats = db.UsersChats.Where(c => c.Owner == user.UserId).OrderBy(c => c.LastMessage).ToList();
                
                var response = new Shared.Models.Messages.Get();
                response.Chats = new List<GetResponse>();
                
                for (int i = offset; i < count; i++)
                {
                    var chat = chats[i];
                    var msgResponse = new GetResponse();
                    msgResponse.ChatId = chat.ChatId;

                    if (msgResponse.ChatId > 0)
                    {
                        var usr = db.Users.Single(u => u.UserId == msgResponse.ChatId);
                        msgResponse.ChatTitle = usr.FirstName + " " + usr.LastName;

                        var lastMsgId = long.Parse(chat.Messages.Split(",").Last());
                        var msg = db.Messages.Single(m => m.MsgId == lastMsgId);

                        msgResponse.LastMessageText = msg.Text;
                        msgResponse.Time = msg.Time;
                        msgResponse.CountUnreadMessages = 0; //todo: добавть проверку на непрочитанные сообщения.
                    }
                    else
                    {
                        //todo: работа с беседами
                    }
                    
                    response.Chats.Add(msgResponse);
                }

                result.Data = response;
                result.Status = true;

                return result;
            }
        }
    }
}
