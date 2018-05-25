using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.SignalR.Server.Hubs;
using Lab.SignalR.Server.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.Server.Controllers 
{

    [Route("machines")]
    public class MachinesController : Controller
    {

        private static Dictionary<string, List<Machine>> _machines = new Dictionary<string, List<Machine>>() {
            { "Webmaschinen", new List<Machine> { new Machine("Webmaschinen", "Weber 1"), new Machine("Webmaschinen", "Weber 2"), new Machine("Webmaschinen", "Weber Turbo") } },
            { "Druckmaschinen", new List<Machine> { new Machine("Druckmaschinen", "Drucker 1"), new Machine("Druckmaschinen", "Drucker 2") } }
        };
        private readonly IHubContext<MachineHub> machineHub;

        public MachinesController(IHubContext<MachineHub> machineHub)
        {
            this.machineHub = machineHub;
        }

        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("groups")]
        public IActionResult GetAllGroups()
        {
            return Ok(_machines.Keys.ToList());
        }

        [Route("machines/{group}")]
        public IActionResult MachinesPerGroup([FromRoute] string group)
        {
            return Ok(_machines[group]);
        }

        [HttpPost]
        [Route("machines/{group}/{name}/failure")]
        public async Task<IActionResult> ReportFailure([FromRoute] string group, [FromRoute] string name)
        {
            await machineHub.Clients.All.SendAsync("NotifyAll", $"{group} - {name}: Habe Störung!");
            return Ok();
        }

    }

}