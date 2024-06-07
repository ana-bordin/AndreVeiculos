using Microsoft.EntityFrameworkCore;

namespace CarAPI.Data
{
    public class CarAPIContext : DbContext
    {
        public CarAPIContext(DbContextOptions<CarAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Car> Car { get; set; } = default!;
        public DbSet<Models.Person> Person { get; set; } = default!;
        public DbSet<Models.Client> Client { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Person>().HasKey(p => p.Document);
        }
    }
}
