using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Sale.Data
{
    public class CarAPISaleContext : DbContext
    {
        public CarAPISaleContext (DbContextOptions<CarAPISaleContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Sale> Sale { get; set; } = default!;
    }
}
