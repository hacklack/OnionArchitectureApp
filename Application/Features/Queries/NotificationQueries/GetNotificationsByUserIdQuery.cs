using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Application.ApiModels;
using static Domain.CommonCodes.CommonEnums;

namespace Application.Features.Queries.NotificationQueries
{
    public class GetNotificationsByUserIdQuery : IRequest<NotificationsHistoryListApiModel>
    {
        public int UserId { get; set; }
        public int NotificationCategoryId { get; set; }
        public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQuery, NotificationsHistoryListApiModel>
        {
            private readonly IApplicationDbContext _context;
            public GetNotificationsByUserIdQueryHandler(IApplicationDbContext context) => _context = context;

            public async Task<NotificationsHistoryListApiModel> Handle(GetNotificationsByUserIdQuery query, CancellationToken cancellationToken)
            {
                NotificationsHistoryListApiModel notificationApiModel = new NotificationsHistoryListApiModel();
                notificationApiModel.lstNotificationHistory = _context.notificationsHistory
                    .Join(_context.notifications,bd=>bd.NotificationId,bm=>bm.Id,(bd,bm)=>new { bd,bm})
                    .Join(_context.userDetails,bma=>bma.bd.UserId,ud=>ud.Id,(bma,ud)=>new { bma,ud})
                    .Where(bda => (bda.bma.bd.UserId == query.UserId) && (bda.ud.IsActive==true) && ((query.NotificationCategoryId>0)? bda.bma.bd.NotificationCategoryId==(NotificationCategories)query.NotificationCategoryId : bda.bma.bd.NotificationCategoryId>0))
                    .Select(x=>new NotificationsHistoryApiModel
                    { 
                    Id=x.bma.bd.Id,
                    PODBubbleId=x.bma.bd.PODBubbleId,
                    NotificationId=x.bma.bm.Id,
                    NotificationUserTitle = x.bma.bd.NotificationUserTitle,
                    NotificationDescription=x.bma.bd.NotificationUserDescription,
                    NotificationTypeId=x.bma.bm.NotificationTypeId,
                    NotificationTypeChild =x.bma.bd.NotificationTypeChild,
                    NotificationCategoryId=x.bma.bd.NotificationCategoryId,
                    CreatedBy = x.bma.bm.CreatedBy,
                    CreatedOn = x.bma.bm.CreatedOn,
                    }).ToList();
                notificationApiModel.Count = notificationApiModel.lstNotificationHistory.Count();
                notificationApiModel.UserId = query.UserId;
                if (notificationApiModel == null)
                {
                    return null;
                }
                return notificationApiModel;
            }
        }
    }
}