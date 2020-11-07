using Microsoft.AspNetCore.Mvc;

namespace Fooxboy.Boop.BackendServer.Controllers.Messages
{
    public class CreateChat : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}