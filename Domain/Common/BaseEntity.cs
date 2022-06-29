using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
   public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
