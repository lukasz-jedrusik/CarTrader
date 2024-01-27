using CarTrader.Services.Cars.Application.Commands.AddCar;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.Cars.Infrastructure.Extensions.FluentValidation
{
    public static class Extension
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AddCarCommandValidator>();

            return services;
        }
    }
}