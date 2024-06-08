using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Buy.Data
{
    public class CarAPIBuyContext : DbContext
    {
        public CarAPIBuyContext (DbContextOptions<CarAPIBuyContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Buy> Buy { get; set; } = default!;
    }
}
