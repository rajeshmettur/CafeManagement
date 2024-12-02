using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context) 
        {
               if(context.Employees.Count() <= 0) 
                {
                    var employeeData = await File.ReadAllTextAsync("../Persistence/Data/SeedData/employee-seed.json");
                    var employees = JsonSerializer.Deserialize<List<Employee>>(employeeData);
                    if(employees == null) return;
                    context.Employees.AddRange(employees);
                    await context.SaveChangesAsync();
                }
                
                if(context.Cafes.Count() <= 0) 
                {
                    var cafeData = await File.ReadAllTextAsync("../Persistence/Data/SeedData/cafe-seed.json");
                    var cafes = JsonSerializer.Deserialize<List<Cafe>>(cafeData);
                    if(cafes == null) return;
                    context.Cafes.AddRange(cafes);
                    await context.SaveChangesAsync();
                }


                if (context.EmployeeCafes.Count() <= 0)
                {
                    var employeeCafeData  = await File.ReadAllTextAsync("../Persistence/Data/SeedData/employee-cafe-seed.json");
                    var data = JsonSerializer.Deserialize<List<EmployeeCafe>>(employeeCafeData);
                    if(data == null) return;
                    context.EmployeeCafes.AddRange(data);
                    await context.SaveChangesAsync();
                }
        }
          
    }
}