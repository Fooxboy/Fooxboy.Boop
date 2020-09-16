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
                        //Проверка есть ли такой пользователь вообще.

                        if (db.Users.Any(u => u.UserId == msg.ChatId))
                        {
                            //Пользователь есть
                            var usr = db.Users.Single(u => u.UserId == msg.ChatId);
                            
                            //Проверяем есть ли чат с этим пользователем.
                            if(db.UsersChats.Any(c=> c.Owner == user.UserId && c.ChatId == usr.UserId))
                            {
                                //ничего не делаем
                            }else
                            {
                                //Создавать чат для отправителя.
                                
                                var usrChat = new UsersChat();
                                usrChat.Messages = "";
                                usrChat.Owner = user.UserId;
                                usrChat.ChatId = usr.UserId;
                                usrChat.LastMessage = 0;
                                usrChat.LocalId = db.UsersChats.Count() + 1;
                                db.UsersChats.Add(usrChat);

                                db.SaveChanges();
                                
                                
                                //Создание чата для принимающего.
                                
                                var usrChat2 = new UsersChat();
                                usrChat2.Messages = "";
                                usrChat2.Owner = usr.UserId;
                                usrChat2.LastMessage = 0;
                                usrChat2.ChatId = user.UserId;
                                usrChat2.LocalId = db.UsersChats.Count() + 1;
                                db.UsersChats.Add(usrChat2);
                            }


                            //Отправляем ему сообщение

                            var msgObj = new Database.Message()
                            {
                                MsgId = db.Messages.Count() + 1,
                                ChatId = 0,
                                RecieverId = usr.UserId,
                                SenderId = user.UserId,
                                Text = msg.Text,
                                Time = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds
                            };
                            
                            //Добавление сообщения в бд
                            db.Messages.Add(msgObj);
                            
                            //Добавление id сообщения в список.

                            var chats1 = db.UsersChats.Single(c => c.Owner == user.UserId && c.ChatId == usr.UserId);
                            chats1.Messages += $",{msgObj}";
                            chats1.LastMessage = msgObj.Time;

                            var chats2 = db.UsersChats.Single(c => c.Owner == usr.UserId && c.ChatId == user.UserId);
                            chats2.Messages += $",{msgObj}";
                            chats2.LastMessage = msgObj.Time;

                            
                            db.SaveChanges();
                            var rsp = new SendResponse();
                            rsp.MsgId = msgObj.MsgId;

                            response.Data = rsp;
                            response.TypeData = "msg.snd";
                            return response;
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
                    
                }
            }
            else
            {
                return null;
            }
        }
    }
}