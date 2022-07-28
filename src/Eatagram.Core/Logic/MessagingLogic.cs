using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Logic;
using Eatagram.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Logic
{
    public class MessagingLogic : IMessagingLogic
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessagingLogic(IMessagesRepository messagesRepository)
        {
            this._messagesRepository = messagesRepository;
        }

        public async Task<IEnumerable<Message>> LoadConversationMessages()
        {
            return await _messagesRepository.GetAsync();
        }

        public async Task SaveMessage(Message message)
        {
            await _messagesRepository.CreateAsync(message);
        }
    }
}
