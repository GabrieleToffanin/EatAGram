using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Chat;
using Eatagram.Core.Interfaces.Messaging;
using Eatagram.Core.MongoDb.DatabaseService;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.MongoDb.Repository
{
    public class UserMessagingRepository : IUserMessagingRepository
    {
        private readonly MessagesDb _messagesDb;

        public UserMessagingRepository(MessagesDb messagesDb)
        {
            _messagesDb = messagesDb;
        }

        public async Task AddUserToMongoCollectionAsync(ChatUser user)
        {
            await _messagesDb.Users.InsertOneAsync(user);
        }

        public async Task<ChatUser> GetUserByUsernameFromMongoAsync(string username)
        {
            var currentUser = await _messagesDb.Users.FindAsync<ChatUser>(Builders<ChatUser>.Filter.Where(x => x.UserName == username));

            if (currentUser != null)
                return currentUser;

            return null;
        }
    }
}
