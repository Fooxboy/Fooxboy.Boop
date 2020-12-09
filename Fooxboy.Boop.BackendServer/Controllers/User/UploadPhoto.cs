using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fooxboy.Boop.BackendServer.Database;
using Fooxboy.Boop.BackendServer.Services;
using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.User
{
    [Produces("application/json")]
    [Route("api/users.[controller]")]
    [ApiController]
    public class UploadPhoto : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private Logger _logger;

        public UploadPhoto(Logger logger, IWebHostEnvironment app)
        {
            _logger = logger;
            _appEnvironment = app;
        }
        
        [HttpPost("{key}"), DisableRequestSizeLimit]
        public async Task<Result> Index(string key, IFormFile file)
        {
            var result = new Result();
            _logger.Debug($"users.uploadPhoto?&key={key}");
            using (var db = new DatabaseContext())
            {
                var user = db.Users.SingleOrDefault(u => u.KeyUploadAvatar == key);

                if (user is null)
                {
                    var error = new Error();
                    error.Code = 2;
                    error.Message = "Неверный токен.";

                    result.Data = error;
                    result.Status = false;

                    return result;
                }

                var pathFile = $"/avatars/{user.UserId}/{file.FileName}";

                if (!System.IO.Directory.Exists(_appEnvironment.WebRootPath + $"/avatars/{user.UserId}"))
                {
                    System.IO.Directory.CreateDirectory(_appEnvironment.WebRootPath + $"/avatars/{user.UserId}");
                }
                
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathFile, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                user.PathProfilePic = pathFile;
                result.Data = pathFile;
                result.Status = true;
                return result;
            }

        }
    }
}