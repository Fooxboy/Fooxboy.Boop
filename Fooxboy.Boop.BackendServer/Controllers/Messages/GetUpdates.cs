﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            try
            {
                //Проверка пользователя.
                _logger.Debug($"msg.getUpdates?token={token}");

                var result = new Result();
                var user = CheckerTokenHelper.GetUser(token);
                if (user is null)
                {
                    var error = new Error();
                    error.Code = 2;
                    error.Message = "Неверный токен.";

                    result.Data = error;
                    result.Status = false;

                    return result;
                }
                
                var updatesResult = new Shared.Models.Messages.GetUpdates();
                updatesResult.Messages = new List<Message>();
                

                using (var db = new DatabaseContext())
                {
                    for (int i = 10; i > 0; )
                    {
                        var usersChats = db.UnreadMessages.SingleOrDefault(u => u.UserId == user.UserId);

                        if (usersChats is null)
                        {
                            var error = new Error();
                            error.Code = 102;
                            error.Message = "Пользователя нет в бд.";

                            result.Data = error;
                            result.Status = false;

                            return result;
                        }

                        if (usersChats.Messages != "")
                        {
                            var ids = usersChats.Messages.Split(",");
                            List<Database.Message> messages = new List<Database.Message>();

                            foreach (var id in ids)
                            {
                                try
                                {
                                    if (id != "")
                                    {
                                        var idLong = long.Parse(id);
                                        var msg = db.Messages.SingleOrDefault(m => m.MsgId == idLong);
                                        messages.Add(msg);
                                    }
                                    
                                }
                                catch (Exception e)
                                {
                                    _logger.Debug($"id = {id}");
                                    _logger.Error("GetUpdates", e);
                                }
                            }
                            

                            foreach (var msg in messages)
                            {
                                updatesResult.Messages.Add(msg.ConvertToMessage());
                            }
                            result.Data = updatesResult;
                            result.Status = true;

                            usersChats.Messages = "";
                            db.SaveChanges();
                            
                            return result;
                            
                        }
                        else
                        {
                            i -= 1;
                            Thread.Sleep(300);
                        }
                        
                        

                    }
                }

                result.Data = updatesResult;
                result.Status = true;
                
                return result;
            }
            catch (Exception e)
            {
                _logger.Error("GetUpdates", e);
                throw e;
            }
        }
    }
}