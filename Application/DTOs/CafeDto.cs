using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTOs
{
    public class CafeDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
         public string? Logo { get; set; }
        public required string Location { get; set; }
        public int Employees { get; set; } 
    }
}