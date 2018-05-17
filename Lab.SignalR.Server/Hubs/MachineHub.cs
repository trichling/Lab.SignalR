using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.Server.Hubs 
{
    public class MachineKey 
    {

        public MachineKey(string group, string name)
        {
            this.Group = group;
            this.Name = name;
        }

        public string Group { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as MachineKey;

            if (other == null)
                return false;

            return Group.Equals(other.Group) && Name.Equals(other.Name);
        }

        public override int GetHashCode() {
            return this.Group.GetHashCode() ^ this.Name.GetHashCode();
        }
    }

    // https://weblogs.asp.net/ricardoperes/signalr-in-asp-net-core
    public class MachineHub : Hub
    {

        private static Dictionary<MachineKey, string> machineConnections = new Dictionary<MachineKey, string>();

        public override async Task OnConnectedAsync() 
        {
            var group = this.Context.Connection.GetHttpContext().Request.Query["group"].FirstOrDefault();
            var name = this.Context.Connection.GetHttpContext().Request.Query["name"].FirstOrDefault();

            if (group != null && name != null)
            {
                var key = new MachineKey(group, name);
                if (!machineConnections.ContainsKey(key))
                    machineConnections.Add(key, this.Context.ConnectionId);

                machineConnections[key] = this.Context.ConnectionId;
            }
            else if (group != null)
                await this.Groups.AddAsync(this.Context.ConnectionId, group);
            
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