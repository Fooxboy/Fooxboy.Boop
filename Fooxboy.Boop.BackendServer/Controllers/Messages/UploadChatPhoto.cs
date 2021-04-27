using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class UploadChatPhoto : Controller
    {
        IWebHostEnvironment _appEnvironment;
        
        
        public UploadChatPhoto( IWebHostEnvironment app)
        {
            _appEnvironment = app;
        }
        
        [HttpPost("{key}"), DisableRequestSizeLimit]
        public async Task<Result> Index(long key, IFormFile file)
        {
            var result = new Result();
            using (var db = new DatabaseContext())
            {
                var attach = db.UploadAttachments.SingleOrDefault(k => k.AttachmentKey == key);

                var pathFile =
                    $"/chatPhoto/{attach.ChatId}/{new Random().Next(0, 99999999)}--{file.FileName.Replace(" ", "_")}";

                if (!System.IO.Directory.Exists(_appEnvironment.WebRootPath +
                                                $"/chatPhoto/{attach.ChatId}/"))
                {
                    System.IO.Directory.CreateDirectory(_appEnvironment.WebRootPath +
                                                        $"/chatPhoto/{attach.ChatId}/");
                }

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathFile, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                attach.File = pathFile;

                var chat = db.GroupChats.SingleOrDefault(c => c.ChatId == attach.ChatId);
                chat.Cover = pathFile;

                db.SaveChanges();
                result.Data = attach.AttachmentId;
                result.Status = true;
                return result;
            }
        }
    }
}