using Microsoft.AspNetCore.Mvc;

namespace Lab.SignalR.Server.Controllers 
{

    [Route("controlcenter")]
    public class ControlCenterController : Controller
    {

        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

    }

}