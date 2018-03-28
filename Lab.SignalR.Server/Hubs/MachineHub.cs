using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.Server.Hubs 
{
    // https://weblogs.asp.net/ricardoperes/signalr-in-asp-net-core
    public class MachineHub : Hub
    {

        public override async Task OnConnectedAsync() 
        {
            var group = this.Context.Connection.GetHttpContext().Request.Query["group"].FirstOrDefault();
            if (group != null)
                await this.Groups.AddAsync(this.Context.ConnectionId, group);
            else
                await base.OnConnectedAsync();
        }

        public async Task ReportMachineSpeed(string group, string machine, double speed)
        {
            // //await Clients.All.SendAsync("Send", message);
            await Clients.Group(group).SendAsync("ReportMachineSpeed", group, machine, speed);
        }

    }

}