using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class UploadAttach : ControllerBase
    {
        IWebHostEnvironment _appEnvironment;
        private Logger _logger;
        
        public UploadAttach(Logger logger, IWebHostEnvironment app)
        {
            _logger = logger;
            _appEnvironment = app;
        }

        [HttpPost("{key}"), DisableRequestSizeLimit]
        public async Task<Result> Index(long key, IFormFile file)
        {
            try
            {
                var result = new Result();
                _logger.Debug($"msg.uploadAttach?&key={key}");
                using (var db = new DatabaseContext())
                {
                    var attach = db.UploadAttachments.SingleOrDefault(k => k.AttachmentKey == key);

                    var pathFile =
                        $"/attachments/{attach.User}_{attach.ChatId}/{new Random().Next(0, 99999999)}--{file.FileName.Replace(" ", "_")}";

                    if (!System.IO.Directory.Exists(_appEnvironment.WebRootPath +
                                                    $"/attachments/{attach.User}_{attach.ChatId}/"))
                    {
                        System.IO.Directory.CreateDirectory(_appEnvironment.WebRootPath +
                                                            $"/attachments/{attach.User}_{attach.ChatId}/");
                    }

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    attach.File = pathFile;

                    db.SaveChanges();
                    result.Data = attach.AttachmentId;
                    result.Status = true;
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
           

        }
    }
}