﻿using System;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class Send : ControllerBase
    {
        private Logger _logger;

        public Send(Logger logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public Result Get(string text, long chatId, string token, long attach = 0)
        {
            _logger.Debug($"msg.send?text={text}&chatId={chatId}&token={token}&attach={attach}");
            var result = new Result();
            var user = Helpers.CheckerTokenHelper.GetUser(token);
            if (user is null)
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
                if (chatId > 0) //отправка в лс..
                {
                    //Проверка есть ли такой пользователь.
                    if (db.Users.Any(u => u.UserId == chatId))
                    {
                        //получение пользователя
                        var usr = db.Users.Single(u => u.UserId == chatId);

                        if (db.UsersChats.Any(c => c.Owner == user.UserId && c.ChatId == usr.UserId))
                        {
                            //Ничего не делаем
                        }
                        else
                        {
                            //Создание  чат для отправителя.

                            var userChat = new UsersChat();
                            userChat.Messages = "";
                            userChat.Owner = user.UserId;
                            userChat.ChatId = usr.UserId;
                            userChat.LastMessage = 0;
                            userChat.LocalId = db.UsersChats.Count() + 1;
                            db.UsersChats.Add(userChat);

                            db.SaveChanges();

                            //Создание чата для принимающего.

                            var userChat2 = new UsersChat();
                            userChat2.Messages = "";
                            userChat2.Owner = usr.UserId;
                            userChat2.LastMessage = 0;
                            userChat2.ChatId = user.UserId;
                            userChat2.LocalId = db.UsersChats.Count() + 1;
                            db.UsersChats.Add(userChat2);

                            db.SaveChanges();
                        }

                        //Отправлем ему сообщение

                        var msgObj = new Message()
                        {
                            MsgId = db.Messages.Count() + 1,
                            ChatId = 0,
                            RecieverId = usr.UserId,
                            SenderId = user.UserId,
                            ImageSender = user.PathProfilePic,
                            NameSender = user.FirstName + " " + user.LastName,
                            Text = text,
                            Attach = attach,
                            Time = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds
                        };

                        //Добавление сообщение в бд
                        db.Messages.Add(msgObj);

                        //Добавление id сообщения в список.

                        var chats1 = db.UsersChats.Single(c => c.Owner == user.UserId && c.ChatId == usr.UserId);
                        chats1.Messages += $",{msgObj.MsgId}";
                        chats1.LastMessage = msgObj.Time;

                        var chats2 = db.UsersChats.Single(c => c.Owner == usr.UserId && c.ChatId == user.UserId);
                        chats2.Messages += $",{msgObj.MsgId}";
                        chats2.LastMessage = msgObj.Time;

                        var update = db.UnreadMessages.SingleOrDefault(u => u.UserId == usr.UserId);
                        update.Messages += $",{msgObj.MsgId}";

                        db.SaveChanges();

                        var response = new Shared.Models.Messages.Send();
                        response.MsgId = msgObj.MsgId;

                        result.Data = response;
                        result.Status = true;
                    }
                }
                else
                {
                    var groupChatId = chatId * -1;

                    var chat = db.GroupChats.SingleOrDefault(c => c.ChatId == groupChatId);

                    var msg = new Message();
                    msg.Attach = attach;
                    msg.Text = text;
                    msg.Time = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    msg.ChatId = chatId;
                    msg.ImageSender = user.PathProfilePic;
                    msg.NameSender = user.FirstName + " " + user.LastName;
                    msg.RecieverId = chatId;
                    msg.SenderId = user.UserId;
                    msg.ServiceMessage = false;
                    msg.UsersReaded = "";
                    msg.MsgId = db.Messages.Count() + 1;

                    db.Messages.Add(msg);

                    chat.Messanges += $"{msg.MsgId},";

                    foreach (var memberStr in chat.Members.Split(","))
                    {
                        if (memberStr != "")
                        {
                            var member = long.Parse(memberStr);

                            var userChat = db.UsersChats.SingleOrDefault(c => c.Owner == member && c.ChatId == chatId);
                            userChat.Messages += $"{msg.MsgId},";
                            userChat.LastMessage = msg.MsgId;
                            
                            //уведомления о новом сообщении других пользователей.
                            var update = db.UnreadMessages.SingleOrDefault(u => u.UserId == long.Parse(memberStr));
                            update.Messages += $",{msg.MsgId}";
                        }
                    }

                    db.SaveChanges();

                    var response = new Shared.Models.Messages.Send();
                    response.MsgId = msg.MsgId;
                    result.Data = response;
                    result.Status = true;
                }

                return result;
            }
            
            
        }
    }
}