using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence;
public class CarWorkshopDbContext : DbContext
{
    public DbSet<Domain.Entities.CarWorkshop> CarWorkhops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarWorkshopDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.CarWorkshop>()
            .OwnsOne(c => c.ContactDetails);
    }
}