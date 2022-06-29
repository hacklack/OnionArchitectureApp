using MediatR;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using System.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace Application.Features.Queries.UserQueries
{
   public class GetUserByUserNameAndPhone : IRequest<UserDetails>
    {
       public string phone { get; set; }
        public class GetUserByUserNameAndPhoneHandler : IRequestHandler<GetUserByUserNameAndPhone, UserDetails>
        {
            private readonly IApplicationDbContext _context;
            public GetUserByUserNameAndPhoneHandler(IApplicationDbContext context) => _context = context;

            public async Task<UserDetails> Handle(GetUserByUserNameAndPhone query, CancellationToken cancellationToken)
            {
                var user = _context.userDetails.Where(a =>(a.PhoneNo==query.phone) && (a.IsActive==true)).FirstOrDefault();
                // session["userid"] = user.Id;
                if (user == null)
                {
                    return null;
                }
                //Random rnd = new Random();
                //string randomNumber = (rnd.Next(100000, 999999)).ToString();
                //string message = "your otp is " + randomNumber;
                //String result;

                //String url = "https://api.txtlocal.com/send/?apikey="+ "xeEbQN5K+jU-nGMk106ZtjMbbkK7y3sdh6JFC0cXe9" + "&numbers=" + query.phone + "&message=" + message + "&sender=Qbubble";
                ////refer to parameters to complete correct url string

                //StreamWriter myWriter = null;
                //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                //objRequest.Method = "POST";
                //objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                //objRequest.ContentType = "application/x-www-form-urlencoded";
                //try
                //{
                //    myWriter = new StreamWriter(objRequest.GetRequestStream());
                //    myWriter.Write(url);
                //}
                //catch (Exception e)
                //{
                  
                //}
                //finally
                //{
                //    myWriter.Close();
                //}

                //HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                //using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                //{
                //    result = sr.ReadToEnd();
                //    // Close and clean up the StreamReader
                //    sr.Close();
                //}
                ////return result;
                ////using (var wb = new WebClient())
                ////{
                ////    byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
                ////{
                ////{"apikey" , "rY2/mwozVP8-ljbOZ8JXl5d2yUy4c65cbg7IxRALjS"},
                ////{"numbers" , query.phone},
                ////{"message" , message},
                ////{"sender" , "Qubble"}
                ////});
                ////    string result = System.Text.Encoding.UTF8.GetString(response);
                ////   // return result;
                ////}
                //var ResendOtp = _context.otpHistory.Where(x => x.UserId == user.Id).FirstOrDefault();
                //if (ResendOtp != null)
                //{
                //    ResendOtp.Otp = randomNumber;
                //    ResendOtp.OtpStatus = true;
                //    ResendOtp.OtpTimeStamp = DateTime.Now;
                //}
                //else
                //{
                //    var otpData = new OtpHistory();
                //    otpData.Otp = randomNumber;
                //    otpData.OtpStatus = true;
                //    otpData.OtpTimeStamp = DateTime.Now;
                //    otpData.UserId = user.Id;
                //    _context.otpHistory.Add(otpData);
                //}
                //await _context.SaveChanges();
                return user;
            }
        }
    }
}
