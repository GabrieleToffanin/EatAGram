using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Interfaces.Messaging
{
    public interface IUserMessagingRepository
    {
        Task AddUserToMongoCollectionAsync(ChatUser user);
        Task<ChatUser> GetUserByUsernameFromMongoAsync(string username);
    }
}
