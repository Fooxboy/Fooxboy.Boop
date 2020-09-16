using System;
using System.Collections.Generic;
using System.Linq;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Server.Commands.Messages
{
    public class Get:ICommand
    {
        public string Command => "msg.get";
        public int Id => 4;
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            var response = new ResponseCommand();

            if (!(data is Shared.Models.Messages.Get))
            {
                response.TypeData = "error";
                response.Data = 5;
            }

            var model = (Shared.Models.Messages.Get) data;

            using (var db = new ServerData())
            {
                var chats = db.UsersChats.Where(c => c.Owner == user.UserId).OrderBy(c => c.LastMessage).ToList();

                var rsp = new GetResponseProxy();
                rsp.Chats = new List<GetResponse>();
                
                for (var i = model.Offset; i < model.Count; i++)
                {
                    var chat = chats[Convert.ToInt32(i)];
                    var msgn = new GetResponse();
                    msgn.ChatId = chat.ChatId;

                    if (msgn.ChatId > 0) //Чат с пользователем.
                    {
                        //получаем инфу о пользователе.
                        var usr = db.Users.Single(u => u.UserId == msgn.ChatId);
                        msgn.ChatTitle = usr.FirstName + " " + usr.LastName;
                        
                        //Получем последнее сообщение.
                        var lastMsgId = long.Parse(chat.Messages.Split(",").Last());
                        var msg = db.Messages.Single(m => m.MsgId == lastMsgId);

                        msgn.LastMessageText = msg.Text;
                        msgn.Time = msg.Time;
                        msgn.CountUnreadMessages = 0; //TODO: добавить проверку на непрочитаныне сообщения.
                    }
                    else
                    {
                        //TODO: работа с беседами
                    }
                    rsp.Chats.Add(msgn);
                }

                response.Data = rsp;
                response.TypeData = "msg.get";

                return response;
            }
        }
    }
}