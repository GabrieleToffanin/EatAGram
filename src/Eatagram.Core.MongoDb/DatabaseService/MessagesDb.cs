using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Chat;
using Eatagram.Core.MongoDb.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Eatagram.Core.MongoDb.DatabaseService
{
    public class MessagesDb
    {
        public IMongoCollection<Message> Messages { get; set; } //Documents - Messages { "Text": " Message text", "ToUser": "Destination User", "FromUser": "starting user" } 
        public IMongoCollection<Connection> Connections { get; set; }
        public IMongoCollection<ConversationRoom> ConversationRooms { get; set; }
        public IMongoCollection<ChatUser> Users { get; set; }


        public MessagesDb(IOptions<MessagesStoreDatabaseSettings> messagesDbSettings) 
        {
            var mongoClient = new MongoClient(messagesDbSettings.Value.ConnectionString);

            var mongoDb = mongoClient.GetDatabase(
                messagesDbSettings.Value.DatabaseName);

            Messages = mongoDb.GetCollection<Message>(
                messagesDbSettings.Value.MessagesCollectionName);

            Connections = mongoDb.GetCollection<Connection>(
                messagesDbSettings.Value.ConnectionsCollectionName);

            ConversationRooms = mongoDb.GetCollection<ConversationRoom>(
                messagesDbSettings.Value.ConversationRoomsCollectionName);

            Users = mongoDb.GetCollection<ChatUser>(
                messagesDbSettings.Value.ChatUsersCollectionName);
        }

        
    }
}
