using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Lab.SignalR.Server.Hubs 
{
    public class ChatHub : Hub
    {

        public override async Task OnConnectedAsync() 
        {
            var room = this.Context.GetHttpContext().Request.Query["room"].FirstOrDefault();
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, room);
            await base.OnConnectedAsync();
        }

        public async Task Send(string room, string message)
        {
            //await Clients.All.SendAsync("Send", message);
            await Clients.Group(room).SendAsync("Send", room, message);
            
        }

    }

}