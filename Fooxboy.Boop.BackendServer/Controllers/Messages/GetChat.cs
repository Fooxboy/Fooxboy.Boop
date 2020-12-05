using System;
using System.Collections.Generic;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Helpers;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Microsoft.AspNetCore.Mvc;
using Message = Fooxboy.Boop.Shared.Models.Messages.Message;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class GetChat : ControllerBase
    {
        
        private Logger _logger;

        public GetChat(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result GetChatMethod(long chatId, long count, long offset, string token)
        {
            _logger.Debug($"msg.getChat?chatId={chatId}&count={count}&offset={offset}&token={token}");
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
            
            _logger.Debug("Обращение к бд.");
            using (var db = new DatabaseContext())
            {
                var chat = db.UsersChats.SingleOrDefault(c => c.Owner == usr.UserId && c.ChatId == chatId);

                if (chat is null)
                {
                    var error = new Error();
                    error.Code = 9;
                    error.Message = "Такого чата не существует.";
                    result.Data = error;
                    result.Status = false;

                    return result;
                }

                _logger.Debug("Поиск сообщений...");
                var chatMsgs = chat.Messages.Split(",").ToList();
                _logger.Debug($"db: {chat.Messages}");
                var chatModel = new Shared.Models.Messages.GetChat();
                chatModel.Messages = new List<Message>();

                try
                {
                    for (var i = offset; i < count; i++)
                    {
                        var msgId = chatMsgs[Convert.ToInt32(i)];
                        _logger.Debug($"db id msg: {msgId}");
                        if (msgId == "")
                        {
                            _logger.Debug("Пропуск числа.");
                            goto blep;
                        }
                        var id = long.Parse(msgId);
                        var msgDb = db.Messages.SingleOrDefault(m => m.MsgId == id);
                        chatModel.Messages.Add(msgDb.ConvertToMessage());
                        
                        blep:
                        int i2 = 0;

                    }
                }
                catch (Exception e)
                {
                    _logger.Error("GetChat", e);
                }
                
                _logger.Debug($"Отправка {chatModel.Messages.Count} сообщений.");
                result.Data = chatModel;
                result.Status = true;
                return result;
            }
            
        }
    }
}