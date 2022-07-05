using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Interfaces.Messaging
{
    public interface IUserMessaging
    {
        Task<ChatUser> GetUserByUsernameAsync(string username);
        Task AddUserToCollectionAsync(ChatUser user);
    }
}
