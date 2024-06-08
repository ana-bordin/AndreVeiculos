using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Payment.Data
{
    public class CarAPIPaymentContext : DbContext
    {
        public CarAPIPaymentContext (DbContextOptions<CarAPIPaymentContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Payment> Payment { get; set; } = default!;

        public DbSet<Models.Pix>? Pix { get; set; }

        public DbSet<Models.BankPaymentSlip>? BankPaymentSlip { get; set; }

        public DbSet<Models.CreditCard>? CreditCard { get; set; }

        public DbSet<Models.PixType>? PixType { get; set; }
    }
}
