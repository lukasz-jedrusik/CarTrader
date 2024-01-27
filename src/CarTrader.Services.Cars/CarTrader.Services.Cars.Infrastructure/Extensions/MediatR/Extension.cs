using System.Reflection;
using CarTrader.Services.Cars.Application.Commands.AddCar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.Cars.Infrastructure.Extensions.MediatR
{
    public static class Extension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddCarCommand).Assembly));
            return services;
        }
    }
}