using Eatagram.Core.Entities.Chat;
using Eatagram.Core.Interfaces.Messaging;
using Eatagram.Framework.Logger.LogSetup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Hubs
{
    [Authorize]
    public class MessagingHub : Hub
    {
        private readonly IUserMessaging _userMessaging;
        private string _identityName;

        public MessagingHub(IUserMessaging userMessaging)
        {
            this._userMessaging = userMessaging;
        }

        public override async Task<Task> OnConnectedAsync()
        {
            SeriLogger.Information("StoQua");
            if (Context.User?.Identity.Name != null)
            {
                var user = await _userMessaging.GetUserByUsernameAsync(Context.User?.Identity.Name ?? string.Empty);

                if (user is null)
                {
                    _identityName = Context.User?.Identity.Name ?? throw new InvalidOperationException();
                    user = new ChatUser()
                    {
                        UserName = _identityName
                    };
                    await _userMessaging.AddUserToCollectionAsync(user);
                }
                else
                {
                    foreach (var item in user.ConversationRooms)
                        await Groups.AddToGroupAsync(Context.ConnectionId, item.RoomName);
                }
            }
            return base.OnConnectedAsync();
        }
    }
}
