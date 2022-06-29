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

namespace Application.Features.Commands.PODMeetCommands
{

    public class CreateUpdatePODMeetPermissionsCommand : IRequest<int>
    {
        public int PermissionParenttId { get; set; }
        public List<int> LstBubbleIds { get; set; }
        public int UserId { get; set; }
        public UserPermission UserPermissionTypeId { get; set; }
        public MeetType MeetTypeId { get; set; }
        public bool UserPermissionStatus { get; set; }
        public int UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public class CreateUpdatePODMeetPermissionsHandler : IRequestHandler<CreateUpdatePODMeetPermissionsCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateUpdatePODMeetPermissionsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUpdatePODMeetPermissionsCommand command, CancellationToken cancellationToken)
            {   
                try
                {
                    foreach (var item in command.LstBubbleIds)
                    {
                      List<int> lstAdmins =  _context.bubbleMeetMemberPermissions.Where(ya => ya.BubbleMeetId == item && ya.UserPermissionTypeId == UserPermission.IsAdmin && ya.MeetTypeId == MeetType.Bubble && ya.UserPermissionStatus == true).Select(xa=>xa.UserId).ToList();
                        foreach (var admin in lstAdmins)
                        {
                            BubbleMeetMemberPermissions dbModel = new BubbleMeetMemberPermissions();
                            if (_context.bubbleMeetMemberPermissions
                                .Where(y => y.BubbleMeetId == command.PermissionParenttId
                                && y.UserId == admin
                                && y.UserPermissionTypeId == command.UserPermissionTypeId && y.MeetTypeId == command.MeetTypeId)
                                .Count() == 0)
                            {
                                
                                    dbModel.BubbleMeetId = command.PermissionParenttId;
                                    dbModel.UserId =admin;
                                      dbModel.UserPermissionTypeId = command.UserPermissionTypeId;
                                    dbModel.UserPermissionStatus = command.UserPermissionStatus;
                                    dbModel.MeetTypeId = command.MeetTypeId;
                                    dbModel.CreatedBy = command.CreatedBy;
                                    dbModel.UpdatedBy = command.UpdatedBy;
                                    dbModel.UpdatedOn = DateTime.UtcNow;
                                    dbModel.CreatedOn = DateTime.UtcNow;
                                    _context.bubbleMeetMemberPermissions.Add(dbModel);
                                    await _context.SaveChanges();
                            }
                            else
                            {
                                dbModel = new BubbleMeetMemberPermissions();
                                dbModel = _context.bubbleMeetMemberPermissions
                                    .Where(y => y.BubbleMeetId == command.PermissionParenttId
                                  && y.UserId == admin
                                  && y.UserPermissionTypeId == command.UserPermissionTypeId && y.MeetTypeId == command.MeetTypeId
                                  ).FirstOrDefault();
                                dbModel.BubbleMeetId = command.PermissionParenttId;
                                dbModel.UserId = admin;
                                dbModel.UserPermissionTypeId = command.UserPermissionTypeId;
                                dbModel.UserPermissionStatus = command.UserPermissionStatus;
                                dbModel.MeetTypeId = command.MeetTypeId;
                                dbModel.UpdatedOn = DateTime.UtcNow;
                                dbModel.UpdatedBy = command.UpdatedBy;
                                await _context.SaveChanges();
                            }
                        }
                    }
                    return command.LstBubbleIds.Count();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
