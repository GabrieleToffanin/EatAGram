using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Repository;
using Eatagram.Core.MongoDb.DatabaseService;
using MongoDB.Driver;

namespace Eatagram.Core.MongoDb.Repository
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly MessagesDb _messages;

        public MessagesRepository(MessagesDb messages)
        {
            _messages = messages;
        }

        public async Task CreateAsync(Message message)
        {
            await _messages.Messages.InsertOneAsync(message);
        }

        public async Task<IEnumerable<Message>> GetAsync()
        {
            return await _messages.Messages.Find(_ => true).ToListAsync();
        }
    }
}
