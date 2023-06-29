using CarWorkshop.Infrastructure.Persistence;

namespace CarWorkshop.Infrastructure.Seeders;

public class CarWorkshopSeeder
{
    private readonly CarWorkshopDbContext _dbContext;

    public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if(_dbContext.CarWorkhops.Any() == false)
            {
                Domain.Entities.CarWorkshop mazdaAso = new Domain.Entities.CarWorkshop()
                {
                    Name = "Mazd ASO",
                    Description = "Autoryzowany serwis Mazda",
                    ContactDetails = new()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001",
                        PhoneNumber = "+48123456789"
                    }
                };
                mazdaAso.EncodeName();
                _dbContext.CarWorkhops.Add(mazdaAso);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}