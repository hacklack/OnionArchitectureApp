using MediatR;
using Domain.Entities;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using static Domain.CommonCodes.CommonEnums;
using Application.ApiModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Application.Features.Commands.BubbleMeetCommands
{

    public class CreateUpdateBubbleMeetPermissionsCommand : IRequest<int>
    {
        public List<BubbleMeetPermissionsApiModel> lstBubbleMeetPermissionsApiModels { get; set; }
        public class CreateUpdateBubbleMeetPermissionsHandler : IRequestHandler<CreateUpdateBubbleMeetPermissionsCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdateBubbleMeetPermissionsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUpdateBubbleMeetPermissionsCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    foreach (var item in command.lstBubbleMeetPermissionsApiModels)
                    {
                        BubbleMeetMemberPermissions dbModel = new BubbleMeetMemberPermissions();
                        if (_context.bubbleMeetMemberPermissions
                            .Where(y => y.BubbleMeetId == item.PermissionParenttId
                            && y.UserId == item.UserId
                            && y.UserPermissionTypeId == item.UserPermissionTypeId && y.MeetTypeId==item.MeetTypeId)
                            .Count() == 0)
                        {
                            if (item.Id == 0)
                            {
                                dbModel.BubbleMeetId = item.PermissionParenttId;
                                dbModel.UserId = item.UserId;
                                dbModel.UserPermissionTypeId = item.UserPermissionTypeId;
                                dbModel.UserPermissionStatus = item.UserPermissionStatus;
                                dbModel.MeetTypeId = item.MeetTypeId;
                                dbModel.CreatedBy = item.CreatedBy;
                                dbModel.UpdatedBy = item.UpdatedBy;
                                dbModel.UpdatedOn = item.UpdatedOn;
                                dbModel.CreatedOn = item.CreatedOn;
                                _context.bubbleMeetMemberPermissions.Add(dbModel);
                                await _context.SaveChanges();
                            }
                        }
                        else
                        {
                            dbModel = new BubbleMeetMemberPermissions();
                            dbModel = _context.bubbleMeetMemberPermissions
                                .Where(y => y.BubbleMeetId == item.PermissionParenttId
                              && y.UserId == item.UserId
                              && y.UserPermissionTypeId == item.UserPermissionTypeId && y.MeetTypeId ==item.MeetTypeId
                              ).FirstOrDefault();
                            dbModel.BubbleMeetId = item.PermissionParenttId;
                            dbModel.UserId = item.UserId;
                            dbModel.UserPermissionTypeId = item.UserPermissionTypeId;
                            dbModel.UserPermissionStatus = item.UserPermissionStatus;
                            dbModel.UpdatedOn = DateTime.UtcNow;
                            dbModel.UpdatedBy = item.UpdatedBy;
                            await _context.SaveChanges();
                        }
                    }
                    return command.lstBubbleMeetPermissionsApiModels.Count();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
