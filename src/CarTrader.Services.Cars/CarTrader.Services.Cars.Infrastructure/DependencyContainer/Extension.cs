using Microsoft.Extensions.DependencyInjection;
using CarTrader.Services.Cars.Application.Services;
using CarTrader.Services.Cars.Application.Interfaces.Services;
using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using CarTrader.Services.Cars.Infrastructure.Repositories;
using CarTrader.Services.Cars.Infrastructure.Services;

namespace CarTrader.Services.Cars.Infrastructure.DependencyContainer
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICarsRepository, CarsRepository>();

            // Services
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageSubscriber, MessageSubscriber>();

            // Queue
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue>(_ =>
            {
                const int queueCapacity = 100;
                return new BackgroundTaskQueue(queueCapacity);
            });

            return services;
        }
    }
}