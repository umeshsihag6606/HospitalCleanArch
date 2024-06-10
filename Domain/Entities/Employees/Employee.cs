using Domain.Comman;
using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Employees
{
    public class Employee:BaseAuditableEntity
    {
        public string FastName {  get; set; }
        public string LastName { get; set; }
        public string FatherName{ get; set; }
        public int Age { get; set; }
        [Phone  (ErrorMessage = "Invalid phone format")]
        public string PhoneNumber {  get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }

        [ForeignKey("City")]
        public int CityId {  get; set; }
        public City City { get; set; }
    }
}
