using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Commands.BubbleMeetCommands;
using Application.Features.Queries.BubbleMeetQueries;
using MediatR;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static Domain.CommonCodes.CommonEnums;
using Microsoft.AspNetCore.Authorization;

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]

    public class BubbleMeetController : BaseApiController
    {
        [HttpPost("createUpdateBubbleMeet")]
        public async Task<IActionResult> CreateUpdateBubbleMeet(CreateUpdateBubbleMeetCommand command)
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
        [HttpPost("deleteBubbleMeet")]
        public async Task<IActionResult> DeleteBubbleMeet(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteBubbleMeetCommand { Id = id });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getAllBubbleMeet")]
        public async Task<IActionResult> GetAllBubbleMeet()
        {
            try
            {
                var result = await Mediator.Send(new GetAllBubbleMeetQuery());
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getBubbleMeetById")]
        public async Task<IActionResult> GetBubbleMeetById(int id)
        {
            try
            {
                var result = await Mediator.Send(new GetBubbleMeetByIdQuery { Id = id });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getBubbleMeetWithFilters")]
        public async Task<IActionResult> GetBubbleMeetWithFilters(string bubbleMeetName,int bubbleMeetMemberId)
        {
            try
            {
                var result = await Mediator.Send(new GetBubbleMeetWithFiltersQuery { BubbleMeetName= bubbleMeetName, BubbleMeetMemberId = bubbleMeetMemberId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getAllBubbleMeetsByUserId")]
        public async Task<IActionResult> GetAllBubbleMeetsByUserId(int userId)
        {
            return Ok(await Mediator.Send(new GetAllUsersForBubbleMeetQuery { UserId = userId }));
        }
        [HttpGet("getAllOrphanUsersForBubbleMeet")]
        public async Task<IActionResult> GetAllOrphanUsersForBubbleMeet(int userId)
        {
            return Ok(await Mediator.Send(new GetAllOrphanUsersForBubbleMeetQuery { UserId = userId }));
        }
        [HttpPost("createUpdateBubbleMeetPermissions")]
        public async Task<IActionResult> CreateUpdateBubbleMeetPermissions(CreateUpdateBubbleMeetPermissionsCommand command)
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
