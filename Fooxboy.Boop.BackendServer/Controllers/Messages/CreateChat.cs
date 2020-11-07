using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    [Route("api/msg.[controller]")]
    [ApiController]
    public class CreateChat : Controller
    {
        [HttpGet]
        public Result Create()
        {
            return null;
        }
    }
}