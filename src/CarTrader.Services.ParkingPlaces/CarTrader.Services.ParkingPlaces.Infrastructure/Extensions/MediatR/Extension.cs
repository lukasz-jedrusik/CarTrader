using MediatR;
using CarTrader.Services.ParkingPlaces.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Extensions.MediatR
{
    public static class Extension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BackgroundTaskQueue).Assembly));
            return services;
        }
    }
}