using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Entities.Chat;
using Eatagram.Core.Interfaces.Messaging;
using Eatagram.Framework.Logger.LogSetup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Hubs
{
    [Authorize(Roles = "Member, Administrator")]
    public class MessagingHub : Hub
    {
        private readonly IUserMessaging userMessaging;

        public MessagingHub(IUserMessaging userMessaging)
        {
            this.userMessaging = userMessaging;
        }

        public async override Task<Task> OnConnectedAsync()
        {
            SeriLogger.Information("StoQua");
            var user = await userMessaging.GetUserByUsernameAsync(Context.User.Identity.Name);

            if (user is null)
            {
                user = new ChatUser()
                {
                    UserName = Context.User.Identity.Name
                };
                SeriLogger.Information("StoQua");
                await userMessaging.AddUserToCollectionAsync(user);
            }
            else
            {
                SeriLogger.Information("StoQua");
                foreach (var item in user.ConversationRooms)
                    await Groups.AddToGroupAsync(Context.ConnectionId, item.RoomName);
            }
            SeriLogger.Information("StoQua");
            return base.OnConnectedAsync();
        }
    }
}
