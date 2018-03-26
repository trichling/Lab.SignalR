using Microsoft.AspNetCore.Mvc;

namespace Dnp.SignalR.Server.Controllers 
{

    [Route("chat")]
    public class ChatController : Controller
    {
        
        [Route("index")]
        [HttpGet]
        public IActionResult Index(string room)
        {
            ViewData["room"] = room;
            return View();
        }

    }

}