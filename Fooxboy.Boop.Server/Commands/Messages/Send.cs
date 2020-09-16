using System;
using System.Linq;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Models;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Server.Commands.Messages
{
    public class Send :ICommand
    {
        public string Command => "msg.send";
        public int Id => 3;
        public ResponseCommand Execute(object data, User user, ILoggerService logger)
        {
            var response = new ResponseCommand();
            if (!(data is Shared.Models.Messages.Send))
            {
                response.Data = 5;
                response.TypeData = "error";
                return response;
            }

            var msg = (Shared.Models.Messages.Send) data;

            if (msg.ChatId > 0)
            {
                //Отправка сообщения пользователю.

                using (var db = new ServerData())
                {
                    try
                    {
                        if (db.Users.Any(u => u.UserId == msg.ChatId))
                        {
                            //Пользователь есть

                            //Отправляем ему сообщение

                            var usr = db.Users.Single(u => u.UserId == msg.ChatId);
                            var msgObj = new Message()
                            {
                                MsgId = db.Messages.Count() + 1,
                                ChatId = 0,
                                RecieverId = usr.UserId,
                                SenderId = user.UserId,
                                Text = msg.Text,
                                Time = DateTime.Now.ToString()
                            };
                            
                            //Добавление сообщения в бд
                            db.Messages.Add(msgObj);
                            usr.Chats += $",{user.UserId}";
                            db.SaveChanges();
                            var rsp = new SendResponse();
                            rsp.MsgId = msgObj.MsgId;
                            
                            
                            //Todo: сделать оповещение юзера.
                        }
                        else
                        {
                            //Пользователя с таким Id не существует.
                            response.Data = 8;
                            response.TypeData = "error";
                            return response;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error("Произошла ошибка при отправке сообщения", e);
                        response.Data = e.Message;
                        response.TypeData = "exception";
                        return response;
                    }
                    //Проверка есть ли такой пользователь вообще.
                    
                }
            }
        }
    }
}