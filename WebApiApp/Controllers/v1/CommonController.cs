using Application.Features.Queries.CommonQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CommonController : BaseApiController
    {
        private IHostingEnvironment env;

        [HttpGet("getAllCounties")]
        public async Task<IActionResult> GetAllCounties()
        {
            try
            {
                var result = await Mediator.Send(new GetAllCountiesQuery());
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [HttpGet("getAllCountiesWithFilter")]
        public async Task<IActionResult> GetAllCountiesWithFilter(string searchTxt)
        {
            try
            {
                var result = await Mediator.Send(new GetAllCountiesWithFilterQuery { searchTxt = searchTxt });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        //[HttpGet()]
        //public string SendNotification(string deviceId, string message)
        //{
        //    string ApiKey = "server api key";
        //    var userId = "application number";
        //    var value = message;
        //    WebRequest tRequest;
        //    tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        //    tRequest.Method = "post";
        //    tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        //    tRequest.Headers.Add(string.Format("Authorization: key={0}", ApiKey));
        //    tRequest.Headers.Add(string.Format("Sender: id={0}", userId));
        //    string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
        //    Console.WriteLine(postData);
        //    Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //    tRequest.ContentLength = byteArray.Length;
        //    Stream dataStream = tRequest.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();
        //    WebResponse tResponse = tRequest.GetResponse();
        //    dataStream = tResponse.GetResponseStream();
        //    StreamReader tReader = new StreamReader(dataStream);
        //    String sResponseFromServer = tReader.ReadToEnd();
        //    tReader.Close();
        //    dataStream.Close();
        //    tResponse.Close();
        //    return sResponseFromServer;
        //}

        [HttpGet("getCountySafetyData")]
        public async Task<IActionResult> GetCountySafetyData(string fips,int createdBy, int updatedBy)
        {
            try
            {
                var result = await Mediator.Send(new GetCountySafetyDataQuery {Fips=fips,CreatedBy=createdBy,UpdatedBy=updatedBy });
                return Ok(new { status = "success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        [Obsolete]
        public CommonController(IHostingEnvironment env)
        {
            this.env = env;
        }


        [HttpGet("SendNotification")]
        public async Task<string> OnGetAsync()
        {
            try
            {
                var path = env.ContentRootPath;
                path = path + "\\Auth.json";
                FirebaseApp app = null;
                try
                {
                    app = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(path)
                    }, "Qbubble");
                }
                catch (Exception ex)
                {
                    app = FirebaseApp.GetInstance("Qbubble");
                }

                var fcm = FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(app);
                Message message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = "My push notification title",
                        Body = "Content for this push notification"
                    },
                    Data = new Dictionary<string, string>()
                 {
                     { "AdditionalData1", "data 1" },
                     { "AdditionalData2", "data 2" },
                     { "AdditionalData3", "data 3" },
                 },

                    Token = "fscLv-OjRCGnYw7ZOVtwlK:APA91bHUXUnrVZgHZect2-QSaq9tM8utGr91LOHaRdUQSmvkB9ac4JogsBs1u0oiIvRALkkDbUCOKUJddR4gG1kJRu3rdDopFZgy30RkhCZ_SgLkZA8ZD_5tg2nnbZfJYbK80_DzbyI_"
                };

               return await fcm.SendAsync(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
