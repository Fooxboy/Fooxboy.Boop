using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class GetChatInfo : Controller
    {
        [HttpGet]
        public Result Index(long chatId)
        {
            using (var db = new DatabaseContext())
            {
                var chat = db.GroupChats.SingleOrDefault(c => c.ChatId == chatId);

                var result = new Result();
                result.Status = true;
                result.Data = chat;

                return result;
            }
        }
    }
}