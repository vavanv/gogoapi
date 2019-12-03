using System;

using Microsoft.EntityFrameworkCore;

using Services.Entities;

namespace Services.DbContextFactory
{
    public class GoGoDbContext : DbContext
    {
        private readonly string _connectionStringKey;

        public GoGoDbContext(string connectionString)
        {
            _connectionStringKey = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionStringKey);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CacheConfig());
            modelBuilder.ApplyConfiguration(new ShapeConfig());
        }
    }
}
