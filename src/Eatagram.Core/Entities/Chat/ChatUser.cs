using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Entities.Chat
{
    public class ChatUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public ICollection<Connection> Connections { get; set; }
        public ICollection<ConversationRoom> ConversationRooms { get; set; }
    }
}
