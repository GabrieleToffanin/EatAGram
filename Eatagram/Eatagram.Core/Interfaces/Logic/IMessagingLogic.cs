using Eatagram.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Interfaces.Logic
{
    public interface IMessagingLogic
    {
        Task<IEnumerable<Message>> LoadConversationMessages();
        Task SaveMessage(Message message);
    }
}
