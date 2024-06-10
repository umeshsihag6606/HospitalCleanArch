using Domain.Comman;
using Domain.Entities.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Staffs;

public class Staff:BaseAuditableEntity
{
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public int Age {  get; set; }

    [Phone (ErrorMessage ="Phone number is in Worng format")]
    public string MobileNumber {  get; set; }

    [EmailAddress(ErrorMessage ="Not a valid Email")]
    public string Email {  get; set; }
    public string Address {  get; set; }

    [ForeignKey("City")]
    public int CityId {  get; set; }
    public City City { get; set; }
}
