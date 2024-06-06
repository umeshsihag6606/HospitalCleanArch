using Domain.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Counteries
{
    public class Countary:BaseAuditableEntity
    {
        public string Name {  get; set; }
    }
}
