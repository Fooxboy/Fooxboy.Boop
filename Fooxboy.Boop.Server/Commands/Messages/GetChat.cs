using System;
using System.Collections.Generic;
using System.Linq;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Message = Fooxboy.Boop.Shared.Models.Messages.Message;

namespace Fooxboy.Boop.Server.Commands.Messages
{
    public class GetChat:ICommand
    {
        public string Command => "msg.getChat";
        public int Id => 5;
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            var response = new ResponseCommand();

            if (!(data is Shared.Models.Messages.GetChat))
            {
                response.Data = 5;
                response.TypeData = "error";
                return response;
            }

            var info = (Shared.Models.Messages.GetChat) data;

            using (var db = new ServerData())
            {
                var chat = db.UsersChats.SingleOrDefault(c => c.Owner == user.UserId && c.ChatId == info.ChatId);

                if (chat is null)
                {
                    response.Data = 9;
                    response.TypeData = "error";
                    return response;
                }

                var chatMsgs = chat.Messages.Split(",").ToList();
                
                var rsp = new GetChatResponse();
                rsp.Messages = new List<Message>();

                for (var i = info.Offset; i < info.Count; i++)
                {
                    var msgId = chatMsgs[Convert.ToInt32(i)];

                    var msgDb = db.Messages.Single(m => m.MsgId == long.Parse(msgId));
                    
                    var msg = new Shared.Models.Messages.Message();
                    msg.Text = msgDb.Text;
                    msg.Time = msgDb.Time;
                    msg.ChatId = msgDb.ChatId;
                    msg.MsgId = msgDb.MsgId;
                    msg.RecieverId = msgDb.RecieverId;
                    msg.SenderId = msgDb.SenderId;
                    msg.UsersReaded = msgDb.UsersReaded;
                    
                    rsp.Messages.Add(msg);
                }
                response.Data = rsp;
            }
            
            response.TypeData = "getChat";
            return response;
            
        }
    }
}