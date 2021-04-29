using Fooxboy.Boop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckCode : Controller
    {
        [HttpGet]
        public Result Index(long code)
        {
            var result = new Result();

            result.Data = code == StaticData.Code;
            result.Status = true;

            return result;
        }
    }
}