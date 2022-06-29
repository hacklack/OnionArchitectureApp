using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
   public class PODMembers : BaseEntity
    {
        public PODMembers()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
        public int PODId { get; set; }
        public int BubbleId { get; set; }

        [ForeignKey("BubbleId")]
        public BubbleDetails BubbleDetails { get; set; }

        [ForeignKey("PODId")]
        public PodDetails PODDetails { get; set; }

    }
}
