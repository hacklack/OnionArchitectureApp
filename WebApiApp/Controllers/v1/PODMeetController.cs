using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Commands.PODMeetCommands;
using Application.Features.Queries.PODMeetQueries;
using static Domain.CommonCodes.CommonEnums;

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PODMeetController : BaseApiController
    {
        [HttpPost("createUpdatePODMeet")]
        public async Task<IActionResult> CreateUpdateBubbleMeet(CreateUpdatePODMeetCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpPost("deletePODMeet")]
        public async Task<IActionResult> DeleteBubbleMeet(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeletePODMeetCommand { Id = id });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getAllPODMeet")]
        public async Task<IActionResult> GetAllPODMeet()
        {
            try
            {
                var result = await Mediator.Send(new GetAllPODMeetQuery());
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getPODMeetById")]
        public async Task<IActionResult> GetPODMeetById(int id,int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetPODMeetByIdQuery { Id = id,UserId=userId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getPODMeetWithFilters")]
        public async Task<IActionResult> GetPODMeetWithFilters(string podMeetName, int podMeetMemberId)
        {
            try
            {
                var result = await Mediator.Send(new GetPODMeetWithFiltersQuery { PODMeetName = podMeetName, PODMeetMemberId = podMeetMemberId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getAllPODMeetsByUserId")]
        public async Task<IActionResult> GetAllPODMeetsByUserId(int userId)
        {
            return Ok(await Mediator.Send(new GetAllUsersForPODMeetQuery { UserId = userId }));
        }
        [HttpGet("getAllOrphanUsersForPODMeet")]
        public async Task<IActionResult> GetAllOrphanUsersForPODMeet(int userId)
        {
            return Ok(await Mediator.Send(new GetAllOrphanUsersForPODMeetQuery { UserId = userId }));
        }
        [HttpGet("getAllPodsByUserId")]
        public async Task<IActionResult> GetAllPodsByUserId(int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetAllPodsByUserIdQuery { UserId = userId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getAllBubbleForPodMeetByUserId")]
        public async Task<IActionResult> GetAllBubbleForPodMeetByUserId(int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetPODMeetCreateInitialsQuerys { UserId = userId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpPost("createUpdatePODMeetPermissions")]
        public async Task<IActionResult> CreateUpdatePODMeetPermissions(CreateUpdatePODMeetPermissionsCommand command)
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
