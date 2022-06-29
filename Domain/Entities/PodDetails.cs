using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;


namespace Domain.Entities
{
    public class PodDetails : BaseEntity
    {
        public PodDetails()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
      
        public string PODName { get; set; }
        public int PODBubbleType { get; set; }
        public int PODSize { get; set; }
        public string PODDescription { get; set; }

    }
}
