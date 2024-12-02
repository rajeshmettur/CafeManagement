using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
{
    [Key]
    [StringLength(10)]
    [RegularExpression(@"^UI[A-Za-z0-9]{7}$")]
    public required string Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string Name { get; set; } 

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string EmailAddress { get; set; } 

    [Required]
    [RegularExpression(@"^[89]\d{7}$")]
    public required string PhoneNumber { get; set; } 

    [Required]
    public required string Gender { get; set; }

    public ICollection<EmployeeCafe> EmployeeCafes { get; set; } = new List<EmployeeCafe>();
}

    public enum Gender {
        Male,
        Female
    }
}