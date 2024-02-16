using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Reservation>()
                .HasKey(r => new { r.TableId, r.CustomerId});

            modelBuilder
                .Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Table>()
                .HasData(
                new Table()
                {
                    Id = 1,
                    NumberOfSeats = 10,
                    IsSmokingAllowed = true
                },
                new Table()
                {
                    Id = 2,
                    NumberOfSeats = 5,
                    IsSmokingAllowed = false
                },
                new Table()
                {
                    Id = 3,
                    NumberOfSeats = 20,
                    IsSmokingAllowed = false
                },
                new Table()
                {
                    Id = 4,
                    NumberOfSeats = 2,
                    IsSmokingAllowed = true
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
