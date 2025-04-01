using CargoPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoPay.Infrastructure
{
    public class CargoPayDbContext : DbContext
    {
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<User> Users => Set<User>();

        public CargoPayDbContext(DbContextOptions<CargoPayDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasIndex(c => c.CardNumber)
                .IsUnique();

            modelBuilder.Entity<Card>()
                .Property(c => c.CardNumber)
                .HasMaxLength(15)
                .IsRequired();
        }
    }

}
