using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Features.Commands.BubbleCommands;
using Application.Features.Queries.BubbleQueries;
using Application.Features.Commands.BubbleMemberCommands;
using Application.Features.Queries.BubbleMemberQueries;
using static Domain.CommonCodes.CommonEnums;
using Application.Features.Commands.BubbleMeetCommands;

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]

    public class BubbleController : BaseApiController
    {
        [HttpPost("createUpdateBubble")]
        public async Task<IActionResult> CreateUpdateBubble(CreateUpdateBubbleCommand command)
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
        [HttpPost("deleteBubble")]
        public async Task<IActionResult> DeleteBubble(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteBubbleCommand { Id = id }));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getAllBubbles")]
        public async Task<IActionResult> GetAllBubbles()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllBubbleQuery()));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBubbleByBubbleId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetBubbleByBubbleIdQuery { Id = id }));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("name/type/Size")]
        public async Task<IActionResult> GetBubbleByActiveDate(string membername, BubbleType type, string size)
        {
            try
            {
                return Ok(await Mediator.Send(new GetBubbleByfilterQuery { Name = membername, Bubbletype = type, Size = size }));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("addBubbleMembers")]
        public async Task<IActionResult> AddBubbleMembers(AddBubbleMemberCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("deleteBubbleMembers")]
        public async Task<IActionResult> DeleteBubbleMembers(DeleteMembersCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getAllBubbleMembers")]
        public async Task<IActionResult> GetAllBubbleMembers()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllBubbleMembersQuery()));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("getAllBubbleMembersByBubbleId")]
        public async Task<IActionResult> GetAllBubbleMembersByBubbleId(int bubbleId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetBubbleMembersByBubbleIdQuery { BubbleId = bubbleId }));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("CountBubbleMembersByBubbleId")]
        public async Task<IActionResult> CountBubbleMembersByBubbleId(int bubbleId)
        {
            try
            {
                return Ok(await Mediator.Send(new CountBubbleMembersByBubbleIdQuery { BubbleId = bubbleId }));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("defineBubbleMembers")]
        public async Task<IActionResult> DefineBubbleMembers(int bubbleId, int memberlength)
        {
            try
            {
                return Ok(await Mediator.Send(new DefineBubbleMembersQuery { BubbleId = bubbleId, MemberLength = memberlength }));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getAllUserBubblesByuserId")]
        public async Task<IActionResult> GetAllUserBubblesByuserId(int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetAllUserBubblesByUserIdQuery { UserId = userId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getBubbleWithFilters")]
        public async Task<IActionResult> GetBubbleWithFilters(int bubbleMemberId,int bubbleType=0, string bubbleCreationDate=null, string bubblesize=null)
        {
            try
            {
                string fromSize = "", toSize = "";
                if (bubblesize != null)
                {
                    var arr = bubblesize.Split('-');
                   
                    if (arr.Length > 0 && arr != null)
                    {
                        fromSize = arr[0].ToString();
                        toSize = arr[1].ToString();
                    }
                    else if (arr.Length == 0 && arr != null)
                    {
                        fromSize = arr[0].ToString();
                        toSize = arr[0].ToString();
                    }
                    else
                    {
                        fromSize = "0";
                        toSize = "10";
                    }
                }
                var result = await Mediator.Send(new GetBubbleWithFiltersQuery { bubbleMemberId= bubbleMemberId, BubbleType = bubbleType, createdDate = bubbleCreationDate, FromSize = fromSize, ToSize = toSize });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpGet("getAllOrphanUser")]
        public async Task<IActionResult> GetAllOrphanUser(int userId,int bubbleType)
        {
            return Ok(await Mediator.Send(new GetAllOrphanUsersQuery { UserId=userId,BubblType=bubbleType}));
        }

        [HttpPost("createUpdateBubblePermissions")]
        public async Task<IActionResult> CreateUpdateBubblePermissions(CreateUpdateBubbleMeetPermissionsCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                if (result > 0)
                    return Ok(new { status = "success", statuscode = 1, data = result });
                else
                    return Ok(new { status = "failure", statuscode = 0, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
    }
}
