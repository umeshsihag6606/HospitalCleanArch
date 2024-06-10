using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Comman.Mappings;
using Domain.Entities.Staffs;

namespace Application.Dto.Staffs
{
    public class GetStaffDto:IMapFrom<Staff>
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int Age { get; set; }
        public long MobileNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int CityId { get; set; }
        
    }
}
