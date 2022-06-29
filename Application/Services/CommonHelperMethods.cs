using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Domain.Entities;
using System.Linq;
using Application.Interfaces;
using static Domain.CommonCodes.CommonEnums;
using Application.ApiModels;

namespace Application.Services
{
    public static class CommonHelperMethods
    {
        private static readonly IApplicationDbContext _context;

        public static string DateFormatterMethod(DateTime date)
        {
            string datestr = date.ToString("yyyy-MM-dd'T'HH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
            return datestr;
        }

        
    }
}
