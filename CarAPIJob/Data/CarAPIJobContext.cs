using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPIJob.Data
{
    public class CarAPIJobContext : DbContext
    {
        public CarAPIJobContext (DbContextOptions<CarAPIJobContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Job> Job { get; set; } = default!;
    }
}
