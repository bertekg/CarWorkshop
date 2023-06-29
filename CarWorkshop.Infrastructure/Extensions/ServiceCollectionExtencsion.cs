using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using CarWorkshop.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarWorkshop.Infrastructure.Repositories;

namespace CarWorkshop.Infrastructure.Extensions;

public static class ServiceCollectionExtencsion
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarWorkshopDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("CarWorkshoop")));

        services.AddScoped<CarWorkshopSeeder>();
        services.AddScoped<ICarWorkshopRepository, CarWorkshopRepository>();
    }
}