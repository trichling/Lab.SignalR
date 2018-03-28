using System;
using System.Collections.Generic;
using System.Linq;
using Lab.SignalR.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab.SignalR.Server.Controllers 
{

    [Route("machines")]
    public class MachinesController : Controller
    {

        private static Dictionary<string, List<Machine>> _machines = new Dictionary<string, List<Machine>>() {
            { "Webmaschinen", new List<Machine> { new Machine("Webmaschinen", "Weber 1"), new Machine("Webmaschinen", "Weber 2"), new Machine("Webmaschinen", "Weber Turbo") } },
            { "Druckmaschinen", new List<Machine> { new Machine("Druckmaschinen", "Drucker 1"), new Machine("Druckmaschinen", "Drucker 2") } }
        };

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

    }

}