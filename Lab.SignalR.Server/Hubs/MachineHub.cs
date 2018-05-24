using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.Server.Hubs
{

    // Authentication Scheme: Identity.Application
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes="Cookies,Bearer,Identity.Application")]
    //[Authorize]
    public class MachineHub : Hub
    {

        private static Dictionary<MachineKey, string> machineConnections = new Dictionary<MachineKey, string>();

        public override async Task OnConnectedAsync() 
        {
            var user = this.Context.User;
            var group = this.Context.GetHttpContext().Request.Query["group"].FirstOrDefault();
            var name = this.Context.GetHttpContext().Request.Query["name"].FirstOrDefault();

            if (group != null && name != null)
            {
                var key = new MachineKey(group, name);
                if (!machineConnections.ContainsKey(key))
                    machineConnections.Add(key, this.Context.ConnectionId);

                machineConnections[key] = this.Context.ConnectionId;
            }
            else if (group != null)
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, group);
            
            await base.OnConnectedAsync();
        }

        public async Task ReportMachineSpeed(string group, string machine, double speed)
        {
            await Clients.Group(group).SendAsync("MachineSpeedReported", group, machine, speed);
        }

        public async Task NotifyMachine(string group, string machine, string message)
        {
            var key = new MachineKey(group, machine);
            var connectionFound = machineConnections.TryGetValue(key, out var connectionId);

            if (connectionFound)
                await Clients.Client(connectionId).SendAsync("NotifyMachine", group, machine, message);
        }

        public async Task NotifyGroup(string group, string message)
        {
            await Clients.OthersInGroup(group).SendAsync("NotifyGroup", group, message);
        }

        public async Task NotifyAll(string message)
        {
            await Clients.All.SendAsync("NotifyAll", message);
        }
    }

}