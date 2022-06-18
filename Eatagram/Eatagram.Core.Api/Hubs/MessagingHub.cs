using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Hubs
{
    public class MessagingHub : Hub
    {
        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }
    }
}
