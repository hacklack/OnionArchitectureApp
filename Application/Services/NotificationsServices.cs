using System;
using System.Collections.Generic;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Application.ApiModels;
using System.Threading.Tasks;
using Application.Interfaces;
using System.Linq;
using static Domain.CommonCodes.CommonEnums;
using Domain.Entities;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.IpMessaging.V2;
using Twilio.Rest.Verify.V2.Service;
using Application.ApiModels;
using CorePush.Apple;
using System.Net.Http;

namespace Application.Services
{
    public class NotificationsServices
    {
        private readonly IApplicationDbContext _context;
        public NotificationsServices(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendNotification(string notificationTitle, string notificationDescription, int notificationId, int podBubbleId, int item, int createdBy, int updatedBy, NotificationTypeChild notificationTypeChild, NotificationCategories notificationCategoryId)
        {
            try
            {
                List<UserDeviceDetailsApiModel> lstUserDeviceDetails = _context.userDeviceDetails
                    .Where(y => y.UserId == item)
                    .Select(x => new UserDeviceDetailsApiModel
                    {
                        Id = x.Id,
                        DeviceToken = x.DeviceToken,
                        UserId = x.UserId
                    }).ToList();
                if (lstUserDeviceDetails != null && lstUserDeviceDetails.Count() > 0)
                {
                    foreach (var token in lstUserDeviceDetails)
                    {
                        //if (token.DeviceTypeId == DeviceType.Ios)
                        //{

                        //    await SendApnNotificationAsync(token.DeviceToken,notificationDescription,notificationTitle);
                        //}
                        //else if (token.DeviceTypeId == DeviceType.Andriod)
                        //{
                            NotificationFCMApiModel fcmModel = new NotificationFCMApiModel();
                            fcmModel.NotificationBody.Title = notificationTitle;
                            fcmModel.NotificationBody.Body = notificationDescription;
                            fcmModel.FcmMessageBody.Notification = fcmModel.NotificationBody;
                            fcmModel.FcmMessageBody.Token = token.DeviceToken;
                            await BuildNotificationBody(fcmModel);
                       // }

                        NotificationsHistory notificationsHistory = new NotificationsHistory();
                        notificationsHistory.NotificationId = notificationId;
                        notificationsHistory.NotificationUserTitle = notificationTitle;
                        notificationsHistory.NotificationTypeChild = notificationTypeChild;
                        notificationsHistory.NotificationCategoryId = notificationCategoryId;
                        notificationsHistory.NotificationUserDescription = notificationDescription;
                        notificationsHistory.PODBubbleId = podBubbleId;
                        notificationsHistory.UserId = item;
                        notificationsHistory.UserDeviceId = token.Id;
                        notificationsHistory.CreatedBy = createdBy;
                        notificationsHistory.UpdatedBy = updatedBy;
                        _context.notificationsHistory.Add(notificationsHistory);
                        await _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<string> BuildNotificationBody(NotificationFCMApiModel fcmModel)
        {
            Message message = new Message();
            try
            {
                var path = CommonStaticStrings.FCMAuthCredentials;
                FirebaseApp app = null;
                try
                {
                    app = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(path)
                    }, CommonStaticStrings.FCMAppName);
                }
                catch (Exception ex)
                {
                    app = FirebaseApp.GetInstance(CommonStaticStrings.FCMAppName);
                }

                var fcm = FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(app);
                message = fcmModel.FcmMessageBody;
                return await fcm.SendAsync(message);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private static readonly HttpClient http = new HttpClient();
        private static async Task<string> SendApnNotificationAsync(string deviceToken, string notificationMessage, string notificationTitle)
        {
            string returnMessage = string.Empty;
            try
            {
            
            var settings = new ApnSettings
            {
                AppBundleIdentifier = CommonStaticStrings.apnBundleId,
                P8PrivateKey = CommonStaticStrings.apnP8PrivateKey,
                P8PrivateKeyId = CommonStaticStrings.apnP8PrivateKeyId,
                TeamId = CommonStaticStrings.apnTeamId,
                ServerType = CommonStaticStrings.apnServerType,
            };

            while (true)
            {
                var apn = new ApnSender(settings, http);
                var payload = new AppleNotificationApiModel(
                    Guid.NewGuid(),
                    notificationMessage,
                    notificationTitle
                    );
                var response = await apn.SendAsync(payload, deviceToken);
                return returnMessage = response.ToString();
            }
            }
            catch (Exception ex)
            {
                return returnMessage = ex.ToString();
            }
        }

        public async Task<OtpVerificationModel> SendOTPVerification(string phone)
        {
            OtpVerificationModel otpVerificationModel = new OtpVerificationModel();

            try
            {
                TwilioClient.Init(CommonStaticStrings.AccountSid, CommonStaticStrings.AuthToken);
                var outout = VerificationResource.Create(
                                    to: "+" + phone,
                                    channel: CommonStaticStrings.Sms,
                                    pathServiceSid: CommonStaticStrings.ServiceSid
                                    );
                //otpVerificationModel.account_sid = outout.AccountSid;
               // otpVerificationModel.channel = outout.Channel;
                //otpVerificationModel.date_created = outout.DateCreated;
               // otpVerificationModel.date_updated = outout.DateUpdated;
               // otpVerificationModel.lookup = outout.Lookup;
               // otpVerificationModel.payee = outout.Payee;
               // otpVerificationModel.send_code_attempts = outout.SendCodeAttempts;
               // otpVerificationModel.service_sid = outout.ServiceSid;
               // otpVerificationModel.sid = outout.Sid;
                otpVerificationModel.status = outout.Status;
                otpVerificationModel.statuscode = (outout.Status == "pending") ? 1 : 0;
                //otpVerificationModel.url = outout.Url;
                otpVerificationModel.to = outout.To;
                otpVerificationModel.valid = outout.Valid;
               

            }
            catch (Exception ex)
            {
                otpVerificationModel.to = "+" + phone;
                otpVerificationModel.status = "Error";
                otpVerificationModel.statuscode = -1;
            }
            return otpVerificationModel;
        }
        public async Task<OtpVerificationModel> RecievedOTPVerification(string phone, string otp)
        {
            OtpVerificationModel otpVerificationModel = new OtpVerificationModel();
            try
            {
                TwilioClient.Init(CommonStaticStrings.AccountSid, CommonStaticStrings.AuthToken);
                var outout = VerificationCheckResource.Create(
                               to: "+" + phone,
                             code: otp,
                             pathServiceSid: CommonStaticStrings.ServiceSid
                            );
              //  otpVerificationModel.account_sid = outout.AccountSid;
              //  otpVerificationModel.date_created = outout.DateCreated;
                //otpVerificationModel.date_updated = outout.DateUpdated;
                //otpVerificationModel.payee = outout.Payee;
                //otpVerificationModel.service_sid = outout.ServiceSid;
                //otpVerificationModel.sid = outout.Sid;
                otpVerificationModel.status = outout.Status;
                otpVerificationModel.statuscode = (outout.Status == "pending") ? 0 : 1;
                otpVerificationModel.to = outout.To; 
                otpVerificationModel.valid = outout.Valid;
            }
            catch (Exception ex)
            {

                otpVerificationModel.to = "+" + phone;
                otpVerificationModel.status = "Error";
                otpVerificationModel.statuscode = -1;

            }
            return otpVerificationModel;

        }

        public async Task<double> CalculateCountySaftyFactor(int fips)
        {
            double csf = 0;
            List<BubbleSafetyWights> bsw = new List<BubbleSafetyWights>();
            BubbleSafetyCalculationFields bsl = new BubbleSafetyCalculationFields();
            bsw = _context.bubbleSafetyWights.ToList();
            bsl = _context.bubbleSafetyWightsCalculationFields.Where(y => Convert.ToInt32(y.Fips) == fips).FirstOrDefault();


            double WCTPR = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.TestPositivityRatio).Select(x => x.BubbleWightValue).FirstOrDefault();
            double WCCD = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.CaseDensityRatio).Select(x => x.BubbleWightValue).FirstOrDefault();
            double WCIR = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.InfectionRatio).Select(x => x.BubbleWightValue).FirstOrDefault();
            double WCIRI90 = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.InfectionRatioC190).Select(x => x.BubbleWightValue).FirstOrDefault();
            double WCVPR = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.VaccineToPopulationRatio).Select(x => x.BubbleWightValue).FirstOrDefault();
            double WCDPR = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.DeathToPopulationRatio).Select(x => x.BubbleWightValue).FirstOrDefault();
            double WCCPR = bsw.Where(y => y.BubbleWightFiledTypeId == BubbleSaftyFieldType.CasesToPopulationRatio).Select(x => x.BubbleWightValue).FirstOrDefault();

            return  csf = (bsl.TestPositivityRatio * WCTPR)
                  * (bsl.CaseDensity * WCCD)
                  * (bsl.InfectionRate * WCIR)
                  * (bsl.InfectionRateCI90 * WCIRI90)
                  * (bsl.VaccineToPopulationRatio * WCVPR)
                  * (bsl.DeathToPopulationRatio * WCDPR)
                  * (bsl.CasesToPopulationRatio * WCCPR);
        }
        public async Task<double> BubbleSaftyCalculation(List<int> userIds)
        {
            try
            {

                List<BubbleApiModel> lstBubbleDetails = new List<BubbleApiModel>();
                List<double> csf = new List<double>();
                List<double> bsc = new List<double>();
                int countyId = 0;
                int bubbleId = 0;
                double bsl = 0;
                foreach (var item in userIds)
                {
                    BubbleApiModel bubbleDetails = new BubbleApiModel();

                    bubbleDetails =
                    _context.userDetails
                    .Join(_context.bubbleMembers, ud => ud.Id, bm => bm.UserId, (ud, bm) => new { ud, bm })
                    .Where(y => y.ud.Id == item && y.ud.IsActive == true)
                    .Select(x => new BubbleApiModel
                    {
                        BubbleCounty = x.ud.County,
                        Id = x.bm.BubbleId
                    }).FirstOrDefault();
                    lstBubbleDetails.Add(bubbleDetails);
                }
                foreach (var county in lstBubbleDetails)
                {
                    if (countyId != county.BubbleCounty)
                    {
                        csf.Add(await CalculateCountySaftyFactor(county.BubbleCounty));
                    }
                    countyId = county.BubbleCounty;
                }
                if (csf.Count() > 1)
                {
                    for (int i = 0; i < csf.Count(); i++)
                    {
                        bsl = bsl + (csf[i] * CommonStaticStrings.BubbleSaftyLevel);
                    }
                    return bsl = ((bsl / csf.Count()) / csf.Count());
                }
                else
                {
                    foreach (var bubble in lstBubbleDetails)
                    {
                        if (bubbleId != bubble.Id)
                        {
                            bsc.Add(CommonStaticStrings.BubbleSaftyLevel);
                        }
                        bubbleId = bubble.Id;
                    }
                    if (bsc.Count() > 1)
                    {
                        for (int i = 0; i < bsc.Count(); i++)
                        {
                            bsl = bsl + bsc[i];
                        }
                        return bsl = ((bsl / bsc.Count()) / bsc.Count());
                    }
                    else
                    {
                        return bsl = CommonStaticStrings.BubbleSaftyLevel;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<double> PODSaftyCalculation(List<int> bubbleIds)
        {
            List<BubbleApiModel> lstBubbleDetails = new List<BubbleApiModel>();
            BubbleApiModel bubbleDetails = new BubbleApiModel();
            List<double> csf = new List<double>();
            List<double> bsc = new List<double>();
            List<int> userIds = new List<int>();
            List<int> bubbleMemberIds = new List<int>();
            int countyId = 0;
            int bubbleId = 0;
            double bsl = 0;


            foreach (var item in bubbleIds)
            {
                bubbleMemberIds = _context
                    .bubbleMembers
                    .Join(_context.userDetails, bm => bm.UserId, ud => ud.Id, (bm, ud) => new { bm, ud })
                    .Where(y => y.bm.BubbleId == item && y.ud.IsActive == true)
                    .Select(x => x.ud.Id)
                    .ToList();

                foreach (var memberId in bubbleMemberIds)
                {
                    userIds.Add(memberId);
                }
            }

            foreach (var item in userIds)
            {
                bubbleDetails =
                _context.userDetails
                .Join(_context.bubbleMembers, ud => ud.Id, bm => bm.UserId, (ud, bm) => new { ud, bm })
                .Where(y => y.ud.Id == item && y.ud.IsActive == true)
                .Select(x => new BubbleApiModel
                {
                    BubbleCounty = x.ud.County,
                    Id = x.bm.BubbleId
                }).FirstOrDefault();
                lstBubbleDetails.Add(bubbleDetails);
            }
            foreach (var county in lstBubbleDetails)
            {
                if (countyId != county.BubbleCounty)
                {
                    csf.Add(await CalculateCountySaftyFactor(county.BubbleCounty));
                }
                countyId = county.BubbleCounty;
            }
            if (csf.Count() > 1)
            {
                for (int i = 0; i < csf.Count(); i++)
                {
                    bsl = bsl + (csf[i] * CommonStaticStrings.BubbleSaftyLevel);
                }
                return bsl = ((bsl / csf.Count()) / csf.Count());
            }
            else
            {
                foreach (var bubble in lstBubbleDetails)
                {
                    if (bubbleId != bubble.Id)
                    {
                        bsc.Add(CommonStaticStrings.BubbleSaftyLevel);
                    }
                    bubbleId = bubble.Id;
                }
                if (bsc.Count() > 1)
                {
                    for (int i = 0; i < bsc.Count(); i++)
                    {
                        bsl = bsl + bsc[i];
                    }
                    return bsl = ((bsl / bsc.Count()) / bsc.Count());
                }
                else
                {
                    return bsl = CommonStaticStrings.BubbleSaftyLevel;
                }
            }
        }


    }
}
