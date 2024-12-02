using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeCafe
    {
        [ForeignKey(nameof(Employee))]
        public required string EmployeeId { get; set; }

        public Employee Employee { get; set; } = default!;

        [ForeignKey(nameof(Cafe))]
        public required Guid CafeId { get; set; }

        public Cafe Cafe { get; set; } = default!;

        [Required]
        public DateTime StartDate { get; set; }
    }
}