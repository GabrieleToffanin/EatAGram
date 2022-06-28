using Eatagram.Core.Entities;
using Eatagram.Core.MongoDb.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.MongoDb.DatabaseService
{
    public class MessagesDb
    {
        public IMongoCollection<Message> Messages { get; set; }

        public MessagesDb(IOptions<MessagesStoreDatabaseSettings> messagesDBSettings) 
        {
            var mongoClient = new MongoClient(messagesDBSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(
                messagesDBSettings.Value.DatabaseName);

            Messages = mongoDb.GetCollection<Message>(
                messagesDBSettings.Value.MessagesCollectionName);
        }

        
    }
}
