using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence;
public class CarWorkshopDbContext : DbContext
{
    public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options)
    {
        
    }
    public DbSet<Domain.Entities.CarWorkshop> CarWorkhops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.CarWorkshop>()
            .OwnsOne(c => c.ContactDetails);
    }
}