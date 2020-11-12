using System;
using System.Collections.Generic;
using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Helpers;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Message = Fooxboy.Boop.Shared.Models.Messages.Message;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class GetUpdates : Controller
    {

        public Logger _logger;

        public GetUpdates(Logger logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        // GET
        public Result Index(string token)
        {
            _logger.Debug($"msg.GetUpdates?token={token}");
            
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
                var userMessages = db.UnreadMessages.SingleOrDefault(u => u.UserId == user.UserId);

                var msgs = userMessages?.Messages.Split(",");
                
                var data = new Shared.Models.Messages.GetUpdates();
                data.Messages = new List<Message>();
                foreach (var msgIdStr in msgs)
                {
                    try
                    {
                        var msgId = long.Parse(msgIdStr);

                        var msg = db.Messages.SingleOrDefault(m => m.MsgId == msgId);
                        data.Messages.Add(msg.ConvertToMessage());

                        userMessages.Messages = "";
                        db.SaveChanges();
                        
                    }
                    catch (Exception e)
                    {
                        _logger.Error("[GetUpdates]", e);
                    }
                }

                result.Data = data;
                result.Status = true;
            }

            return result;
        }
    }
}