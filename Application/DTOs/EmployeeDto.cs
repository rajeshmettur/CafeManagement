using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.DTOs
{
    public class EmployeeDto
    {
        public required string Id { get; set; } // Format: UIXXXXXXX
        public required string Name { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; } // Starts with 8 or 9
        public string Gender { get; set; }  = string.Empty;// Male/Female

       // public Gender GenderEnum { get; set; }
        public DateTime StartDate { get; set; }

        public Guid CafeId { get; set; }
        public String? Cafe { get; set; } 

        public int  DaysWorked { get; set; }
    }
}