using System.Linq;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class GetAttachment : ControllerBase
    {
        public Result Index(long attachId)
        {
            var r = new Result();
            using (var db = new DatabaseContext())
            {
                var attach = db.UploadAttachments.SingleOrDefault(a => a.AttachmentId == attachId);
                r.Data = attach;
                r.Status = true;
            }
            return r;
        }
    }
}