using Application.Features.Commands.NotificationCommands;
using Application.Features.Queries.ChatQueries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using static Domain.CommonCodes.CommonEnums;
using Application.ApiModels;
using Application.ChatComponents.Hubs;
using Application.Features.Commands.ChatCommands;
using Application.Interfaces;   

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ChatController : BaseApiController
    {
        [HttpGet("getAllChatmembersByUserIdChatTypeId")]
        public async Task<IActionResult> GetAllChatmembersByUserIdChatTypeId(int bubblePODId, int chatParentTypeId)
        {
            try
            {
                var result = await Mediator.Send(new GetChatMembersByUserIdChatypeIdQuery { BubblePODId = bubblePODId, ChatParentTypeId = (MeetType)chatParentTypeId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpPost("createUpdatChat")]
        public async Task<IActionResult> CreateUpdateChat(CreateUpdateChatCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpGet("getGroupChathistoryByChatParentId")]
        public async Task<IActionResult> GetChatHistoryByChatId(int chatTypeId, int chatParentTypeId, int chatParentId)
        {
            try
            {
                var result = await Mediator.Send(new GetChatHistoryByChatIdQuery { ChatTypeId = chatTypeId, ChatParentTypeId = chatParentTypeId, ChatParentId = chatParentId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage(SendMessageCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost("deleteChat")]
        public async Task<IActionResult> DeleteChat(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteChatCommand { Id = id }));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("createUpdatPersonalChat")]
        public async Task<IActionResult> CreateUpdatePersonalChat(CreateUpdatePersonalChatCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        private readonly IHubContext<ChatHubs> _hubContext;

        [HttpPost("getPersonalChathistoryByChatMemberIds")]
        public async Task<IActionResult> GetPersonalChathistoryByChatMemberIds(GetPersonalChatHistoryByChatIdQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }


        public ChatController(IHubContext<ChatHubs> hubContext)
        {
            _hubContext = hubContext;
        }

        //[Route("send")]                                           //path looks like this: https://localhost:44379/api/chat/send
        //[HttpPost]
        //public IActionResult SendRequest([FromBody] MessageModel msg)
        //{
        //    _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.msgText);
        //    return Ok();
        //}

    }
}
