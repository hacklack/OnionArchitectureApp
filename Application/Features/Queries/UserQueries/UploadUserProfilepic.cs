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
using System.Net.Http.Headers;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Application.ApiModels;

namespace Application.Features.Queries.UserQueries
{
    public class UploadUserProfilepic : IRequest<UserApiModels>
    {

        public string ImageUrl { get; set; }
        public int Id { get; set; }
        public class UploadUserProfilepicHandler : IRequestHandler<UploadUserProfilepic, UserApiModels>
        {
            private readonly IHostingEnvironment _appEnvironment;
            private readonly IApplicationDbContext _context;
            public UploadUserProfilepicHandler(IApplicationDbContext context, IHostingEnvironment appEnvironment)
            {
                _context = context;
                _appEnvironment = appEnvironment;
            }


            public async Task<UserApiModels> Handle(UploadUserProfilepic query, CancellationToken cancellationToken)
            {
                UserApiModels apiModel = new UserApiModels();
                if (!string.IsNullOrEmpty(query.ImageUrl) && query.Id > 0)
                {
                    var userData = _context.userDetails.Where(x => (x.Id == query.Id) && (x.IsActive == true)).FirstOrDefault();
                    userData.ProfilePicUrl = query.ImageUrl;
                    userData.UpdatedOn = DateTime.UtcNow;
                    userData.UpdatedBy = query.Id;
                    await _context.SaveChanges();
                    apiModel.Id = userData.Id;
                    apiModel.PhoneNo = userData.PhoneNo;
                    apiModel.Username = userData.Username;
                    apiModel.ProfilePicUrl = userData.ProfilePicUrl;
                    apiModel.UpdatedBy = userData.UpdatedBy;
                    apiModel.UpdatedOn = userData.UpdatedOn;
                    apiModel.County = userData.County;
                    apiModel.CountyName = _context.counties.Where(y => y.Fips == userData.County).Select(x => x.CountyName).FirstOrDefault();

                }
                //apiModel.userProfileImagePath= "http://mtmohali.dyndns.org:2021/" + query.ImageUrl;
                return apiModel;
            }
        }
    }
}







