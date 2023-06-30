﻿using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Application.Extensions;

public static class ServiceColletionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddMediatR(typeof(CreateCarWorkshopCommand));

        services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

        services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
    }
}