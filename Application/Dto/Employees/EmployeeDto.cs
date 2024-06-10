using Application.Comman.Mappings;
using Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Employees
{
    public class EmployeeDto:IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string FastName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int Age { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
    }
}
