using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Logic
{
    public class MessagingLogic : IMessagingLogic
    {
        private readonly IMessagesRepository messagesRepository;

        public MessagingLogic(IMessagesRepository messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task<IEnumerable<Message>> LoadConversationMessages()
        {
            return await messagesRepository.GetAsync();
        }

        public async Task SaveMessage(Message message)
        {
            await messagesRepository.CreateAsync(message);
        }
    }
}
