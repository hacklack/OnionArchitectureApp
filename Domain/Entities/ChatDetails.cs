using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using static Domain.CommonCodes.CommonEnums;

namespace Domain.Entities
{
   public class ChatDetails : BaseEntity
    {
        public ChatDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int ChatTypeId { get; set; }
        public int ChatParentTypeId { get; set; }
        public int ChatParentId { get; set; }
        public bool ChatStatus { get; set; }
    }
}
