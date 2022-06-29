using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Features.Commands.UserCommands;
using Application.Features.Queries.UserQueries;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using WebApiApp.CommonCodes;
using Application.ApiModels;
using Application.Services;
namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]

    public class UserController : BaseApiController
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _appEnvironment;
        public UserController(IConfiguration config, IWebHostEnvironment appEnvironment)
        {
            _config = config;
            _appEnvironment = appEnvironment;
        }

        #region Sign-up User/Member
        /// <summary>
        /// Generate OTP to Register/Login User
        /// </summary>
        /// <param phone="phone"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GenerateOTP/{phone}")]
        public async Task<IActionResult> GenerateOTP(string phone)
        {
            try
            {
                // return Ok(await Mediator.Send(new GenerateOtpQuery { Phone = phone }));
                return Ok(new { status = "True", statuscode = 1, OTP = "123456" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = -1 });

            }
        }

        /// <summary>
        /// Validate OTP to Register/Login User
        /// </summary>
        /// <param OTP="OTP"></param>
        /// <param phone="phone"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ValidateOTP/{OTP}")]
        public async Task<IActionResult> ValidateOTP(string OTP, string phone)
        {
            try
            {
                // return Ok(await Mediator.Send(new GetUserOtp { Otp = OTP, Phone = phone }));
                if (OTP == "123456")
                {
                    return Ok(new { status = "True", statuscode = 1 });
                }
                else
                {
                    return Ok(new { status = "False", statuscode = 0 });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = -1 });
            }
        }

        /// <summary>
        /// Validate Mobile number exists or not
        /// </summary>
        /// <param phone="phone"></param>
        /// <returns></returns>
        [HttpGet("VerifyNumber/{phone}")]
        public async Task<IActionResult> VerifyNumber(string phone)
        {
            try
            {
                var user = await Mediator.Send(new GetUserByUserNameAndPhone { phone = phone });
                if (user != null)
                {
                    return Ok(new { status = "User Already Exists", statuscode = 0 });
                }
                else
                {
                    return Ok(new { status = "Not Exists", statuscode = 1 });
                }
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }

        }

        /// <summary>
        /// Create a new User.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost("create/{ufile}")]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
            //var image = ufile;
            //if (image != null)
            //{
            //    var formCollection = await Request.ReadFormAsync();
            //    var file = formCollection.Files.FirstOrDefault();
            //    //var files = HttpContext.Request.Form.Files;
            //    //foreach (var Image in files)
            //    //{
            //    if (file != null && file.Length > 0)
            //    {
            //        var fileimg = file;
            //        //There is an error here
            //        var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
            //        if (fileimg.Length > 0)
            //        {
            //            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            //            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            //            {
            //                await file.CopyToAsync(fileStream);
            //                ufile = fileName;
            //            }

            //        }
            //    }
            //    return Ok(await Mediator.Send(new UploadUserProfilepic { ImageUrl = ufile }));
            //}
        }

        ///<summary>
        /// Add or update the profile photo for current user 
        ///</summary>
        /// <returns>Profile photo path</returns>
        //[HttpPost("upload/{ufile}/{id}")]
        //public async Task<ActionResult> SetProfilePhoto([FromForm] string ufile, int id)
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.FirstOrDefault();
        //        if (file != null && file.Length > 0)
        //        {
        //            var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
        //            if (file.Length > 0)
        //            {
        //                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
        //                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                    ufile = fileName;
        //                }
        //            }
        //        }
        //        return Ok(await Mediator.Send(new UploadUserProfilepic { ImageUrl = ufile, Id = id }));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;

        //    }
        //}
        [Consumes("multipart/form-data")]
        [HttpPost("uploadProfilePic/{id}/{file}")]
        public async Task<ActionResult> SetProfilePhoto([FromForm] UserProfileImageApiModel apiModel)
        {
            try
            {
                if (apiModel.file.Length > 0)
                {
                    string fName = apiModel.file.FileName;
                    string ImageLocation = Path.Combine("C:/Users/QbubblePublished/wwwroot", "/Images");
                    string path = Path.Combine("C:/Users/QbubblePublished/wwwroot/Images/" + apiModel.id + "_" + apiModel.file.FileName);
                    string pathdb = Path.Combine("http://mthouse.dyndns.org/Images/" + apiModel.id + "_" + apiModel.file.FileName);
                    if (!Directory.Exists(ImageLocation))
                    {
                        Directory.CreateDirectory(ImageLocation);
                    }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await apiModel.file.CopyToAsync(stream);
                    }

                    var result = await Mediator.Send(new UploadUserProfilepic { ImageUrl = pathdb, Id = apiModel.id });
                    // result.userProfileImagePath = _appEnvironment.WebRootFileProvider.GetFileInfo("Images\\" + apiModel.id + apiModel.file.FileName)?.PhysicalPath;
                    return Ok(new { status = "Image Uploded Successfully", statuscode = 1, data = result });
                }
                else
                {
                    return Ok(new { status = "No File were selected", statuscode = 0 });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = ex.Message, statuscode = 0 });
            }
        }
        #endregion

        #region Login User/Member
        [AllowAnonymous]
        [HttpPost("Login/{phone}")]
        public async Task<IActionResult> Login(string phone)
        {
            IActionResult response = Unauthorized();
            var user = await Mediator.Send(new GetUserByUserNameAndPhone { phone = phone });
            if (user != null)
            {
                CommoMethods commoMethods = new CommoMethods(_config);
                var tokenString = commoMethods.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, UserDetails = user });
            }
            return Ok(response);
        }
        #endregion

        /// <summary>
        /// Get Member by User Id.
        /// </summary>
        /// <param userid="userid"></param>
        /// <returns></returns>
        [HttpGet("GetMember/{userid}")]
        public async Task<IActionResult> GetMemberByUserId(int userid)
        {
            try
            {
                return Ok(await Mediator.Send(new GetMemberByUserIdQuery { UserId = userid }));
            }
            catch (Exception)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        /// <summary>
        /// Delete Member by User Id.
        /// </summary>
        /// <param userid="userid"></param>
        /// <param memberId="memberId"></param>
        /// <returns></returns>
        [HttpPost("{userid},{memberid}")]
        public async Task<IActionResult> DeleteMemberByUserId(int userid, int memberid)
        {
            try
            {
                var result = await Mediator.Send(new DeleteMemberByUserIdCommand { UserId = userid, MemberId = memberid });
                return Ok(new { status = "Success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        /// <summary>
        /// Gets all Products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        /// <summary>
        /// Gets Product Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
        }

        /// <summary>
        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id, bool isActive)
        {
            try
            {
                var result = await Mediator.Send(new DeleteUserByIdCommand { Id = id, IsActive = isActive });
                return Ok(new { status = "Success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        /// <summary>
        /// Updates the Product Entity based on Id.   
        /// </summary>
        /// <param id="id"></param>
        /// <param command="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Update(UpdateUserCommand command, int id)
        {
            try
            {
                if (id == command.Id || command.CreatedBy == id)
                {
                    var result = await Mediator.Send(command);
                    return Ok(new { status = "Success", statuscode = 1, data = result });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

        /// <summary>
        /// Get all Counties  
        /// </summary>
        /// <returns></returns>
        //[HttpGet("GetAllCounties")]
        //public async Task<IActionResult> GetAllCounties()
        //{
        //    try
        //    {
        //        var result = await Mediator.Send(new GetAllCountiesQuery());
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

        /// <summary>
        /// Get all Counties by Name
        /// <param name="name"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCountiesByName/{name}")]
        public async Task<IActionResult> GetAllCountiesByName(string name)
        {
            try
            {
                var result = await Mediator.Send(new GetAllCountiesByNameQuery { Name = name });
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
                    status = "error",
                    statuscode = 0
                });
            }
        }

        [HttpPost("createUserDeviceToken")]
        public async Task<IActionResult> CreateUserDeviceToken(CreateUpdateUserDeviceDetailsCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpGet("getAllDeviceTokens")]
        public async Task<IActionResult> GetAllDeviceTokens(int id)
        {
            return Ok(await Mediator.Send(new GetAllUsersDeviceTokenQuery { Id=id}));
        }
        [HttpPost("DeleteDeviceToken/{id}")]
        public async Task<IActionResult> DeleteDeviceToken(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteUserDeviceDetailsByIdCommand { Id = id});
                return Ok(new { status = "Success", statuscode = 1, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Error", statuscode = 0 });
            }
        }

    }
}
