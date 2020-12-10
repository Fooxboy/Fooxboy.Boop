using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Fooxboy.Boop.Shared.Models.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class Get: ControllerBase
    {
        private Logger _logger;
        public Get(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Result GetMethod(int count, bool onlyUnread, int offset, string token)
        {
            
            _logger.Debug($"msg.get?count={count}&onlyUnread={onlyUnread}&offset={offset}&token={token}");
            var result = new Result();
            
            if (Helpers.CheckerTokenHelper.GetUser(token) is null)
            {
                _logger.Debug("Пользователь указал не верный токен");
                var error = new Error();
                error.Code = 2;
                error.Message = "Неверный токен.";

                result.Data = error;
                result.Status = false;

                return result;
            }

            using (var db = new DatabaseContext())
            {
                _logger.Debug("Поиск пользователя...");
                var user = db.Users.Single(u => u.Token == token);
                _logger.Debug($"Найден пользователь {user.Nickname}");
                _logger.Debug($"Загрузка диалогов..");
                var chats = db.UsersChats.Where(c => c.Owner == user.UserId).OrderBy(c => c.LastMessage).ToList();
                _logger.Debug($"Загружено {chats.Count} штук.");
                var response = new Shared.Models.Messages.Get();
                response.Chats = new List<GetResponse>();

                var countLoadingDialogs = count < chats.Count ? count : chats.Count;
                
                _logger.Debug($"обрабатываем {countLoadingDialogs} диалогов");
                
                for (int i = offset; i < countLoadingDialogs; i++)
                {
                    _logger.Debug($"i = {i}");
                    var chat = chats[i];
                    var msgResponse = new GetResponse();
                    msgResponse.ChatId = chat.ChatId;

                  

                    if (chat.Messages != "" && msgResponse.ChatId > 0)
                    {

                        var usr = db.Users.Single(u => u.UserId == msgResponse.ChatId);
                        msgResponse.ChatTitle = usr.FirstName + " " + usr.LastName;
                        msgResponse.CoverChat = usr.PathProfilePic;
                        
                        _logger.Debug($"Chat Title: {msgResponse.ChatTitle}");
                        long id = 0;
                        try
                        {
                            id = long.Parse(chat.Messages.Split(",").Last());
                        }
                        catch
                        {
                            
                        }

                        var lastMsgId = id;
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
