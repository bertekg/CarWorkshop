using CarWorkshop.Application.Mappings;
using CarWorkshop.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Application.Extensions;

public static class ServiceColletionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICarWorkshopService, CarWorkshopService>();

        services.AddAutoMapper(typeof(CarWorkshopMappingProfile));
    }
}