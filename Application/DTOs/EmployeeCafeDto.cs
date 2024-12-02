using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeCafeDto
    {
        public string EmployeeId { get; set; } = default!;
        public string EmployeeName { get; set; } = default!;
        public Guid CafeId { get; set; }
        public string CafeName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public int DaysWorked { get; set; }
    }
}