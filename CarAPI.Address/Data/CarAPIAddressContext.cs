using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Address.Data
{
    public class CarAPIAddressContext : DbContext
    {
        public CarAPIAddressContext (DbContextOptions<CarAPIAddressContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Address> Address { get; set; } = default!;
    }
}
