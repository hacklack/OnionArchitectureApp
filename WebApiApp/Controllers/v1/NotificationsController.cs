using Application.Features.Commands.NotificationCommands;
using Application.Features.Queries.NotificationQueries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NotificationsController : BaseApiController
    {
        [HttpGet("getAllNotificationsByUserId")]
        public async Task<IActionResult> GetAllNotificationsByUserId(int userId,int notificationCategoryId)
        {
            try
            {
                var result = await Mediator.Send(new GetNotificationsByUserIdQuery {UserId= userId, NotificationCategoryId = notificationCategoryId });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }
        [HttpPost("DeleteNotificationHistory/{id}")]
        public async Task<IActionResult> DeleteNotificationHistory(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteNotificationHistoryCommand { Id = id });
                return Ok(new { status = "Success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

    }
}
