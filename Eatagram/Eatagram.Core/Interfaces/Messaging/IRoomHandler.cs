using Eatagram.Core.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Interfaces.Messaging
{
    public interface IRoomHandler
    {
        Task<ConversationRoom> FindRoomByNameAsync(string roomName);
        Task AddUserToConversationRoomAsync(ChatUser user);
        Task RemoveUserFromConversationRoomAsync(ChatUser user);
    }
}
