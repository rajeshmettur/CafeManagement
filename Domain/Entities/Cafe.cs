using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Cafe
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string Name { get; set; } 

        [Required]
        public required string Description { get; set; } 

        public string? Logo { get; set; }

        [Required]
        [StringLength(255)]
        public required string Location { get; set; } 

        public ICollection<EmployeeCafe> EmployeeCafes { get; set; } = new List<EmployeeCafe>();
    }
}