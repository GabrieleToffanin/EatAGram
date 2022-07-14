using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Entities.Chat;
using Eatagram.Core.Interfaces.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Hubs
{
    [Authorize]
    public class MessagingHub : Hub
    {
        private readonly IUserMessaging userMessaging;

        public MessagingHub(IUserMessaging userMessaging)
        {
            this.userMessaging = userMessaging;
        }

        public async override Task OnConnectedAsync()
        {
            var user = await userMessaging.GetUserByUsernameAsync(Context.User.Identity.Name);

            if (user is null)
            {
                user = new ChatUser()
                {
                    UserName = Context.User.Identity.Name
                };
                await userMessaging.AddUserToCollectionAsync(user);
            }
            else
            {
                foreach (var item in user.ConversationRooms)
                    await Groups.AddToGroupAsync(Context.ConnectionId, item.RoomName);
            }

            await base.OnConnectedAsync();
        }
    }
}
