using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.CarJob.Data
{
    public class CarAPICarJobContext : DbContext
    {
        public CarAPICarJobContext (DbContextOptions<CarAPICarJobContext> options)
            : base(options)
        {
        }

        public DbSet<Models.CarJob> CarJob { get; set; } = default!;
    }
}
