using Eatagram.Core.Api.Hubs;
using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Eatagram.Core.Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>

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
        [Authorize(Roles = "Member, Administrator")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Message>))]
        public async Task<IActionResult> LoadMessages()
        {
            return Ok(await _messagingLogic.LoadConversationMessages());
        }

        

        [HttpPost]
        [Route("SendMessage")]
        [ProducesResponseType(200, Type = typeof(Message))]
        public async Task<IActionResult> SendMessage([FromBody]MessageRequest request)
        {
            var content = request.GetContract() with { FromUser = User.GetUserId() };

            //Private Chat
            await _hubContext.Clients.Group(request.GroupName!).SendAsync("sendPrivateMessage");

            //Global chat
            //await _hubContext.Clients.All.SendAsync("receiveMessage", request.User, request.Message);


            await _messagingLogic.SaveMessage(content);

            return Ok();
        }
    }
}
