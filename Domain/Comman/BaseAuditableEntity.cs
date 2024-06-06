using Domain.Comman.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Comman
{
    public class BaseAuditableEntity : BaseEntity, IAudiTableEntity
    {
        public int? CreateBy { get ; set; }
        public int? UpdateDateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
