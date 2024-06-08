using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Data
{
    public class CarAPIContext : DbContext
    {
        public CarAPIContext(DbContextOptions<CarAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Car> Car { get; set; } = default!;
        public DbSet<Models.Person> Person { get; set; }
        public DbSet<Models.Client> Client { get; set; }
        public DbSet<Models.Employee> Employee { get; set; }
        public DbSet<Models.BankPaymentSlip>? BankPaymentSlip { get; set; }
        public DbSet<Models.CreditCard>? CreditCard { get; set; }
        public DbSet<Models.Pix>? Pix { get; set; }
        public DbSet<Models.Buy> Buy { get; set; }
        public DbSet<Models.Payment> Payment { get; set; }
        public DbSet<Models.Address>? Address { get; set; }

        public DbSet<Models.CarJob>? CarJob { get; set; }

        public DbSet<Models.Job>? Job { get; set; }

        public DbSet<Models.Sale> Sale { get; set; }

        public DbSet<Models.PositionCompany>? PositionCompany { get; set; }

        public DbSet<Models.PixType>? PixType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Person>().HasKey(p => p.Document);
            modelBuilder.Entity<Models.Person>().ToTable("Person");
            modelBuilder.Entity<Models.Client>().ToTable("Client");
            modelBuilder.Entity<Models.Employee>().ToTable("Employee");
            //modelBuilder.Entity<Models.Employee>().HasKey(e => e.Document);
        }


    }
}
