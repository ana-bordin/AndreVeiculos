using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Employee.Data
{
    public class CarAPIEmployeeContext : DbContext
    {
        public CarAPIEmployeeContext (DbContextOptions<CarAPIEmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Employee> Employee { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Person>().HasKey(p => p.Document); 
            modelBuilder.Entity<Models.Address>().HasKey(p => p.Id);
            modelBuilder.Entity<Models.Employee>().ToTable("Employee");
        }
    }
}
