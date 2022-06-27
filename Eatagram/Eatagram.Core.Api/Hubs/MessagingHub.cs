using Eatagram.Core.Api.Models.Requests;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Hubs
{
    public class MessagingHub : Hub
    {   
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync(Context.User.Identity.Name + " joined.");
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
