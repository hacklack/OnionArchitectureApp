using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ApiModels
{
   public class PODMembersApiModel : BaseApiModel
    {
        public PODMembersApiModel()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int PODId { get; set; }
        public int BubbleId { get; set; }

    }
}
