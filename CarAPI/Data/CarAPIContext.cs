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
    }
}
