using Eatagram.Core.Api.Filter;
using Eatagram.Core.Api.Hubs;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Logic;
using Eatagram.SDK.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Controllers
{

    [DurationFilter]
    [ExceptionFilter]
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
        [Authorize]
        [Route("GetMessages")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Message>))]
        public async Task<IActionResult> LoadMessages()
        {
            return Ok(await _messagingLogic.LoadConversationMessages());
        }

        

        [HttpPost]
        [Authorize]
        [Route("SendMessage")]
        [ProducesResponseType(200, Type = typeof(Message))]
        public async Task<IActionResult> SendMessage([FromBody]MessageRequest request)
        {
            var content = request.GetContract();

            content.FromUser = User.GetUserId();
            await _hubContext.Clients.Group(request.GroupName!).SendAsync("sendPrivateMessage");

            await _messagingLogic.SaveMessage(content);

            return Ok(content);
        }
    }
}
