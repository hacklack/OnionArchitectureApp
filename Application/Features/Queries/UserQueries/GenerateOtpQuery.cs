using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Twilio.Rest.Verify.V2.Service;
using Application.Services;
using Application.ApiModels;

namespace Application.Features.Queries.UserQueries
{
    public class GenerateOtpQuery : IRequest<OtpVerificationModel>
    {
        public string Phone { get; set; }
        public class GetUserOtpHandler : IRequestHandler<GenerateOtpQuery, OtpVerificationModel>
        {
            private readonly IApplicationDbContext _context;
            public GetUserOtpHandler(IApplicationDbContext context) => _context = context;

            public async Task<OtpVerificationModel> Handle(GenerateOtpQuery query, CancellationToken cancellationToken)
            {

                NotificationsServices notificationsServices = new NotificationsServices(_context);
                return await  notificationsServices.SendOTPVerification(query.Phone);

                //var objOtp = _context.otpHistory.Where(a => a.Otp == query.Otp && a.OtpTimeStamp>DateTime.Now.AddMinutes(-1)).FirstOrDefault();
                //if (objOtp == null)
                //{
                //    var objOtp2 = _context.otpHistory.Where(a => a.Otp == query.Otp && a.OtpStatus==true).FirstOrDefault();
                //    objOtp2.OtpStatus = false;
                //    await _context.SaveChanges();
                //    return null;
                //}
            }
        }
    }
}
