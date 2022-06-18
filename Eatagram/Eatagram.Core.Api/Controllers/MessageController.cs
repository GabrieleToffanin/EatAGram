using Eatagram.Core.Api.Hubs;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessagingLogic _messagingLogic;
        private readonly IHubContext<MessagingHub> _hubContext;

        public MessageController(IMessagingLogic messagingLogic, IHubContext<MessagingHub> hubContext)
        {
            _messagingLogic = messagingLogic;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("GetMessages")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Message>))]
        public async Task<IActionResult> LoadMessages()
        {
            return Ok(await _messagingLogic.LoadConversationMessages());
        }

        [HttpGet]
        [Route("SendMessage")]
        [ProducesResponseType(200, Type = typeof(Message))]
        public async Task<IActionResult> SendMessage(string message, string userId)
        {
            var content = new Message()
            {
                Text = message,
                FromUser = User.GetUserId(),
                ToUser = userId
            };

            await _messagingLogic.SaveMessage(content);

            return Ok();
        }
    }
}
