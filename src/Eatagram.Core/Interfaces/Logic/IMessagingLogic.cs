using Eatagram.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Interfaces.Logic
{
    /// <summary>
    /// Provides methods for messaging through SignalR and saving Chats into MongoDB
    /// </summary>
    public interface IMessagingLogic
    {
        /// <summary>
        /// Loads the conversation between Clients
        /// </summary>
        /// <returns>A collection of <see cref="Message"/></returns>
        Task<IEnumerable<Message>> LoadConversationMessages();
        /// <summary>
        /// Save the message and notifies to the clients
        /// </summary>
        /// <param name="message">Current <see cref="Message"/> to be send and saved</param>
        Task SaveMessage(Message message);
    }
}
