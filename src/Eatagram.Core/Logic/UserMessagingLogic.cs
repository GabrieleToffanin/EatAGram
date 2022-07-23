using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Chat;
using Eatagram.Core.Interfaces.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Logic
{
    public sealed class UserMessagingLogic : IUserMessaging
    {
        private readonly IUserMessagingRepository _userMessagingRepository;

        public UserMessagingLogic(IUserMessagingRepository userMessagingRepository)
        {
            _userMessagingRepository = userMessagingRepository;
        }

        public async Task AddUserToCollectionAsync(ChatUser user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            await _userMessagingRepository.AddUserToMongoCollectionAsync(user);
        }

        public async Task<ChatUser> GetUserByUsernameAsync(string username)
        {
            return await _userMessagingRepository.GetUserByUsernameFromMongoAsync(username);
        }
    }
}
