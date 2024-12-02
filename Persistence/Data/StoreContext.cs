using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.SeedData
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Cafe> Cafes { get; set; } = default!;
        public DbSet<EmployeeCafe> EmployeeCafes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
                modelBuilder.Entity<EmployeeCafe>()
                    .HasKey(ec => new { ec.EmployeeId, ec.CafeId });

                modelBuilder.Entity<EmployeeCafe>()
                    .HasOne(ec => ec.Employee)
                    .WithMany(e => e.EmployeeCafes)
                    .HasForeignKey(ec => ec.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                 modelBuilder.Entity<EmployeeCafe>()
                    .HasOne(ec => ec.Cafe)
                    .WithMany(c => c.EmployeeCafes)
                    .HasForeignKey(ec => ec.CafeId)
                    .OnDelete(DeleteBehavior.Restrict); 

                modelBuilder.Entity<EmployeeCafe>()
                            .HasIndex(ec => ec.EmployeeId)
                            .IsUnique();    
           
            /* modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Cafe>()
                .Property(c => c.Id)
                .IsRequired();

            modelBuilder.Entity<Employee>().ToTable(x =>
             x.HasCheckConstraint("CK_Employee_EmailAddress_Format", "[EmailAddress] LIKE '%_@__%.__%'"));
             
            modelBuilder.Entity<Employee>().ToTable(x =>
             x.HasCheckConstraint("CK_Employee_PhoneNumber_Format", "[PhoneNumber] LIKE '[89]%' AND LEN([PhoneNumber]) = 8"));
         
            modelBuilder.Entity<Employee>().ToTable(x =>
             x.HasCheckConstraint("CK_Employee_EmployeeID_Format", "[ID] LIKE '[UI]%' AND LEN([ID]) = 9")); */

            //  var cafes = SeedDataLoader.LoadSeedData<Cafe>("cafe-seed.json");
            // modelBuilder.Entity<Cafe>().HasData(cafes);

            // Seed data for Employee (no tracking to avoid conflict)
            // var employees = SeedDataLoader.LoadSeedData<Employee>("employee-seed.json");
            // modelBuilder.Entity<Employee>().HasData(employees);

            // // Seed data for EmployeeCafe relationship
            // var employeeCafes = SeedDataLoader.LoadSeedData<EmployeeCafe>("employee-cafe-seed.json");
            // modelBuilder.Entity<EmployeeCafe>().HasData(employeeCafes);

        }
    }
}