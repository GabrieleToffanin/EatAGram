using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.MongoDb.Configuration
{
    public class MessagesStoreDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string MessagesCollectionName { get; set; }
        public string ConnectionsCollectionName { get; set; }
        public string ConversationRoomsCollectionName { get; set; }
        public string ChatUsersCollectionName { get; set; }

    }
}
