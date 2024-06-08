using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Car.Data
{
    public class CarAPICarContext : DbContext
    {
        public CarAPICarContext (DbContextOptions<CarAPICarContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Car> Car { get; set; } = default!;
    }
}
