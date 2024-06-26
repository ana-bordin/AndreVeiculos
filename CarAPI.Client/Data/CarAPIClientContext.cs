﻿using Microsoft.EntityFrameworkCore;
using Models;

namespace CarAPI.Client.Data
{
    public class CarAPIClientContext : DbContext
    {
        public CarAPIClientContext(DbContextOptions<CarAPIClientContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Client> Client { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Person>().HasKey(p => p.Document);
            modelBuilder.Entity<Models.Address>().HasKey(p => p.Id);
            modelBuilder.Entity<Models.Client>().ToTable("Client");
        }
    }
}
