using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Commands.PodCommands;
using MediatR;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static Domain.CommonCodes.CommonEnums;
using Application.Features.Queries.PodQueries;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Queries.BubbleMemberQueries;

namespace WebApiApp.Controllers.v1
{

    [ApiVersion("1.0")]

    public class PodController : BaseApiController
    {
        [HttpGet("getPODInitials")]
        public async Task<IActionResult> GetPODInitials(int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetPODCreateInitialsQuerys { UserId = userId });
                return Ok(new
                {
                    status = "success",
                    statuscode = 1,
                    data = result
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    status = "Error",
                    statuscode = 0
                });
            }
        }
        [HttpPost("createUpdatePod")]
        public async Task<IActionResult> CreateUpdatePod(CreateUpdatePodCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpPost("deletePod")] 
        public async Task<IActionResult> DeletePod(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeletePodCommand { Id = id });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpGet("getAllPod")]
        public async Task<IActionResult> GetAllPod()
        {
            try
            {
                var result = await Mediator.Send(new GetAllPodQuery());
                return Ok(new
                {
                    status = "success",
                    statuscode = 1,
                    data = result
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    status = "Error",
                    statuscode = 0
                });
            }
        }
        //[HttpGet("getPodWithFilters")]
        //public async Task<IActionResult> GetPodWithFilters(string name)
        //{
        //    try
        //    {
        //        var result = await Mediator.Send(new GetPodWithFiltersQuery { PName = name });
        //        return Ok(new
        //        {
        //            status = "success",
        //            statuscode = 1,
        //            data = result
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return Ok(new
        //        {
        //            status = "Error",
        //            statuscode = 0
        //        });
        //    }
        //}

        [HttpGet("getPodById")]
        public async Task<IActionResult> GetPodById(int id)
        {
            try
            {

                var result = await Mediator.Send(new GetPodByIdQuery { PodId = id });
                return Ok(new
                {
                    status = "success",
                    statuscode = 1,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "Error",
                    statuscode = 0
                });
            }
        }

        [HttpGet("getAllUserPODByuserId")]
        public async Task<IActionResult> GetAllUserPODByuserId(int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetAllUserPODsByUserIdQuery { UserId = userId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpGet("getPodByfilters/{userId}")]
        public async Task<IActionResult> getPodByfilters(int userId=0,string podSize=null,string podName=null,int bubbletype=0, string podDate=null)
        {
            try
            {
                string fromSize = "", toSize = "";
                if (podSize != null)
                {
                    var arr = podSize.Split('-');

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
                var result = await Mediator.Send(new GetPodByFiltersQuery {UserId=userId, FromSize = fromSize, ToSize = toSize,PodName = podName, PodDate = podDate, BubbleType = bubbletype });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpGet("getBubblesByBubbleType")]
        public async Task<IActionResult> GetBubblesByBubbleType(BubbleType bubbleType)
        {
            try
            {
                var result = await Mediator.Send(new GetBubbleByBubbleTypeQuery {Bubbletype=bubbleType});
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpGet("getBubbleDetailsByUserId")]
        public async Task<IActionResult> GetBubbleDetailsByUserId(int userId)
        {
            try
            {
                var result = await Mediator.Send(new GetBubbleDetailsByUserIdQuery { UserId = userId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }


    }
}


